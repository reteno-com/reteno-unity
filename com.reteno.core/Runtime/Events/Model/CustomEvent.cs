using System.Collections.Generic;
using Reteno.Utilities;

namespace Reteno.Events
{
    public class CustomEvent
    {
        /// <summary>
        /// Gets or sets the value of the event type key
        /// </summary>
        public string EventTypeKey { get; set; }

        /// <summary>
        /// Gets or sets the value of the occurred
        /// </summary>
        public DateAndTime OccurredDate { get; set; }

        /// <summary>
        /// Gets or sets the value of the params
        /// </summary>
        public List<Parameter> Parameters { get; set; }
    }
}
