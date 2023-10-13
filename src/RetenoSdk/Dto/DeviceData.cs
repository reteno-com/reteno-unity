namespace Core.Scripts.RetenoSdk.Dto
{
    /// <summary>
    /// The device data class
    /// </summary>
    public class DeviceData
    {
        /// <summary>
        /// Gets or sets the value of the device id
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the value of the external user id
        /// </summary>
        public string ExternalUserId { get; set; }

        /// <summary>
        /// Gets or sets the value of the push token
        /// </summary>
        public string PushToken { get; set; }

        /// <summary>
        /// Gets or sets the value of the push subscribed
        /// </summary>
        public bool PushSubscribed { get; set; } = true;

        /// <summary>
        /// Gets or sets the value of the category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the value of the os type
        /// </summary>
        public string OsType { get; set; }

        /// <summary>
        /// Gets or sets the value of the os version
        /// </summary>
        public string OsVersion { get; set; }

        /// <summary>
        /// Gets or sets the value of the device model
        /// </summary>
        public string DeviceModel { get; set; }

        /// <summary>
        /// Gets or sets the value of the app version
        /// </summary>
        public string AppVersion { get; set; }

        /// <summary>
        /// Gets or sets the value of the language code
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets the value of the time zone
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the value of the advertising id
        /// </summary>
        public string AdvertisingId { get; set; }
    }
}