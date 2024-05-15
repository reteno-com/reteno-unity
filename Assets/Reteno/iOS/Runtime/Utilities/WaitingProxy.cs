using System.Collections.Generic;
using Laters;

namespace Reteno.iOS.Utilities
{
    internal static class WaitingProxy
    {
        private static readonly Dictionary<int, ILater> WaitingProxies = new();

        public static (Later<TResult> proxy, int hashCode) _setupProxy<TResult>()
        {
            var proxy = new Later<TResult>();
            var hashCode = proxy.GetHashCode();
            WaitingProxies[hashCode] = proxy;
            return (proxy, hashCode);
        }

        public static void ResolveCallbackProxy<TResponse>(int hashCode, TResponse response)
        {
            if (!WaitingProxies.ContainsKey(hashCode))
                return;

            if (WaitingProxies[hashCode] is Later<TResponse> later)
                later.Complete(response);

            WaitingProxies.Remove(hashCode);
        }
    }
}