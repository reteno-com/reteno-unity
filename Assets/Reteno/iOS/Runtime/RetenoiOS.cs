using System.Runtime.InteropServices;
using Reteno.Debug;
using Reteno.iOS.Notifications;
using Reteno.iOS.User;
using Reteno.Notifications;
using Reteno.User;

namespace Reteno.iOS
{
    public sealed partial class RetenoiOS : RetenoPlatform
    {
        public override INotificationsManager Notifications => _notifications;
        public override IUserManager UserManager => _userManager;

        private static RetenoiOS _instance;
        private iOSNotificationsManager _notifications;
        private iOSUserManager _userManager;

        public RetenoiOS()
        {
            if (_instance != null)
                SDKDebug.Error("Additional instance of Reteno created.");

            _instance = this;
        }

        [DllImport("__Internal")]
        private static extern void startReteno(string apiKey, bool debugMode);

        [DllImport("__Internal")]
        private static extern void registerForNotifications();

        [DllImport("__Internal")]
        private static extern void configureFirebase();

        public override void Initialize(string appId)
        {
            startReteno(appId, true);
            
            if (_notifications == null)
            {
                _notifications = new iOSNotificationsManager();
                _notifications.Initialize();
            }

            RegisterNotifications();

            _completedInit(appId);
        }
        
        public void RegisterNotifications() {
            registerForNotifications();
        }

        public void SetupFirebase() {
            configureFirebase();
        }
    }
}