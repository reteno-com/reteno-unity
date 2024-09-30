using System;
using System.Collections.Generic;
using System.Reflection;
using Reteno.Notifications;
using Reteno.Users;
using Reteno.Events;
using Reteno.InAppMessages;
using Reteno.Utilities;

namespace Reteno.Core
{
    public static class RetenoSDK
    {
        #region InitSDK

        private static RetenoPlatform _platform;
        public static RetenoPlatform Platform
        {
            private get
            {
                if (_platform != null)
                    return _platform;

                string platformTypeName = ApplicationUtil.GetPlatformName();
                if (platformTypeName == null)
                {
                    throw new InvalidOperationException("Unsupported platform");
                }

                AppDomain current = AppDomain.CurrentDomain;
                Assembly[] assemblies = current.GetAssemblies();

                Type platformType = null;
      
                foreach (var assembly in assemblies)
                {
                    if (assembly.FullName.Contains(platformTypeName))
                    {
                        Assembly assemblyReteno = Assembly.Load(assembly.FullName);
                        foreach (var type in assemblyReteno.GetTypes())
                        {
                            if (type.FullName != null && type.FullName.EndsWith(ApplicationUtil.GetTypeName()))
                            {
                                platformType = type;
                                break; // Exit the loop once the correct type is found
                            }
                        }
                        if (platformType != null) break; // Exit the outer loop if the type is already found
                    }
                }

                if (platformType == null)
                {
                    throw new InvalidOperationException("Platform type not found: " + platformTypeName);
                }
        
                return Activator.CreateInstance(platformType) as RetenoPlatform;
            }
            set => _platform = value;
        }

        #endregion
        
        private static INotificationsManager Notifications => Platform.Notifications;
        private static IUserManager UserManager => Platform.UserManager;
        private static IPushNotificationPermissionManager PushNotificationPermissionManager =>
            Platform.PushNotificationPermissionManager;
        private static IEventManager EventManager => Platform.EventManager;
        private static IInAppMessagesManager InAppMessagesManager => Platform.InAppMessagesManager;

        /// <summary>
        /// Get InAppCustomData 
        /// </summary>
        public static event Action<Dictionary<string, string>> InAppCustomData;

        /// <summary>
        /// Enter point to Initialize Reteno
        /// </summary>
        /// <param name="appId"></param>
        public static void Initialize(string appId)
        {
            Platform.Initialize(appId);
            InAppMessagesManager.CustomData += InAppMessagesManagerOnCustomData;
        }
        
        /// <summary>
        /// Sets the user attributes using the specified user
        /// </summary>
        /// <param name="user">The user</param>
        public static void SetUserAttributes(string externalUserId, User user) =>
            UserManager.SetUserAttributes(externalUserId, user);

        /// <summary>
        /// Sets the anonymous user attributes using the specified user
        /// </summary>
        /// <param name="user">The user</param>
        public static void SetAnonymousUserAttributes(UserAttributesAnonymous userAttributesAnonymous) =>
            UserManager.SetAnonymousUserAttributes(userAttributesAnonymous);

        /// <summary>
        /// Requests the user for push notification permissions and handles the callbacks.
        /// </summary>
        public static void RequestPushPermission(Action onPermissionGranted = null, Action onPermissionDenied = null, Action onPermissionDeniedAndDontAskAgain = null) =>
            PushNotificationPermissionManager.RequestPush(onPermissionGranted, onPermissionDenied, onPermissionDeniedAndDontAskAgain);

        /// <summary>
        /// Calls the native method to update push notification permission status.
        /// </summary>
        public static void UpdatePushPermissionStatus() =>
            PushNotificationPermissionManager.UpdatePushPermissionStatus();

        /// <summary>
        /// Sends the custom event to backend
        /// </summary>
        /// <param name="customEvent">The custom event</param>
        public static void LogEvent(CustomEvent customEvent) =>
            EventManager.LogEvent(customEvent);

        /// <summary>
        /// Sends the custom event using the specified custom events
        /// </summary>
        /// <param name="screenName">The custom events</param>
        public static void LogScreenView(string screenName) =>
            EventManager.LogScreenView(screenName);

        /// <summary>
        /// Set pause all in app messages
        /// </summary>
        /// <param name="isPaused">Status paused</param>
        public static void PauseInAppMessages(bool isPaused) => 
            InAppMessagesManager.PauseInAppMessages(isPaused);

        /// <summary>
        /// Set specific pause for in app messages
        /// </summary>
        /// <param name="inAppPauseBehaviour">Specific type</param>
        public static void SetInAppMessagesPauseBehaviour(InAppPauseBehaviour inAppPauseBehaviour) =>
            InAppMessagesManager.SetInAppMessagesPauseBehaviour(inAppPauseBehaviour);
        
        private static void InAppMessagesManagerOnCustomData(Dictionary<string, string> customData) =>
            InAppCustomData?.Invoke(customData);
    }
}