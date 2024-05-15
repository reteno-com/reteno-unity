using System.Threading;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Reteno
{
    /// <summary>
    ///     Helper class for ensuring a callback is invoked on the main Unity thread
    /// </summary>
    public static class UnityMainThreadDispatch
    {
        private static SynchronizationContext _context;

        /// <summary>
        ///     Synchronous; blocks until the callback completes
        /// </summary>
        public static void Send(SendOrPostCallback callback)
        {
            _context.Send(callback, null);
        }

        /// <summary>
        ///     Asynchronous; send and forget
        /// </summary>
        public static void Post(SendOrPostCallback callback)
        {
            _context.Post(callback, null);
        }

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
#endif
        [RuntimeInitializeOnLoadMethod]
        private static void _initialize() => _context = _context ?? SynchronizationContext.Current;
    }
}