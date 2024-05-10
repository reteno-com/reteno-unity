#if UNITY_IOS
using UnityEditor.iOS.Xcode;

namespace RetenoSDK.iOS
{
    public static class PBXProjectExtensions
    {
#if UNITY_2019_3_OR_NEWER
        public static string GetMainTargetName(this PBXProject project) 
            => "Unity-iPhone";

        public static string GetMainTargetGuid(this PBXProject project) 
            => project.GetUnityMainTargetGuid();
#else
        public static string GetMainTargetName(this PBXProject project) 
            => PBXProject.GetUnityTargetName();
         
        public static string GetMainTargetGuid(this PBXProject project)
             => project.TargetGuidByName(PBXProject.GetUnityTargetName());
#endif
    }
}
#endif