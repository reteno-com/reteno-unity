#if UNITY_IOS && !UNITY_EDITOR 
using UnityEngine;

namespace RetenoSDK.iOS {
    /// <summary>
    /// 
    /// </summary>
    internal static class RetenoOSInit {
        [RuntimeInitializeOnLoadMethod] public static void Init() {
            if (!RetenoPlatform.DidInitialize)
                Reteno.Platform = new RetenoiOS();
        }
    }
}
#endif