using Reteno.Notifications;

namespace Reteno.iOS.Notifications
{
    internal sealed class iOSNotificationsManager : INotificationsManager
    {
        private static iOSNotificationsManager _instance;

        public iOSNotificationsManager()
        {
            _instance = this;
        }

        public void Initialize()
        {
        }

        public void SetNotificationCustomDataListener(RetenoCustomDataListener listener)
        {
            
        }
    }
}