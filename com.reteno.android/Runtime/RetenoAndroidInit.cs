#if UNITY_ANDROID
using Reteno.Core;
using UnityEngine;

namespace Reteno.Android
{
    internal static class RetenoAndroidInit
    {
        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            if (!RetenoPlatform.DidInitialize)
                RetenoSDK.Platform = new RetenoAndroid();
        }
    }
}
#endif