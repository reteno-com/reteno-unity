using Reteno.Android.Events;
using Reteno.Android.InAppMessages;
using Reteno.Android.Users;
using Reteno.Core;
using Reteno.Debug;
using Reteno.Events;
using Reteno.InAppMessages;
using Reteno.Notifications;
using Reteno.Users;
using UnityEngine;

namespace Reteno.Android
{
    public sealed class RetenoAndroid : RetenoPlatform
    {
        private static RetenoAndroid _instance;

        public override INotificationsManager Notifications => _androidNotifications;
        public override IUserManager UserManager => _androidUserManager;
        public override IPushNotificationPermissionManager PushNotificationPermissionManager => _androidPushNotificationPermissionManager;
        public override IInAppMessagesManager InAppMessagesManager => _androidInAppMessages;
        public override IEventManager EventManager => _androidEventManager;

        private AndroidNotificationsManager _androidNotifications;
        private AndroidUserManager _androidUserManager;
        private AndroidInAppMessagesManager _androidInAppMessages;
        private AndroidEventManager _androidEventManager;
        private AndroidPushNotificationPermissionManager _androidPushNotificationPermissionManager;

        public override void Initialize(string appId)
        { 
            SDKDebug.Info(nameof(Initialize) + "Android platform"); 
            InitReteno(appId);
           
           _androidNotifications ??= new AndroidNotificationsManager();
           _androidUserManager ??= new AndroidUserManager();
           _androidInAppMessages ??= new AndroidInAppMessagesManager();
           _androidEventManager ??= new AndroidEventManager();
           _androidPushNotificationPermissionManager ??= new AndroidPushNotificationPermissionManager();
           
           SDKDebug.Info( "End init");
        }

        private void InitReteno(string apiKey)
        {
            const string unityPlayerClass = "com.unity3d.player.UnityPlayer";
            const string retenoProviderClass = "com.reteno.unity.RetenoProvider";
            
            try
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass(unityPlayerClass);
                AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
                AndroidJavaClass retenoProvider = new AndroidJavaClass(retenoProviderClass);
             
                retenoProvider.CallStatic("initReteno", context, apiKey);

                SDKDebug.Info("Init");
            }
            catch (AndroidJavaException e)
            {
                SDKDebug.Error(e.Message);
            }
        }

        public RetenoAndroid()
        {
            if (_instance != null)
                SDKDebug.Error("Additional instance of Reteno created.");

            _instance = this;
        }
    }
}