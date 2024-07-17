#if UNITY_IOS
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
using UnityEngine;

namespace Reteno.iOS.Editor
{
    public class BuildPostProcessor : IPostprocessBuildWithReport
{
    private static readonly string[] PossiblePaths = {
        "Assets/com.reteno.ios",
        "Packages/com.reteno.ios"
    };

    private string _editorFilesPath;
    private string _pluginLibrariesPath;
    private string _pluginFilesPath;

    private const string ServiceExtensionTargetName = "NotificationServiceExtension";
    private const string ServiceExtensionFilename = "NotificationService.swift";
    private const string DependenciesFilename = "RetenoiOSDependencies.xml";
    
    private string _projectPath;
    private string _outputPath;
    
    private string _appGroupName => $"group.{PlayerSettings.applicationIdentifier}.reteno";

    private PBXProject _project;

    public int callbackOrder => 45;

    public void OnPostprocessBuild(BuildReport report)
    {
        if (report.summary.platform != BuildTarget.iOS)
            return;
        
        _outputPath  = report.summary.outputPath;

        SetPaths();

        if (string.IsNullOrEmpty(_pluginFilesPath))
        {
            Debug.LogError("Could not find valid Reteno plugin path.");
            return;
        }

        EnableRemoteNotification(_outputPath);

        _projectPath = PBXProject.GetPBXProjectPath(report.summary.outputPath);
        _project = new PBXProject();
        _project.ReadFromFile(_projectPath);

        string mainTargetGUID = _project.GetUnityMainTargetGuid();
        string frameworkTargetGUID = _project.GetUnityFrameworkTargetGuid();
        
        AddProjectCapabilities();

        // Add Bridging Header
        string bridgingHeaderPath = Path.Combine(_pluginFilesPath, "Reteno-Bridging-Header.h");
        
        _project.SetBuildProperty(mainTargetGUID, "SWIFT_OBJC_BRIDGING_HEADER", bridgingHeaderPath);
        _project.SetBuildProperty(mainTargetGUID, "SWIFT_OBJC_INTERFACE_HEADER_NAME", "Reteno-Bridging-Header.h");

        // Add UserNotifications framework
        _project.AddFrameworkToProject(frameworkTargetGUID, "UserNotifications.framework", false);

        // Set Swift version
        _project.SetBuildProperty(frameworkTargetGUID, "SWIFT_VERSION", "5.0");

        AddNotificationServiceExtension();
        
        DisableBitcode();

        // Save project file
        File.WriteAllText(_projectPath, _project.WriteToString());
    }

    private void EnableRemoteNotification(string path)
    {
        string preprocessorPath = path + "/Classes/Preprocessor.h";
        string text = File.ReadAllText(preprocessorPath);
        text = text.Replace("UNITY_USES_REMOTE_NOTIFICATIONS 0", "UNITY_USES_REMOTE_NOTIFICATIONS 1");
        File.WriteAllText(preprocessorPath, text);
    }
    
    private void SetPaths()
    {
        foreach (var basePath in PossiblePaths)
        {
            if (Directory.Exists(basePath))
            {
                _editorFilesPath = Path.Combine(basePath, "Editor");
                _pluginLibrariesPath = Path.Combine(basePath, "Runtime", "Plugins", "iOS");
                _pluginFilesPath = Path.Combine(_pluginLibrariesPath);
                return;
            }
        }

        Debug.LogError("Could not find valid Reteno plugin path.");
    }
    
    /// <summary>
    /// Add the required capabilities and entitlements for OneSignal
    /// </summary>
    private void AddProjectCapabilities()
    {
        var targetGuid = _project.GetMainTargetGuid();
        var targetName = _project.GetMainTargetName();

        var entitlementsPath = GetEntitlementsPath(targetGuid, targetName);
        var projCapability = new ProjectCapabilityManager(_projectPath, entitlementsPath, targetName);

        projCapability.AddBackgroundModes(BackgroundModesOptions.RemoteNotifications);
        projCapability.AddBackgroundModes(BackgroundModesOptions.BackgroundFetch);
        projCapability.AddBackgroundModes(BackgroundModesOptions.Processing);
        projCapability.AddPushNotifications(false);
        projCapability.AddAppGroups(new[] { _appGroupName });
            
        projCapability.WriteToFile();
    }
    
    /// <summary>
    /// Get existing entitlements file if exists or creates a new file, adds it to the project, and returns the path
    /// </summary>
    private string GetEntitlementsPath(string targetGuid, string targetName) {
        var relativePath = _project.GetBuildPropertyForAnyConfig(targetGuid, "CODE_SIGN_ENTITLEMENTS");

        if (relativePath != null) {
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
    
    private void DisableBitcode()
    {
        // Main
        var targetGuid = _project.GetUnityMainTargetGuid();
        _project.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");

        // Unity Tests
        var unityTests = _project.TargetGuidByName(PBXProject.GetUnityTestTargetName());
        _project.SetBuildProperty(unityTests, "ENABLE_BITCODE", "NO");

        // Unity Framework
        var unityFramework = _project.GetUnityFrameworkTargetGuid();
        _project.SetBuildProperty(unityFramework, "ENABLE_BITCODE", "NO");
    }
    
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
                $"$(PROJECT_DIR)/Libraries/{_pluginLibrariesPath.Replace("\\", "/")}");

            _project.WriteToFile(_projectPath);

            // add capabilities + entitlements
            var entitlementsPath = GetEntitlementsPath(extensionGuid, ServiceExtensionTargetName);
            var projCapability = new ProjectCapabilityManager(_projectPath, entitlementsPath, ServiceExtensionTargetName);
            
            projCapability.AddAppGroups(new[] { _appGroupName });
            
            projCapability.WriteToFile();
#endif
        }
    
    private void ExtensionAddPodsToTarget() 
    {
        var podfilePath = Path.Combine(_outputPath, "Podfile");

        if (!File.Exists(podfilePath)) {
            Debug.LogError($"Could not find Podfile. {ServiceExtensionFilename} will have errors.");
            return;
        }

        var dependenciesFilePath = Path.Combine(_editorFilesPath, DependenciesFilename);

        if (!File.Exists(dependenciesFilePath)) {
            Debug.LogError($"Could not find {DependenciesFilename}");
            return;
        }
        
        var dependenciesFile = File.ReadAllText(dependenciesFilePath);
        var dependenciesRegex = new Regex("(?<=<iosPod name=\"Reteno\" version=\").+(?=\" addToAllTargets=\"false\" />)");

        if (!dependenciesRegex.IsMatch(dependenciesFile)) {
            Debug.LogError($"Could not read current iOS framework dependency version from {DependenciesFilename}");
            return;
        }

        var podfile = File.ReadAllText(podfilePath);
        var podfileRegex = new Regex($@"target '{ServiceExtensionTargetName}' do\n  pod 'Reteno', '(.+)'\nend\n");
        
        var requiredVersion = dependenciesRegex.Match(dependenciesFile).ToString();
        var requiredTarget = $"target '{ServiceExtensionTargetName}' do\n  pod 'Reteno', '{requiredVersion}'\nend\n";

        if (!podfileRegex.IsMatch(podfile))
            podfile += requiredTarget;
        else {
            var podfileTarget = podfileRegex.Match(podfile).ToString();
            podfile = podfile.Replace(podfileTarget, requiredTarget);
        }
        
        File.WriteAllText(podfilePath, podfile);
    }
    
    /// <summary>
    /// Add the swift source file required by the notification extension
    /// </summary>
    private void ExtensionAddSourceFiles(string extensionGuid) {
        var buildPhaseID     = _project.AddSourcesBuildPhase(extensionGuid);
        var sourcePath       = Path.Combine(_pluginFilesPath, ServiceExtensionFilename);
        var destPathRelative = Path.Combine(ServiceExtensionTargetName, ServiceExtensionFilename);

        var destPath = Path.Combine(_outputPath, destPathRelative);
        if (!File.Exists(destPath))
            FileUtil.CopyFileOrDirectory(sourcePath.Replace("\\", "/"), destPath.Replace("\\", "/"));

        var sourceFileGuid = _project.AddFile(destPathRelative, destPathRelative);
        _project.AddFileToBuildSection(extensionGuid, buildPhaseID, sourceFileGuid);
    }


    /// <summary>
    /// Create a .plist file for the extension
    /// </summary>
    /// <remarks>NOTE: File in Xcode project is replaced everytime, never appends</remarks>
    private bool ExtensionCreatePlist(string path) {
        var extensionPath = Path.Combine(path, ServiceExtensionTargetName);
        Directory.CreateDirectory(extensionPath);

        var plistPath     = Path.Combine(extensionPath, "Info.plist");
        var alreadyExists = File.Exists(plistPath);

        var notificationServicePlist = new PlistDocument();
        notificationServicePlist.ReadFromFile(Path.Combine(_pluginFilesPath, "Info.plist"));
        notificationServicePlist.root.SetString("CFBundleShortVersionString", PlayerSettings.bundleVersion);
        notificationServicePlist.root.SetString("CFBundleVersion", PlayerSettings.iOS.buildNumber);

        notificationServicePlist.WriteToFile(plistPath);
            
        return alreadyExists;
    }
}
}

#endif
