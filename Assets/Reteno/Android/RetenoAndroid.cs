using RetenoSDK;
using RetenoSDK.Notifications;
using RetenoSDK.User;

namespace Reteno.Android
{
    public class RetenoAndroid : RetenoPlatform
    {
        public override INotificationsManager Notifications { get; }
        public override IUserManager UserManager { get; }

        public override void Initialize(string appId)
        {
            throw new System.NotImplementedException();
        }
    }
}