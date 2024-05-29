using UnityEngine;

namespace Reteno.Utilities
{
    public static class ApplicationUtil
    {
        private static RuntimePlatform Platform
        {
            get
            {
#if UNITY_ANDROID
                return RuntimePlatform.Android;
#elif UNITY_IOS
                return RuntimePlatform.IPhonePlayer;
#endif
                throw new UnityException("Platform doesnt support");
            }
        }
        
        public static string GetPlatformName()
        {
            return Platform switch
            {
                RuntimePlatform.Android => "Reteno.Android",
                RuntimePlatform.IPhonePlayer => "Reteno.iOS",
                _ => null
            };
        }

        public static string GetTypeName()
        {
            return Platform switch
            {
                RuntimePlatform.Android => "RetenoAndroid",
                RuntimePlatform.IPhonePlayer => "RetenoiOS",
                _ => null
            };
        }
    }
}