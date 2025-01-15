using System;

namespace Reteno.Core.Recommendations
{
    public interface IRecommendationsManager
    {
        void GetRecommendations(
            string recomVariantId,
            string[] productIds,
            string categoryId,
            string filtersJSON,
            string[] fields,
            Action<string, string> callback
        );

        void LogRecommendations(RecomEvents recomEvents);
    }
}