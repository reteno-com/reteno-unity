using UnityEngine;

namespace Reteno
{
    public class ApplicationUtil
    {
        public static RuntimePlatform Platform
        {
            get
            {
#if UNITY_ANDROID
                return RuntimePlatform.Android;
#elif UNITY_IOS
                return RuntimePlatform.IPhonePlayer;
#endif

            }
        }
    }
}