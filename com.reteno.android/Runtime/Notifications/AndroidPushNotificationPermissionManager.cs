using System;
using Reteno.Android;
using UnityEngine.Android;

namespace Reteno.Notifications
{
    // <summary>
    /// Manages the push notification permission request and handles the callbacks.
    /// </summary>
    public class AndroidPushNotificationPermissionManager : IPushNotificationPermissionManager
    {
        public void Initialize()
        {
        }

        public void RequestPush(Action onPermissionGranted = null, 
            Action onPermissionDenied = null, Action onPermissionDeniedAndDontAskAgain = null, Action<string> onDataReceived = null)
        {
            var callbacks = new PermissionCallbacks();
            callbacks.PermissionGranted += CallbacksOnPermissionGranted;
            callbacks.PermissionDenied += CallbacksOnPermissionDenied;
            callbacks.PermissionDeniedAndDontAskAgain += CallbacksOnPermissionDeniedAndDontAskAgain;

            void CallbacksOnPermissionDenied(string permissionName)
            {
                onPermissionDenied?.Invoke();
            }

            void CallbacksOnPermissionDeniedAndDontAskAgain(string permissionName)
            {
                onPermissionDeniedAndDontAskAgain?.Invoke();
            }
            
            void CallbacksOnPermissionGranted(string permissionName)
            {
                UpdatePushPermissionStatus();
                onPermissionGranted?.Invoke();
            }

            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS", callbacks);
        }
        
    
        public void UpdatePushPermissionStatus()
        {
            RetenoJavaInstance.Get().Call("updatePushPermissionStatus");
        }
    }
}