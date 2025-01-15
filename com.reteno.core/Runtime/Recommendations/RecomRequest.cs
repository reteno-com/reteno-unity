using System;

namespace Reteno.Core.Recommendations
{
    [Serializable]
    public class RecomRequest
    {
        public string recomVariantId;
        public string[] productIds;
        public string categoryId;
        public RecomFilter[] filters;
        public string[] fields;
    }
}