using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Scripts.RetenoSdk.Common;
using Core.Scripts.RetenoSdk.Dto;
using Firebase;
using Firebase.Extensions;
using Firebase.Messaging;
using UnityEngine;
#if UNITY_IOS
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.PushNotifications;
#endif

namespace Core.Scripts.RetenoSdk
{
    /// <summary>
    /// The reteno sdk class
    /// </summary>
    public static class RetenoSdk
    {
        /// <summary>
        /// The fcm token
        /// </summary>
        public static string FcmToken;


        /// <summary>
        /// The is initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// The reteno notifications platform
        /// </summary>
        private static readonly IRetenoNotificationsPlatform RetenoNotificationsPlatform;

        private static DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
        private const string Topic = "RetenoUnityApp";

        /// <summary>
        /// Initializes a new instance of the <see cref="RetenoSdk"/> class
        /// </summary>
        static RetenoSdk()
        {
#if UNITY_ANDROID
            RetenoNotificationsPlatform = new AndroidRetenoNotificationsPlatform();
#endif
#if UNITY_IOS
            RetenoNotificationsPlatform = new IOSRetenoNotificationsPlatform();
#endif
            if (!isInitialized && !string.IsNullOrEmpty(RetenoData.AccessKey) && !string.IsNullOrEmpty(RetenoData.ExternalUserId))
            {
                Start();

                isInitialized = true;
            }
        }

        /// <summary>
        /// Initializes the access key
        /// </summary>
        /// <param name="accessKey">The access key</param>
        /// <param name="externalUserId">The external user id</param>
        public static void Initialize(string accessKey, string externalUserId = "")
        {
            RetenoData.SetConfigurations(accessKey, externalUserId);

            if (!isInitialized)
            {
                Start();

                isInitialized = true;
            }

            if (!string.IsNullOrEmpty(FcmToken))
            {
                SetDevice();
            }
        }

        /// <summary>
        /// Sets the user attributes using the specified user
        /// </summary>
        /// <param name="user">The user</param>
        public static void SetUserAttributes(User user)
        {
            if (!string.IsNullOrEmpty(FcmToken) || !string.IsNullOrEmpty(RetenoData.ExternalUserId))
            {
                SetDevice();
            }

            user.DeviceId = RetenoData.DeviceId;
            user.ExternalUserId = RetenoData.ExternalUserId;

            RetenoHttpClient.SetUserData(user);
        }


        /// <summary>
        /// Sets the anonymous user attributes using the specified user
        /// </summary>
        /// <param name="user">The user</param>
        public static void SetAnonymousUserAttributes(User user)
        {
            if (!string.IsNullOrEmpty(FcmToken) || !string.IsNullOrEmpty(RetenoData.ExternalUserId))
            {
                SetDevice();
            }

            user.DeviceId = RetenoData.DeviceId;

            RetenoHttpClient.SetUserData(user);
        }

        /// <summary>
        /// Sends the custom event using the specified custom events
        /// </summary>
        /// <param name="customEvents">The custom events</param>
        public static void SendCustomEvent(List<CustomEvent> customEvents)
        {
            var customEventPayload = new CustomEventPayload
            {
                DeviceId = RetenoData.DeviceId,
                ExternalUserId = RetenoData.ExternalUserId,
                Events = customEvents
            };

            RetenoHttpClient.SendCustomEvent(customEventPayload);
        }

        /// <summary>
        /// Ons the notification clicked
        /// </summary>
        public static void OnNotificationClicked()
        {
            var intentData = RetenoNotificationsPlatform.GetLastNotification();
            if (intentData == null)
            {
                return;
            }

            const string status = "CLICKED";
            var isUpdated = RetenoHttpClient.UpdateNotificationStatus(intentData.InteractionId, status);
            var isNeedToRedirect = false;
            var notification = RetenoData.Interactions.FirstOrDefault(x => x.interactionId == intentData.InteractionId);
            if (notification == null)
            {
                RetenoData.AddNotification(intentData.InteractionId, status, false, isUpdated);
            }
            else
            {
                isNeedToRedirect = notification.isNeedToRedirect;
                RetenoData.UpdateNotification(intentData.InteractionId, status, false, isUpdated);
            }

            if (isNeedToRedirect && !string.IsNullOrEmpty(intentData.Link))
            {
                Application.OpenURL(intentData.Link);
            }
        }

        /// <summary>
        /// Processes the message using the specified data
        /// </summary>
        /// <param name="data">The data</param>
        public static void ProcessMessage(IDictionary<string, string> data)
        {
            Debug.Log("Received a new message");
            const string status = "DELIVERED";

            var image = string.Empty;

            if (data.TryGetValue("es_notification_images", out var notificationImages))
            {
                image = RetenoJsonDeserializer.ParseJsonArray(notificationImages)[0];
            }
            else if (data.TryGetValue("es_notification_image", out var notificationImage))
            {
                image = notificationImage;
            }

            var link = string.Empty;
            if (data.TryGetValue("es_link_raw", out var notificationLink))
            {
                link = notificationLink;
            }

            SendNotification(data["es_title"], data["es_content"], image, DateTime.Now.ToLocalTime() + TimeSpan.FromSeconds(3), data["es_interaction_id"], link);

            var isSentToApi = RetenoHttpClient.UpdateNotificationStatus(data["es_interaction_id"], status);
            RetenoData.AddNotification(data["es_interaction_id"], status, !string.IsNullOrEmpty(link), isSentToApi);

            Debug.Log("Notification sent");
        }

        /// <summary>
        /// Ons the token received using the specified sender
        /// </summary>
        /// <param name="token">The token</param>
        public static void SetToken(string token)
        {
            Debug.Log("Received Registration Token: " + token);

            FcmToken = token;

            SetDevice();
        }

        /// <summary>
        /// Sets the device
        /// </summary>
        private static void SetDevice()
        {
            if (!string.IsNullOrEmpty(RetenoData.AccessKey) && !string.IsNullOrEmpty(RetenoData.ExternalUserId))
            {
                var deviceData = new DeviceData
                {
                    DeviceId = RetenoData.DeviceId,
                    DeviceModel = SystemInfo.deviceModel,
                    ExternalUserId = RetenoData.ExternalUserId,
                    PushSubscribed = true,
                    PushToken = FcmToken,
                    Category = DeviceCategory.MOBILE.ToString(),
#if UNITY_ANDROID
                    OsType = DeviceOS.ANDROID.ToString(),
#endif
#if UNITY_IOS
                    OsType = DeviceOS.IOS.ToString(),
#endif
                    LanguageCode = DictionaryHelper.GetLanguageCode(),
                    TimeZone = DictionaryHelper.GetOlsonTimeZone()
                };

                RetenoHttpClient.SetDevice(deviceData);
            }
        }

        /// <summary>
        /// Starts
        /// </summary>
        private static void Start()
        {
#if UNITY_ANDROID
            InitializeFirebase();
#endif
#if UNITY_IOS
            if (CheckIsApnsPackageInstalled())
            {
                UnityServices.InitializeAsync().Wait();
                AnalyticsService.Instance.StartDataCollection();
                
                string pushToken = PushNotificationsService.Instance.RegisterForPushNotificationsAsync().Wait();
                SetToken(pushToken);
                
                PushNotificationsService.Instance.OnNotificationReceived += notificationData =>
                {
                    ProcessMessage(notificationData);
                };
            }
            else
            {
                InitializeFirebase();
            }

#endif
            RetenoNotificationsPlatform.InitializeRetenoNotification();
        }

        /// <summary>
        /// Sends the notification using the specified title
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="content">The content</param>
        /// <param name="image">The image</param>
        /// <param name="fireTime">The fire time</param>
        /// <param name="interactionId">The interaction id</param>
        /// <param name="link">The link</param>
        private static void SendNotification(string title, string content, string image, DateTime fireTime, string interactionId, string link)
        {
            var path = Path.Combine(Application.persistentDataPath, $"image-{interactionId}.jpg");
            RetenoHttpClient.DownloadImage(path, image);

            RetenoNotificationsPlatform.SendNotification(title, content, interactionId, link, fireTime, path);
        }

#if UNITY_ANDROID
        /// <summary>
        /// Initializes the firebase
        /// </summary>
        private static void InitializeFirebase()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    FirebaseMessaging.MessageReceived += OnMessageReceived;
                    FirebaseMessaging.TokenReceived += OnTokenReceived;
                    FirebaseMessaging.SubscribeAsync(Topic).ContinueWithOnMainThread(null);

                    FirebaseMessaging.RequestPermissionAsync().ContinueWithOnMainThread(null);
                    Debug.Log("Firebase Messaging Initialized");
                }
                else
                {
                    Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
                }
            });
        }
#endif
        /// <summary>
        /// Ons the message received using the specified sender
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The </param>
        private static void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            ProcessMessage(e.Message.Data);
        }

        /// <summary>
        /// Ons the token received using the specified sender
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="token">The token</param>
        private static void OnTokenReceived(object sender, TokenReceivedEventArgs token)
        {
            SetToken(token.Token);
        }

        private static bool CheckIsApnsPackageInstalled()
        {
            var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            var fileName = Path.Combine(directoryInfo.FullName, "Packages/manifest.json");
            var manifestFile = new FileInfo(fileName);
            if (manifestFile.Exists)
            {
                var jsonText = File.ReadAllText(fileName);
                return jsonText.Contains("com.unity.services.push-notifications");
            }

            return false;
        }
    }
}