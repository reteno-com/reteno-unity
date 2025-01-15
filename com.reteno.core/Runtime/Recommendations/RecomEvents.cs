using System;
using System.Collections.Generic;

namespace Reteno.Core.Recommendations
{
    [Serializable]
    public class RecomEvents
    {
        public string RecomVariantId { get; set; }
        public List<RecomEvent> RecomEventsList { get; set; }
    }
}