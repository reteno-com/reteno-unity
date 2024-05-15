using Reteno.InAppMessages;
using UnityEngine;

namespace Reteno.Android.InAppMessages
{
    public class InAppMessagesManager : IInAppMessagesManager
    {
        /// <summary>
        /// Change logic of In-App messages in paused state
        /// Default state is InAppPauseBehaviour.POSTPONE_IN_APPS
        /// </summary>
        /// <param name="isPaused">New pause value</param>
        public static void PauseInAppMessages(bool isPaused)
        {
            AndroidJavaObject reteno = RetenoJavaInstance.Get();
            reteno.Call("pauseInAppMessages", isPaused);
        }

        /// <summary>
        /// Pause or unpause In-App messages showing during app runtime.
        /// </summary>
        /// <param name="behaviour">new behaviour</param>
        public static void SetInAppMessagesPauseBehaviour(InAppPauseBehaviour behaviour)
        {
            AndroidJavaObject reteno = RetenoJavaInstance.Get();
            // TODO not implemented yet
            //reteno.Call("setInAppMessagesPauseBehaviour", behaviour);
        }

        public static void SetInAppMessageCustomDataListener(RetenoCustomDataListener listener)
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