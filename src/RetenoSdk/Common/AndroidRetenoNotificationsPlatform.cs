#if UNITY_ANDROID
using System;
using Core.Scripts.RetenoSdk.Dto;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;

namespace Core.Scripts.RetenoSdk.Common
{
    /// <summary>
    /// The android reteno notifications platform class
    /// </summary>
    /// <seealso cref="IRetenoNotificationsPlatform"/>
    public class AndroidRetenoNotificationsPlatform : IRetenoNotificationsPlatform
    {
        /// <summary>
        /// The is initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// The channel id
        /// </summary>
        private const string ChannelId = "RetenoChannelId";

        /// <summary>
        /// Initializes the reteno notification
        /// </summary>
        public void InitializeRetenoNotification()
        {
            if (isInitialized)
            {
                return;
            }

            isInitialized = true;

            InitializeAndroidNotification();
        }

        /// <summary>
        /// Gets the last notification
        /// </summary>
        /// <returns>The intent data</returns>
        public IntentData GetLastNotification()
        {
            var notificationIntentData = AndroidNotificationCenter.GetLastNotificationIntent();

            if (notificationIntentData != null)
            {
                var intentData = RetenoJsonDeserializer.DeserializeIntentData(notificationIntentData.Notification.IntentData);
                AndroidNotificationCenter.CancelNotification(notificationIntentData.Id);

                return intentData;
            }

            return null;
        }

        /// <summary>
        /// Sends the notification using the specified title
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="content">The content</param>
        /// <param name="interactionId">The interaction id</param>
        /// <param name="link">The link</param>
        /// <param name="fireTime">The fire time</param>
        /// <param name="path">The path</param>
        public void SendNotification(string title, string content, string interactionId, string link, DateTime fireTime, string path)
        {
            try
            {
                var notification = new AndroidNotification()
                {
                    Title = title,
                    Text = content,
                    FireTime = fireTime,
                    BigPicture = new BigPictureStyle()
                    {
                        Picture = path,
                        ContentTitle = title,
                        SummaryText = content,
                        ShowWhenCollapsed = true,
                        ContentDescription = content
                    }
                };

                var intentData = new IntentData
                {
                    InteractionId = interactionId,
                    Link = link
                };

                notification.IntentData = RetenoJsonSerializer.Serialize(intentData);
                AndroidNotificationCenter.SendNotification(notification, ChannelId);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                Debug.LogError(exception.ToString());
            }
        }

        /// <summary>
        /// Initializes the android notification
        /// </summary>
        private static void InitializeAndroidNotification()
        {
            try
            {
                var androidChannel = new AndroidNotificationChannel(
                    ChannelId,
                    "Default Channel",
                    "Reteno notifications",
                    Importance.Default)
                {
                    CanBypassDnd = false,
                    CanShowBadge = true,
                    EnableLights = false,
                    EnableVibration = false,
                    LockScreenVisibility = LockScreenVisibility.Public,
                    VibrationPattern = null
                };

                AndroidNotificationCenter.RegisterNotificationChannel(androidChannel);

                if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
                {
                    Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
                }

                Debug.Log("Notification channel registered");
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                Debug.LogError(exception.ToString());
            }
        }
    }
}
#endif