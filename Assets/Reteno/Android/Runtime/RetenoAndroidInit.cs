#if UNITY_ANDROID
using UnityEngine;

namespace Reteno.Android
{
    public class RetenoAndroidInit
    {
        internal static class RetenoOSInit
        {
            [RuntimeInitializeOnLoadMethod]
            public static void Init()
            {
                if (!RetenoPlatform.DidInitialize)
                    Reteno.Platform = new RetenoAndroid();
            }
        }
    }
}
#endif