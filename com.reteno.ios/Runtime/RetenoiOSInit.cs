#if UNITY_IOS
using Reteno.Core;
using UnityEngine;

namespace Reteno.iOS
{
    internal static class RetenoiOSInit 
    {
        [RuntimeInitializeOnLoadMethod] 
        public static void Init() 
        {
            if (!RetenoPlatform.DidInitialize)
                RetenoSDK.Platform = new RetenoiOS();
        }
    }
}
#endif
