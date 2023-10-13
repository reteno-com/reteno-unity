using System.Collections.Generic;

namespace Core.Scripts.RetenoSdk.Dto
{
    /// <summary>
    /// The custom event class
    /// </summary>
    public class CustomEvent
    {
        /// <summary>
        /// Gets or sets the value of the event type key
        /// </summary>
        public string EventTypeKey { get; set; }

        /// <summary>
        /// Gets or sets the value of the occurred
        /// </summary>
        public string Occurred { get; set; }

        /// <summary>
        /// Gets or sets the value of the params
        /// </summary>
        public List<Param> Params { get; set; }
    }
}