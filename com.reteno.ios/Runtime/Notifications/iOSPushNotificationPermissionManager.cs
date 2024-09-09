using System;
using System.Runtime.InteropServices;
using AOT;
using Reteno.Notifications;

namespace Reteno.iOS.Notifications
{
    public class iOSPushNotificationPermissionManager : IPushNotificationPermissionManager
    {
        private static iOSPushNotificationPermissionManager currentInstance;

        [DllImport("__Internal")]
        private static extern void registerForNotifications();
      
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        private static extern void notificationsAddPermissionCallback(PermissionCallback permissionCallback);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        private static extern void registerNotificationCallback(NotificationCallback dataCallback);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PermissionCallback(bool granted);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void NotificationCallback(string jsonData);
        
        private static PermissionCallback storedPermissionCallback;
        private static NotificationCallback storedDataCallback;

        private Action onPermissionGranted;
        private Action onPermissionDenied;
        private Action onPermissionDeniedAndDontAskAgain;
        private Action<string> onDataReceived;

        public iOSPushNotificationPermissionManager()
        {
            currentInstance = this;  // Set the current instance
        }
        
        public void Initialize()
        {
            RequestPermission();
        }

        public void RequestPush(Action onPermissionGranted = null, Action onPermissionDenied = null,
            Action onPermissionDeniedAndDontAskAgain = null, Action<string> onDataReceived = null)
        {
            this.onPermissionGranted = onPermissionGranted;
            this.onPermissionDenied = onPermissionDenied;
            this.onPermissionDeniedAndDontAskAgain = onPermissionDeniedAndDontAskAgain;
            this.onDataReceived = onDataReceived;
            
            // Registering the notifications
            registerForNotifications();
        }
        
        private void RequestPermission()
        {
            PermissionCallback permissionCallbackDelegate = PermissionCallbackHandler;
            NotificationCallback dataCallbackDelegate = DataCallbackHandler;

            notificationsAddPermissionCallback(permissionCallbackDelegate);
            registerNotificationCallback(dataCallbackDelegate);

            storedPermissionCallback = permissionCallbackDelegate; // Store the callback to prevent it from being garbage-collected
            storedDataCallback = dataCallbackDelegate;
        }

        [MonoPInvokeCallback(typeof(PermissionCallback))]
        private static void PermissionCallbackHandler(bool granted)
        {
            if (granted)
                currentInstance.onPermissionGranted?.Invoke();
            else
                currentInstance.onPermissionDenied?.Invoke();
        }

        [MonoPInvokeCallback(typeof(NotificationCallback))]
        private static void DataCallbackHandler(string jsonData)
        {
            currentInstance.onDataReceived?.Invoke(jsonData);
        }

        public void UpdatePushPermissionStatus()
        {
            // Update or check the current status of the push notifications
        }
    }
}
