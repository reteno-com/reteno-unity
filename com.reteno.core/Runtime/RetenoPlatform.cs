using System;
using Reteno.Events;
using Reteno.InAppMessages;
using Reteno.Notifications;
using Reteno.Users;

namespace Reteno.Core
{
    public abstract class RetenoPlatform
    {
        internal static string AppId { get; private set; }
        public static bool DidInitialize { get; private set; }

        public abstract INotificationsManager Notifications { get; }
        public abstract IUserManager UserManager { get; }
        public abstract IPushNotificationPermissionManager PushAndroidPushNotificationPermissionManagerManager { get; }
        public abstract IInAppMessagesManager InAppMessagesManager { get; }
        public abstract IEventManager EventManager { get; }

        internal static event Action<string> OnInitialize;

        protected static void _completedInit(string appId)
        {
            AppId = appId;
            DidInitialize = true;
            OnInitialize?.Invoke(AppId);
        }

        protected static void _init(string appId)
        {
            OnInitialize?.Invoke(appId);
        }
        
        public abstract void Initialize(string appId);
    }
}