using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Core.Scripts.RetenoSdk.Dto;
using UnityEngine;

namespace Core.Scripts.RetenoSdk.Common
{
    /// <summary>
    /// The reteno http client class
    /// </summary>
    public static class RetenoHttpClient
    {
        /// <summary>
        /// The client
        /// </summary>
        private static readonly HttpClient Client = new();

        /// <summary>
        /// The is initialized
        /// </summary>
        private static readonly bool IsInitialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="RetenoHttpClient"/> class
        /// </summary>
        static RetenoHttpClient()
        {
            if (IsInitialized)
            {
                return;
            }

            Client.DefaultRequestHeaders.Add("X-Reteno-Access-Key", RetenoData.AccessKey);
            Client.DefaultRequestHeaders.Add("X-Reteno-SDK-Version", "1.0.0");

            IsInitialized = true;
        }

        /// <summary>
        /// Sets the device using the specified device data
        /// </summary>
        /// <param name="deviceData">The device data</param>
        public static void SetDevice(DeviceData deviceData)
        {
            var json = RetenoJsonSerializer.Serialize(deviceData);
            Debug.Log($"HTTPPOST {ApiContract.DeviceUrl}, {json}");
            HttpPost(ApiContract.DeviceUrl, json);
        }

        /// <summary>
        /// Sets the user data using the specified user
        /// </summary>
        /// <param name="user">The user</param>
        public static void SetUserData(User user)
        {
            var json = RetenoJsonSerializer.Serialize(user);
            Debug.Log($"HTTPPOST {ApiContract.UserUrl}, {json}");
            HttpPost(ApiContract.UserUrl, json);
        }

        /// <summary>
        /// Sends the custom event using the specified custom event
        /// </summary>
        /// <param name="customEvent">The custom event</param>
        public static void SendCustomEvent(CustomEventPayload customEvent)
        {
            var json = RetenoJsonSerializer.Serialize(customEvent);
            Debug.Log($"HTTPPOST {ApiContract.EventsUrl}, {json}");
            HttpPost(ApiContract.EventsUrl, json);
        }

        /// <summary>
        /// Describes whether update notification status
        /// </summary>
        /// <param name="interactionId">The interaction id</param>
        /// <param name="status">The status</param>
        /// <returns>The bool</returns>
        public static bool UpdateNotificationStatus(string interactionId, string status)
        {
            Debug.Log($"HTTPPUT {ApiContract.InteractionsUrl}, {interactionId}, {status}");
            return HttpPut($"{ApiContract.InteractionsUrl}{interactionId}/status", "{\"status\": \"" + status + "\"}");
        }

        /// <summary>
        /// Downloads the image using the specified path
        /// </summary>
        /// <param name="path">The path</param>
        /// <param name="image">The image</param>
        public static void DownloadImage(string path, string image)
        {
            HttpDownloadFile(path, image);
        }

        /// <summary>
        /// Https the download file using the specified path
        /// </summary>
        /// <param name="path">The path</param>
        /// <param name="image">The image</param>
        private static void HttpDownloadFile(string path, string image)
        {
            try
            {
                using (var response = Client.GetAsync(image).Result)
                using (var streamToReadFrom = response.Content.ReadAsStreamAsync().Result)
                {
                    using (Stream streamToWriteTo = File.Open(path, FileMode.Create))
                    {
                        streamToReadFrom.CopyTo(streamToWriteTo);
                    }
                }

                Debug.Log($"File downloaded: {path}");
            }
            catch (Exception exception)
            {
                Debug.LogError($"Exception: {exception.Message}");
                Debug.LogError(exception);
            }
        }

        /// <summary>
        /// Https the post using the specified url
        /// </summary>
        /// <param name="url">The url</param>
        /// <param name="json">The json</param>
        private static void HttpPost(string url, string json)
        {
            try
            {
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                using var response = Client.PostAsync(url, stringContent).Result;
                var responseBody = response.Content.ReadAsStringAsync().Result;

                Debug.Log($"Status Code: {response.StatusCode}");
                Debug.Log($"Response Body: {responseBody}");

                response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                Debug.LogError($"Exception: {exception.Message}");
                Debug.LogError(exception);
            }
        }

        /// <summary>
        /// Describes whether http put
        /// </summary>
        /// <param name="url">The url</param>
        /// <param name="json">The json</param>
        /// <returns>The bool</returns>
        private static bool HttpPut(string url, string json)
        {
            try
            {
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                using var response = Client.PutAsync(url, stringContent).Result;
                var responseBody = response.Content.ReadAsStringAsync().Result;

                Debug.Log($"Status Code: {response.StatusCode}");
                Debug.Log($"Response Body: {responseBody}");

                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception exception)
            {
                Debug.LogError($"Exception: {exception.Message}");
                Debug.LogError(exception);

                return false;
            }
        }
    }
}