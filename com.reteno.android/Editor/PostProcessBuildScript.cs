using System;
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
                UnityEngine.Debug.Log("Post-process build for Android platform.");
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
                implementation 'com.reteno:fcm:2.0.16'
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
                        UnityEngine.Debug.Log("Gradle build file modified with new dependencies.");
                    }
                    else
                    {
                        UnityEngine.Debug.LogWarning("Could not find the end of the dependencies block.");
                    }
                }
                else
                {
                    UnityEngine.Debug.LogWarning("Could not find the dependencies block.");
                }
            }
            else
            {
                UnityEngine.Debug.LogWarning($"Gradle build file not found at path: {gradleBuildFilePath}");
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
                // Add application name attribute
                XmlAttribute nameAttribute = manifestDoc.CreateAttribute("android", "name", "http://schemas.android.com/apk/res/android");
                nameAttribute.Value = "com.reteno.unity.RetenoApp";
                applicationNode.Attributes.Append(nameAttribute);

                // Add RetenoFirebaseListenerService
                XmlElement serviceElement = manifestDoc.CreateElement("service");
                XmlAttribute serviceNameAttribute = manifestDoc.CreateAttribute("android", "name", "http://schemas.android.com/apk/res/android");
                serviceNameAttribute.Value = "com.reteno.unity.RetenoFirebaseListenerService";
                serviceElement.Attributes.Append(serviceNameAttribute);

                XmlAttribute serviceExportedAttribute = manifestDoc.CreateAttribute("android", "exported", "http://schemas.android.com/apk/res/android");
                serviceExportedAttribute.Value = "false";
                serviceElement.Attributes.Append(serviceExportedAttribute);

                XmlElement intentFilterElement = manifestDoc.CreateElement("intent-filter");
                XmlElement actionElement = manifestDoc.CreateElement("action");
                XmlAttribute actionNameAttribute = manifestDoc.CreateAttribute("android", "name", "http://schemas.android.com/apk/res/android");
                actionNameAttribute.Value = "com.google.firebase.MESSAGING_EVENT";
                actionElement.Attributes.Append(actionNameAttribute);

                intentFilterElement.AppendChild(actionElement);
                serviceElement.AppendChild(intentFilterElement);
                applicationNode.AppendChild(serviceElement);

                manifestDoc.Save(manifestPath);
                UnityEngine.Debug.Log("Android manifest file modified with new application name attribute and service.");
            }
            else
            {
                UnityEngine.Debug.LogWarning("Application node not found in the Android manifest file.");
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("Manifest node not found in the Android manifest file.");
        }
    }
    else
    {
        UnityEngine.Debug.LogWarning($"Android manifest file not found at path: {manifestPath}");
    }
}

    }
}
