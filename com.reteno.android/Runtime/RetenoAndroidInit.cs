#if UNITY_ANDROID
using Reteno.Core;
using UnityEngine;

namespace Reteno.Android
{
    public sealed class RetenoAndroidInit
    {
        internal static class RetenoOSInit
        {
            [RuntimeInitializeOnLoadMethod]
            public static void Init()
            {
                if (!RetenoPlatform.DidInitialize)
                    RetenoSDK.Platform = new RetenoAndroid();
            }
        }
    }
}
#endif