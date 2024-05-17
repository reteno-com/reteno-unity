using Reteno.Android.Events;
using Reteno.Android.InAppMessages;
using Reteno.Android.User;
using Reteno.Debug;
using Reteno.Events;
using Reteno.InAppMessages;
using Reteno.Notifications;
using Reteno.User;
using UnityEngine;

namespace Reteno.Android
{
    public partial class RetenoAndroid : RetenoPlatform
    {
        private static RetenoAndroid _instance;

        public override INotificationsManager Notifications => _androidNotifications;
        public override IUserManager UserManager => _androidUserManager;
        public override IInAppMessagesManager InAppMessagesManager => _androidInAppMessages;
        public override IEventManager EventManager => _androidEventManager;

        private AndroidNotificationsManager _androidNotifications;
        private AndroidUserManager _androidUserManager;
        private AndroidInAppMessagesManager _androidInAppMessages;
        private AndroidEventManager _androidEventManager;

        public override void Initialize(string appId)
        {
            AndroidJavaObject lifecycleTrackingOptionsJava = new AndroidJavaObject(
                "com.reteno.core.domain.model.event.LifecycleTrackingOptions",
                true,
                true,
                true
            );
            
             AndroidJavaObject configJava = new AndroidJavaObject(
                "com.reteno.core.RetenoConfig",
                false,
                null,
                lifecycleTrackingOptionsJava,
                appId
            );
            
           RetenoJavaInstance.Get().Call("initWith", configJava);
           
           if (_androidNotifications == null)
           {
               _androidNotifications = new AndroidNotificationsManager();
               _androidNotifications.Initialize();
           }
            
           _androidUserManager ??= new AndroidUserManager();
           _androidInAppMessages ??= new AndroidInAppMessagesManager();
           _androidEventManager ??= new AndroidEventManager();
        }
        
        public RetenoAndroid()
        {
            if (_instance != null)
                SDKDebug.Error("Additional instance of Reteno created.");

            _instance = this;
        }
    }
}