using System;

namespace Reteno.Notifications
{
    public interface IPushNotificationPermissionManager
    {
        /// <summary>
        /// Intialize pushNotificationPermissionManager
        /// </summary>
        void Initialize();
        
        /// <summary>
        /// Requests the user for push notification permissions and handles the callbacks.
        /// </summary>
        void RequestPush(Action onPermissionGranted = null,
            Action onPermissionDenied = null, Action onPermissionDeniedAndDontAskAgain = null,  Action<string> onDataReceived = null);

        /// <summary>
        /// Calls the native method to update push notification permission status.
        /// </summary>
        void UpdatePushPermissionStatus();
    }
}