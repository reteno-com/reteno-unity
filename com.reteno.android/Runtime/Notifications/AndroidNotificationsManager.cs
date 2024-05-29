using Reteno.Android.Utilities;
using Reteno.Notifications;
using UnityEngine;

namespace Reteno.Android
{
    public class AndroidNotificationsManager : INotificationsManager
    {
        public void SetNotificationCustomDataListener(RetenoCustomDataListener listener)
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
    }
}