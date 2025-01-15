using System;
using System.Collections.Generic;
using Reteno.Core.Recommendations;
using Reteno.Utilities;
using UnityEngine;

namespace Reteno.Android.Recommendations
{
    public class AndroidRecommendationsManager : IRecommendationsManager
    {
        private AndroidJavaObject recommendationBridge;

        public AndroidRecommendationsManager()
        {
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                var bridgeClass = new AndroidJavaClass("com.reteno.unity.RetenoRecommendation");
                recommendationBridge = bridgeClass.CallStatic<AndroidJavaObject>("getInstance");
            }
        }

        public void GetRecommendations(
            string recomVariantId,
            string[] productIds,
            string categoryId,
            string filtersJSON,
            string[] fields,
            Action<string, string> callback)
        {
            var callbackProxy = new RecommendationCallback(
                onSuccess: (jsonResult) => { callback?.Invoke(jsonResult, null); },
                onFailure: (error) => { callback?.Invoke(null, error); }
            );

            string recomRequestJSON = CreateRecomRequestJSON(recomVariantId, productIds, categoryId, filtersJSON, fields);
            recommendationBridge.Call("fetchRecommendation", recomVariantId, recomRequestJSON, callbackProxy);
        }

        public void LogRecommendations(RecomEvents recomEvents)
        {
            string recomEventsJson = Json.Serialize(recomEvents);
            recommendationBridge.Call("logRecommendations", recomEventsJson);
        }

        private string CreateRecomRequestJSON(string recomVariantId, string[] productIds, string categoryId,
            string filtersJSON, string[] fields)
        {
            var recomRequest = new Dictionary<string, object>
            {
                { "recomVariantId", recomVariantId },
                { "productIds", productIds },
                { "categoryId", categoryId },
                { "filters", Json.Deserialize(filtersJSON) },
                { "fields", fields }
            };

            return Json.Serialize(recomRequest);
        }
    }
}