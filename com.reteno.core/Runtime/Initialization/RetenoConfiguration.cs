namespace Reteno.Core.Initialization
{
    public class RetenoConfiguration
    {
        public bool IsAutomaticScreenReportingEnabled { get; set; }
        public bool IsAutomaticAppLifecycleReportingEnabled { get; set; }
        public bool IsAutomaticPushSubscriptionReportingEnabled { get; set; }
        public bool IsAutomaticSessionReportingEnabled { get; set; }
        public bool IsPausedInAppMessages { get; set; }
        public int InAppMessagesPauseBehaviour { get; set; }
        public bool IsDebugMode { get; set; }
        public PushNotificationProvider PushNotificationProvider { get; set; }

        public RetenoConfiguration(bool isAutomaticScreenReportingEnabled = false,
            bool isAutomaticAppLifecycleReportingEnabled = true,
            bool isAutomaticPushSubscriptionReportingEnabled = true,
            bool isAutomaticSessionReportingEnabled = true,
            bool isPausedInAppMessages = false,
            int inAppMessagesPauseBehaviour = 0,
            bool isDebugMode = false,
            PushNotificationProvider pushNotificationProvider = PushNotificationProvider.Fcm)
        {
            IsAutomaticScreenReportingEnabled = isAutomaticScreenReportingEnabled;
            IsAutomaticAppLifecycleReportingEnabled = isAutomaticAppLifecycleReportingEnabled;
            IsAutomaticPushSubscriptionReportingEnabled = isAutomaticPushSubscriptionReportingEnabled;
            IsAutomaticSessionReportingEnabled = isAutomaticSessionReportingEnabled;
            IsPausedInAppMessages = isPausedInAppMessages;
            InAppMessagesPauseBehaviour = inAppMessagesPauseBehaviour;
            IsDebugMode = isDebugMode;
            PushNotificationProvider = pushNotificationProvider;
        }
    }
}