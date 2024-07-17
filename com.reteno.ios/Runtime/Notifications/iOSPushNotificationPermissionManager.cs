using System;
using System.Runtime.InteropServices;
using Reteno.Notifications;

namespace Reteno.iOS.Notifications
{
    public class iOSPushNotificationPermissionManager : IPushNotificationPermissionManager
    {
        [DllImport("__Internal")]
        private static extern void registerForNotifications();
        
        private Action _onPermissionGranted;
        private Action _onPermissionDenied;
        
        public void RequestPush(Action onPermissionGranted = null, Action onPermissionDenied = null,
            Action onPermissionDeniedAndDontAskAgain = null)
        {
            _onPermissionGranted = onPermissionGranted;
            _onPermissionDenied = onPermissionDenied;

            registerForNotifications();
        }

        public void ChangePermissionStatus(string status)
        {
            bool isGranted = status == "true";
            if(isGranted)
                _onPermissionGranted?.Invoke();
            else
                _onPermissionDenied?.Invoke();
        }

        public void UpdatePushPermissionStatus()
        {
        }
    }
}