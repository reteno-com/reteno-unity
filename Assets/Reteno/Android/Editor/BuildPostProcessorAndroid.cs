using System.IO;
using UnityEngine;

using UnityEditor.Android;

namespace Reteno.Android
{
    public class BuildPostProcessorAndroid : IPostGenerateGradleAndroidProject
    {
        public int callbackOrder { get; }
        public void OnPostGenerateGradleAndroidProject(string path)
        {
            Debug.Log("Bulid path : " + path);
            string gradlePropertiesFile = path + "/build.gradle";
            if (File.Exists(gradlePropertiesFile))
            {
                File.Delete(gradlePropertiesFile);
            }
            
            //step 1
            //Find build.gradle
            //step 2
            //In file, find android
            //step 3
            //in file, find defaultConfig
            //step 3 
            //add this variable
            //buildConfigField "String", "RETENO_API_KEY", "some_key"
         
        }
    }
}