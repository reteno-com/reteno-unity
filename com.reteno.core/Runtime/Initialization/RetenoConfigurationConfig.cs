using UnityEngine;

namespace Reteno.Core.Initialization
{
    [CreateAssetMenu(fileName = "RetenoConfiguration", menuName = "Configurations/RetenoConfiguration")]
    public class RetenoConfigurationConfig : ScriptableObject
    {
        public bool IsAutomaticScreenReportingEnabled;
        public bool IsAutomaticAppLifecycleReportingEnabled;
        public bool IsAutomaticPushSubscriptionReportingEnabled;
        public bool IsAutomaticSessionReportingEnabled;
        public bool IsPausedInAppMessages;
        public int InAppMessagesPauseBehaviour;
        public bool IsDebugMode;

        public RetenoConfiguration GetConfig()
        {
            return new RetenoConfiguration
            {
                IsAutomaticScreenReportingEnabled = this.IsAutomaticScreenReportingEnabled,
                IsAutomaticAppLifecycleReportingEnabled = this.IsAutomaticAppLifecycleReportingEnabled,
                IsAutomaticPushSubscriptionReportingEnabled = this.IsAutomaticPushSubscriptionReportingEnabled,
                IsAutomaticSessionReportingEnabled = this.IsAutomaticSessionReportingEnabled,
                IsPausedInAppMessages = this.IsPausedInAppMessages,
                InAppMessagesPauseBehaviour = this.InAppMessagesPauseBehaviour,
                IsDebugMode = this.IsDebugMode
            };
        }
    }
}