namespace Reteno.Notifications
{
    public interface INotificationsManager
    {
        void Initialize();
        void SetNotificationCustomDataListener(RetenoCustomDataListener listener);
    }
}