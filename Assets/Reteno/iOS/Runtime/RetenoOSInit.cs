#if UNITY_IOS
using UnityEngine;

namespace Reteno.iOS {
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