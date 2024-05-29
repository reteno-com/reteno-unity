using System;
using Reteno.Debug;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.IO;
using System.Xml;

namespace Reteno.Android.Editor
{
    public class PostProcessBuildScript : IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPostprocessBuild(BuildReport report)
        {
            if (report.summary.platform == BuildTarget.Android)
            {
                SDKDebug.Info("Post-process build for Android platform.");
                string buildPath = report.summary.outputPath;
                ModifyGradleBuildFiles(buildPath);
                ModifyAndroidManifest(buildPath);
            }
        }

        private void ModifyGradleBuildFiles(string buildPath)
        {
            // Find the Gradle build file in the build path
            string gradleBuildFilePath = Path.Combine(buildPath, "unityLibrary/build.gradle");

            if (File.Exists(gradleBuildFilePath))
            {
                string gradleContent = File.ReadAllText(gradleBuildFilePath);

                // Add the dependencies
                string dependenciesToAdd = @"
                implementation 'androidx.work:work-runtime-ktx:2.8.1'
                implementation 'androidx.cardview:cardview:1.0.0'
                implementation 'com.reteno:fcm:2.0.6'
                api 'com.google.code.gson:gson:2.10.1'
                ";

                // Find the dependencies block and append the new dependencies
                int dependenciesIndex = gradleContent.IndexOf("dependencies {", StringComparison.Ordinal);
                if (dependenciesIndex >= 0)
                {
                    int endOfDependenciesIndex = gradleContent.IndexOf('}', dependenciesIndex);
                    if (endOfDependenciesIndex >= 0)
                    {
                        gradleContent = gradleContent.Insert(endOfDependenciesIndex, dependenciesToAdd);
                        File.WriteAllText(gradleBuildFilePath, gradleContent);
                        SDKDebug.Info("Gradle build file modified with new dependencies.");
                    }
                    else
                    {
                        SDKDebug.Warn("Could not find the end of the dependencies block.");
                    }
                }
                else
                {
                    SDKDebug.Warn("Could not find the dependencies block.");
                }
            }
            else
            {
                SDKDebug.Warn($"Gradle build file not found at path: {gradleBuildFilePath}");
            }
        }

        private void ModifyAndroidManifest(string buildPath)
        {
            // Find the Android manifest file in the build path
            string manifestPath = Path.Combine(buildPath, "unityLibrary/src/main/AndroidManifest.xml");
            if (File.Exists(manifestPath))
            {
                XmlDocument manifestDoc = new XmlDocument();
                manifestDoc.Load(manifestPath);

                XmlNode manifestNode = manifestDoc.SelectSingleNode("/manifest");
                if (manifestNode != null)
                {
                    XmlNode applicationNode = manifestNode.SelectSingleNode("application");
                    if (applicationNode != null)
                    {
                        XmlAttribute nameAttribute = manifestDoc.CreateAttribute("android", "name", "http://schemas.android.com/apk/res/android");
                        nameAttribute.Value = "com.reteno.unity.RetenoApp";
                        applicationNode.Attributes.Append(nameAttribute);

                        manifestDoc.Save(manifestPath);
                        SDKDebug.Info("Android manifest file modified with new application name attribute.");
                    }
                    else
                    {
                        SDKDebug.Warn("Application node not found in the Android manifest file.");
                    }
                }
                else
                {
                    SDKDebug.Warn("Manifest node not found in the Android manifest file.");
                }
            }
            else
            {
                SDKDebug.Warn($"Android manifest file not found at path: {manifestPath}");
            }
        }
    }
}