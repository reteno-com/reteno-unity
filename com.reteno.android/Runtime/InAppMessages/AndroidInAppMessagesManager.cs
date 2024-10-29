using System;
using System.Collections.Generic;
using Reteno.Android.Utilities;
using Reteno.InAppMessages;
using UnityEngine;

namespace Reteno.Android.InAppMessages
{
    /// <summary>
    /// Manages the inApp Messages
    /// </summary>
    public class AndroidInAppMessagesManager : IInAppMessagesManager
    {
        public event Action<Dictionary<string, string>> CustomData;

        private IRetenoCustomDataListener _retenoCustomDataListener;

        public void Initialize()
        {
            _retenoCustomDataListener = new RetenoCustomDataListener();
            _retenoCustomDataListener.CustomData += RetenoCustomDataListenerOnCustomData;

            SetInAppMessageCustomDataListener(_retenoCustomDataListener);

            void RetenoCustomDataListenerOnCustomData(Dictionary<string, string> customData)
            {
                CustomData?.Invoke(customData);
            }
        }

        public void PauseInAppMessages(bool isPaused)
        {
            AndroidJavaObject reteno = RetenoJavaInstance.Get();
            reteno.Call("pauseInAppMessages", isPaused);
        }
        
        public void SetInAppMessagesPauseBehaviour(InAppPauseBehaviour behaviour)
        {
            AndroidJavaObject reteno = RetenoJavaInstance.Get();
            
            AndroidJavaObject enumObj = new AndroidJavaObject("com.reteno.core.features.iam.InAppPauseBehaviour");
            AndroidJavaClass enumValue = enumObj.CallStatic<AndroidJavaClass>("valueOf");
            reteno.Call("setPushInAppMessagesPauseBehaviour", enumValue);
        }

        private void SetInAppMessageCustomDataListener(IRetenoCustomDataListener listener)
        {
            AndroidJavaObject customDataHandler = RetenoJavaInstance.GetCustomDataHandler();
            if (listener == null)
            {
                customDataHandler.Call("setNotificationCustomDataReceiver", null);
            }
            else
            {
                customDataHandler.Call("setNotificationCustomDataReceiver", new CustomDataProxy(listener));
            }
        }
    }
}