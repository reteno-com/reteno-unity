namespace Core.Scripts.RetenoSdk.Common
{
    /// <summary>
    /// The api contract class
    /// </summary>
    public static class ApiContract
    {
        /// <summary>
        /// The reteno api url
        /// </summary>
        private const string RetenoApiUrl = "https://api.reteno.com/api/v1/";

        /// <summary>
        /// The mobile api url
        /// </summary>
        private const string MobileApiUrl = "https://mobile-api.reteno.com/api/v1/";

        /// <summary>
        /// The mobile api url
        /// </summary>
        public static readonly string DeviceUrl = $"{MobileApiUrl}device";

        /// <summary>
        /// The mobile api url
        /// </summary>
        public static readonly string UserUrl = $"{MobileApiUrl}user";

        /// <summary>
        /// The mobile api url
        /// </summary>
        public static readonly string EventsUrl = $"{MobileApiUrl}events";

        /// <summary>
        /// The reteno api url
        /// </summary>
        public static readonly string InteractionsUrl = $"{RetenoApiUrl}interactions/";
    }
}