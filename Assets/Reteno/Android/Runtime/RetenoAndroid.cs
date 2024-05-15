using Reteno.Notifications;
using Reteno.User;

namespace Reteno.Android
{
    public class RetenoAndroid : RetenoPlatform
    {
        public override INotificationsManager Notifications { get; }
        public override IUserManager UserManager { get; }

        public override void Initialize(string appId)
        {
           
        }
    }
}