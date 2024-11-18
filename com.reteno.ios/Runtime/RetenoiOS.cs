using System.Runtime.InteropServices;

using Reteno.Core;
using Reteno.Core.Initialization;
using Reteno.Debug;
using Reteno.Events;
using Reteno.InAppMessages;
using Reteno.iOS.Events;
using Reteno.iOS.InAppMessages;
using Reteno.iOS.Notifications;
using Reteno.iOS.Users;
using Reteno.Notifications;
using Reteno.Users;

namespace Reteno.iOS
{
    public class RetenoiOS : RetenoPlatform
    {
        private static RetenoiOS _instance;

        [DllImport("__Internal")]
        private static extern void startRetenoWithConfiguration(string apiKey,
            bool isAutomaticScreenReportingEnabled,
            bool isAutomaticAppLifecycleReportingEnabled,
            bool isAutomaticPushSubscriptionReportingEnabled,
            bool isAutomaticSessionReportingEnabled,
            bool isPausedInAppMessages,
            int inAppMessagesPauseBehaviour,
            bool isDebugMode,
            string pushNotificationProvider); 

        public override INotificationsManager Notifications => _iOSNotificationsManager;
        public override IUserManager UserManager => _iOSUserManager;
        public override IPushNotificationPermissionManager PushNotificationPermissionManager => _iOSPushNotificationPermissionManager;
        public override IInAppMessagesManager InAppMessagesManager => _iOSInAppMessages;
        public override IEventManager EventManager => _iOSEventManager;

        private iOSNotificationsManager _iOSNotificationsManager;
        private iOSUserManager _iOSUserManager;
        private iOSInAppMessagesManager _iOSInAppMessages;
        private iOSEventManager _iOSEventManager;
        private iOSPushNotificationPermissionManager _iOSPushNotificationPermissionManager;
    
        public override void Initialize(string appId, RetenoConfiguration retenoConfiguration = null)
        {
            SDKDebug.Info(nameof(Initialize) + "iOS platform");

            retenoConfiguration ??= new RetenoConfiguration();
            
            startRetenoWithConfiguration(
                appId, 
                retenoConfiguration.IsAutomaticScreenReportingEnabled,
                retenoConfiguration.IsAutomaticAppLifecycleReportingEnabled,
                retenoConfiguration.IsAutomaticPushSubscriptionReportingEnabled,
                retenoConfiguration.IsAutomaticSessionReportingEnabled,
                retenoConfiguration.IsPausedInAppMessages,
                retenoConfiguration.InAppMessagesPauseBehaviour,
                retenoConfiguration.IsDebugMode,
                retenoConfiguration.PushNotificationProvider.ToString().ToLower());

            _iOSNotificationsManager = new iOSNotificationsManager();
            _iOSUserManager = new iOSUserManager();
            _iOSInAppMessages = new iOSInAppMessagesManager();
            _iOSInAppMessages.Initialize();

            _iOSEventManager = new iOSEventManager();
            _iOSPushNotificationPermissionManager = new iOSPushNotificationPermissionManager();
            _iOSPushNotificationPermissionManager.Initialize();
        
            SDKDebug.Info( "End init");
        }
    
        public RetenoiOS()
        {
            if (_instance != null)
                SDKDebug.Error("Additional instance of Reteno created.");

            _instance = this;
        }
    }
}
