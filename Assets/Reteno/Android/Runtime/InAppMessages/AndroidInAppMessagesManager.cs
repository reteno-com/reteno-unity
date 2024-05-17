using Reteno.InAppMessages;
using UnityEngine;

namespace Reteno.Android.InAppMessages
{
    public class AndroidInAppMessagesManager : IInAppMessagesManager
    {
        public void PauseInAppMessages(bool isPaused)
        {
            AndroidJavaObject reteno = RetenoJavaInstance.Get();
            reteno.Call("pauseInAppMessages", isPaused);
        }
        
        public void SetInAppMessagesPauseBehaviour(InAppPauseBehaviour behaviour)
        {
            AndroidJavaObject reteno = RetenoJavaInstance.Get();
            //TODO WIP
            //reteno.Call("setInAppMessagesPauseBehaviour", behaviour);
        }

        public void SetInAppMessageCustomDataListener(RetenoCustomDataListener listener)
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