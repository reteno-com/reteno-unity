using System;

namespace Reteno.Core.Recommendations
{
    [Serializable]
    public class RecomEvent
    {
        public RecomEventType RecomEventType { get; set; }
        public DateTimeOffset Occurred { get; set; }
        public string ProductId { get; set; }
    }
}