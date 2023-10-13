using System;
using Core.Scripts.RetenoSdk.Dto;

namespace Core.Scripts.RetenoSdk.Common
{
    /// <summary>
    /// The reteno notifications platform interface
    /// </summary>
    public interface IRetenoNotificationsPlatform
    {
        /// <summary>
        /// Initializes the reteno notification
        /// </summary>
        public void InitializeRetenoNotification();

        /// <summary>
        /// Gets the last notification
        /// </summary>
        /// <returns>The intent data</returns>
        public IntentData GetLastNotification();

        /// <summary>
        /// Sends the notification using the specified title
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="content">The content</param>
        /// <param name="interactionId">The interaction id</param>
        /// <param name="link">The link</param>
        /// <param name="fireTime">The fire time</param>
        /// <param name="path">The path</param>
        public void SendNotification(string title, string content, string interactionId, string link, DateTime fireTime, string path);
    }
}