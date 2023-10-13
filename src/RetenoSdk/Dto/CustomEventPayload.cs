using System.Collections.Generic;

namespace Core.Scripts.RetenoSdk.Dto
{
    /// <summary>
    /// The custom event payload class
    /// </summary>
    public class CustomEventPayload
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
        /// Gets or sets the value of the events
        /// </summary>
        public List<CustomEvent> Events { get; set; }
    }
}