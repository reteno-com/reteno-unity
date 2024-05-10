#if UNITY_IOS
#define ADD_APP_GROUP

using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
using Debug = UnityEngine.Debug;

namespace RetenoSDK.iOS
{
    /// <summary>
    ///     Adds required frameworks to the iOS project, and adds the RetenoNotificationServiceExtension. Also handles
    ///     making sure both targets (app and extension service) have the correct dependencies
    /// </summary>
    public class BuildPostProcessor : IPostprocessBuildWithReport
    {
        private const string ServiceExtensionTargetName = "NotificationServiceExtension";
        private const string ServiceExtensionFilename = "NotificationService.swift";
        private const string DependenciesFilename = "RetenoiOSDependencies.xml";
        private const string PackageName = "Assets/Reteno";
        
        private static readonly string EditorFilesPath = Path.Combine(PackageName, "iOS", "Editor");
        private static readonly string PluginLibrariesPath = Path.Combine(PackageName, "iOS", "Runtime", "Plugins", "iOS");
        private static readonly string PluginFilesPath = Path.Combine(PluginLibrariesPath);

        private readonly PBXProject _project = new();

        private string _outputPath;
        private string _projectPath;
        
        //group.{bundle_id}.reteno-local-storage
        private string _appGroupName => $"group.{PlayerSettings.applicationIdentifier}.reteno-local-storage";

        /// <summary>
        ///     must be between 40 and 50 to ensure that it's not overriden by Podfile generation (40) and that it's
        ///     added before "pod install" (50)
        ///     https://github.com/googlesamples/unity-jar-resolver#appending-text-to-generated-podfile
        /// </summary>
        public int callbackOrder => 45;

        /// <summary>
        ///     Entry for the build post processing necessary to get the Reteno SDK iOS up and running
        /// </summary>
        public void OnPostprocessBuild(BuildReport report)
        {
            if (report.summary.platform != BuildTarget.iOS)
                return;

            // Load the project
            _outputPath = report.summary.outputPath;
            _projectPath = PBXProject.GetPBXProjectPath(_outputPath);
            _project.ReadFromString(File.ReadAllText(_projectPath));

            string targetGuid = _project.GetUnityMainTargetGuid();

            //// Configure build settings
            //_project.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");
            _project.SetBuildProperty(targetGuid, "SWIFT_VERSION", "5.0");  // Задайте версію Swift
            _project.SetBuildProperty(targetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
            //_project.SetBuildProperty(targetGuid, "SWIFT_OBJC_BRIDGING_HEADER", "Unity-iPhone/Classes/TestOBJc-Bridging-Header.h");
            _project.SetBuildProperty(targetGuid, "SWIFT_OBJC_BRIDGING_HEADER", "Libraries/Reteno/iOS/Runtime/Plugins/iOS/TestOBJc-Bridging-Header.h");
            _project.SetBuildProperty(targetGuid, "SWIFT_OBJC_INTERFACE_HEADER_NAME", "TestOBJc-Bridging-Header.h");
            _project.AddBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");

            //Turn on capabilities required by Reteno
            AddProjectCapabilities();

            // Add the service extension
            AddNotificationServiceExtension();

            DisableBitcode();

            // Save the project back out
            File.WriteAllText(_projectPath, _project.WriteToString());
        }

        /// <summary>
        ///     Get existing entitlements file if exists or creates a new file, adds it to the project, and returns the path
        /// </summary>
        private string GetEntitlementsPath(string targetGuid, string targetName)
        {
            var relativePath = _project.GetBuildPropertyForAnyConfig(targetGuid, "CODE_SIGN_ENTITLEMENTS");

            if (relativePath != null)
            {
                var fullPath = Path.Combine(_outputPath, relativePath);

                if (File.Exists(fullPath))
                    return fullPath;
            }

            var entitlementsPath = Path.Combine(_outputPath, targetName, $"{targetName}.entitlements");

            // make new file
            var entitlementsPlist = new PlistDocument();
            entitlementsPlist.WriteToFile(entitlementsPath);

            // Copy the entitlement file to the xcode project
            var entitlementFileName = Path.GetFileName(entitlementsPath);
            var relativeDestination = targetName + "/" + entitlementFileName;

            // Add the pbx configs to include the entitlements files on the project
            _project.AddFile(relativeDestination, entitlementFileName);
            _project.SetBuildProperty(targetGuid, "CODE_SIGN_ENTITLEMENTS", relativeDestination);

            return relativeDestination;
        }

        /// <summary>
        ///     Add the required capabilities and entitlements for Reteno
        /// </summary>
        private void AddProjectCapabilities()
        {
            var targetGuid = _project.GetMainTargetGuid();
            var targetName = _project.GetMainTargetName();

            var entitlementsPath = GetEntitlementsPath(targetGuid, targetName);
            var projCapability = new ProjectCapabilityManager(_projectPath, entitlementsPath, targetName);

            projCapability.AddBackgroundModes(BackgroundModesOptions.RemoteNotifications);
            projCapability.AddPushNotifications(false);
            projCapability.AddAppGroups(new[] { _appGroupName });

            projCapability.WriteToFile();
        }

        /// <summary>
        ///     Create and add the notification extension to the project
        /// </summary>
        private void AddNotificationServiceExtension()
        {
#if !UNITY_CLOUD_BUILD
            // refresh plist and podfile on appends
            ExtensionCreatePlist(_outputPath);
            ExtensionAddPodsToTarget();

            var extensionGuid = _project.TargetGuidByName(ServiceExtensionTargetName);

            // skip target setup if already present
            if (!string.IsNullOrEmpty(extensionGuid))
                return;

            extensionGuid = _project.AddAppExtension(_project.GetMainTargetGuid(),
                ServiceExtensionTargetName,
                PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.iOS) + "." + ServiceExtensionTargetName,
                ServiceExtensionTargetName + "/" + "Info.plist" // Unix path as it's used by Xcode
            );

            ExtensionAddSourceFiles(extensionGuid);

            // Makes it so that the extension target is Universal (not just iPhone) and has an iOS 10 deployment target
            _project.SetBuildProperty(extensionGuid, "TARGETED_DEVICE_FAMILY", "1,2");
            _project.SetBuildProperty(extensionGuid, "IPHONEOS_DEPLOYMENT_TARGET", "12.0");
            _project.SetBuildProperty(extensionGuid, "SWIFT_VERSION", "5.0");
            _project.SetBuildProperty(extensionGuid, "ARCHS", "arm64");
            _project.SetBuildProperty(extensionGuid, "DEVELOPMENT_TEAM", PlayerSettings.iOS.appleDeveloperTeamID);
            _project.SetBuildProperty(extensionGuid, "ENABLE_BITCODE", "NO");

            _project.AddBuildProperty(extensionGuid, "LIBRARY_SEARCH_PATHS",
                $"$(PROJECT_DIR)/Libraries/{PluginLibrariesPath.Replace("\\", "/")}");

            _project.WriteToFile(_projectPath);

            // add capabilities + entitlements
            var entitlementsPath = GetEntitlementsPath(extensionGuid, ServiceExtensionTargetName);
            var projCapability =
                new ProjectCapabilityManager(_projectPath, entitlementsPath, ServiceExtensionTargetName);

            projCapability.AddAppGroups(new[] { _appGroupName });

            projCapability.WriteToFile();
#endif
        }
        
        /// <summary>
        ///     Add the swift source file required by the notification extension
        /// </summary>
        private void ExtensionAddSourceFiles(string extensionGuid)
        {
            var buildPhaseID = _project.AddSourcesBuildPhase(extensionGuid);
            var sourcePath = Path.Combine(PluginFilesPath, ServiceExtensionFilename);
            var destPathRelative = Path.Combine(ServiceExtensionTargetName, ServiceExtensionFilename);

            var destPath = Path.Combine(_outputPath, destPathRelative);
            if (!File.Exists(destPath))
                FileUtil.CopyFileOrDirectory(sourcePath.Replace("\\", "/"), destPath.Replace("\\", "/"));

            var sourceFileGuid = _project.AddFile(destPathRelative, destPathRelative);
            _project.AddFileToBuildSection(extensionGuid, buildPhaseID, sourceFileGuid);
        }

        /// <summary>
        ///     Create a .plist file for the extension
        /// </summary>
        /// <remarks>NOTE: File in Xcode project is replaced everytime, never appends</remarks>
        private bool ExtensionCreatePlist(string path)
        {
            var extensionPath = Path.Combine(path, ServiceExtensionTargetName);
            Directory.CreateDirectory(extensionPath);

            var plistPath = Path.Combine(extensionPath, "Info.plist");
            var alreadyExists = File.Exists(plistPath);

            var notificationServicePlist = new PlistDocument();
            notificationServicePlist.ReadFromFile(Path.Combine(PluginFilesPath, "Info.plist"));
            notificationServicePlist.root.SetString("CFBundleShortVersionString", PlayerSettings.bundleVersion);
            notificationServicePlist.root.SetString("CFBundleVersion", PlayerSettings.iOS.buildNumber);

            notificationServicePlist.WriteToFile(plistPath);

            return alreadyExists;
        }

        private void ExtensionAddPodsToTarget()
        {
            var podfilePath = Path.Combine(_outputPath, "Podfile");

            if (!File.Exists(podfilePath))
            {
                Debug.LogError($"Could not find Podfile. {ServiceExtensionFilename} will have errors.");
                return;
            }

            var dependenciesFilePath = Path.Combine(EditorFilesPath, DependenciesFilename);

            if (!File.Exists(dependenciesFilePath))
            {
                Debug.LogError($"Could not find {DependenciesFilename}");
                return;
            }

            var dependenciesFile = File.ReadAllText(dependenciesFilePath);
            var dependenciesRegex =
                new Regex("(?<=<iosPod name=\"Reteno\" version=\").+(?=\" addToAllTargets=\"true\" />)");

            if (!dependenciesRegex.IsMatch(dependenciesFile))
            {
                Debug.LogError($"Could not read current iOS framework dependency version from {DependenciesFilename}");
                return;
            }

            var podfile = File.ReadAllText(podfilePath);
            var podfileRegex =
                new Regex($@"target '{ServiceExtensionTargetName}' do\n  pod 'Reteno', '(.+)'\nend\n");

            var requiredVersion = dependenciesRegex.Match(dependenciesFile).ToString();
            var requiredTarget =
                $"target '{ServiceExtensionTargetName}' do\n  pod 'Reteno', '{requiredVersion}'\nend\n";

            if (!podfileRegex.IsMatch(podfile))
            {
                podfile += requiredTarget;
            }
            else
            {
                var podfileTarget = podfileRegex.Match(podfile).ToString();
                podfile = podfile.Replace(podfileTarget, requiredTarget);
            }

            File.WriteAllText(podfilePath, podfile);
        }

        private void DisableBitcode()
        {
            // Main
            var targetGuid = _project.GetMainTargetGuid();
            _project.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");

            // Unity Tests
            var unityTests = _project.TargetGuidByName(PBXProject.GetUnityTestTargetName());
            _project.SetBuildProperty(unityTests, "ENABLE_BITCODE", "NO");

            // Unity Framework
            var unityFramework = _project.GetUnityFrameworkTargetGuid();
            _project.SetBuildProperty(unityFramework, "ENABLE_BITCODE", "NO");
        }
    }
}
#endif