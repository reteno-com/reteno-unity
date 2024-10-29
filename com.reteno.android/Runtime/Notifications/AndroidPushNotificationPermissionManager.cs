using System;
using System.Collections.Generic;
using Reteno.Android;
using Reteno.Android.Utilities;
using Reteno.Utilities;
using UnityEngine;
using UnityEngine.Android;

namespace Reteno.Notifications
{
    // <summary>
    /// Manages the push notification permission request and handles the callbacks.
    /// </summary>
    public class AndroidPushNotificationPermissionManager : IPushNotificationPermissionManager
    {
        public event Action<Dictionary<string, string>> CustomData;

        private IRetenoCustomDataListener _retenoCustomDataListener;

        private Action<string> _onDataReceived;

        public void Initialize()
        {
            _retenoCustomDataListener = new RetenoCustomDataListener();
            _retenoCustomDataListener.CustomData += RetenoCustomDataListenerOnCustomData;

            SetNotificationCustomDataListener(_retenoCustomDataListener);

            void RetenoCustomDataListenerOnCustomData(Dictionary<string, string> customData)
            {
                CustomData?.Invoke(customData);

                if (_onDataReceived != null)
                {
                    DictionaryWrapper wrapper = new DictionaryWrapper(customData);
                    string serializedData = JsonUtility.ToJson(wrapper);
                    _onDataReceived.Invoke(serializedData);
                }
            }
        }

        public void RequestPush(Action onPermissionGranted = null, 
            Action onPermissionDenied = null, Action onPermissionDeniedAndDontAskAgain = null,
            Action<string> onDataReceived = null)
        {
            var callbacks = new PermissionCallbacks();
            callbacks.PermissionGranted += CallbacksOnPermissionGranted;
            callbacks.PermissionDenied += CallbacksOnPermissionDenied;
            callbacks.PermissionDeniedAndDontAskAgain += CallbacksOnPermissionDeniedAndDontAskAgain;

            _onDataReceived = onDataReceived;

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
        
        private void SetNotificationCustomDataListener(IRetenoCustomDataListener listener)
        {
            AndroidJavaObject customDataHandler = RetenoJavaInstance.GetCustomDataHandler();
            if (listener == null)
            {
                customDataHandler.Call("setNotificationCustomDataReceiver", null);
            } else
            {
                customDataHandler.Call("setNotificationCustomDataReceiver", new CustomDataProxy(listener));
            }
        }
    
        public void UpdatePushPermissionStatus()
        {
            RetenoJavaInstance.Get().Call("updatePushPermissionStatus");
        }
    }
}