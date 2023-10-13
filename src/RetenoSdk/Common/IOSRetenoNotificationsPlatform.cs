#if UNITY_IOS
using System;
using Core.Scripts.RetenoSdk.Dto;
using Unity.Notifications.iOS;
using UnityEngine;

namespace Core.Scripts.RetenoSdk.Common
{
    /// <summary>
    /// The ios reteno notifications platform class
    /// </summary>
    /// <seealso cref="IRetenoNotificationsPlatform"/>
    public class IOSRetenoNotificationsPlatform : IRetenoNotificationsPlatform
    {
        /// <summary>
        /// The is initialized
        /// </summary>
        private static bool isInitialized;

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

            InitializeIOSNotification();
        }

        /// <summary>
        /// Gets the last notification
        /// </summary>
        /// <returns>The intent data</returns>
        public IntentData GetLastNotification()
        {
            var notificationIntentData = iOSNotificationCenter.GetLastRespondedNotification();

            if (notificationIntentData != null)
            {
                var intentData = RetenoJsonDeserializer.DeserializeIntentData(notificationIntentData.Data);

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
            var timeTrigger = new iOSNotificationTimeIntervalTrigger()
            {
                TimeInterval = fireTime.Subtract(DateTime.Now),
                Repeats = false
            };

            var intentData = new IntentData
            {
                InteractionId = interactionId,
                Link = link
            };

            var notification = new iOSNotification()
            {
                Identifier = interactionId,
                Title = title,
                Body = content,
                Subtitle = title,
                ShowInForeground = true,
                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                CategoryIdentifier = "Reteno",
                ThreadIdentifier = "Reteno",
                Trigger = timeTrigger,
                Data = RetenoJsonSerializer.Serialize(intentData)
            };

            iOSNotificationCenter.ScheduleNotification(notification);
        }

        /// <summary>
        /// Initializes the ios notification
        /// </summary>
        private static void InitializeIOSNotification()
        {
            using var req = new AuthorizationRequest(AuthorizationOption.Alert | AuthorizationOption.Badge, true);
            Debug.Log($"Device token: {req.DeviceToken}");
        }
    }
}
#endif