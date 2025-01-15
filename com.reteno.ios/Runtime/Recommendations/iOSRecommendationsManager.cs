using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Google.MiniJSON;
using Reteno.Core.Recommendations;

namespace Reteno.iOS.Recommendations
{
    public class iOSRecommendationsManager : IRecommendationsManager
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void CompletionDelegate(string jsonResult, string error);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        private static extern void getRecommendations(
            string recomVariantId,
            string[] productIds, int productIdsCount,
            string categoryId,
            string filtersJSON,
            string[] fields, int fieldsCount,
            CompletionDelegate callback);
        
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        private static extern void logRecommendations(string recomVariantId,
            string[] impressionsJSON, int impressionsCount,
            string[] clicksJSON, int clicksCount);

        private static CompletionDelegate storedCompletionDelegate;

        public void GetRecommendations(
            string recomVariantId,
            string[] productIds,
            string categoryId,
            string filtersJSON,
            string[] fields,
            Action<string, string> callback)
        {
            CompletionDelegate completionDelegate = (jsonResult, error) =>
            {
                callback(jsonResult, error);
            };

            storedCompletionDelegate = completionDelegate;

            getRecommendations(
                recomVariantId,
                productIds, productIds.Length,
                categoryId,
                filtersJSON,
                fields, fields.Length,
                completionDelegate);
        }

        public void LogRecommendations(RecomEvents recomEvents)
        {
            List<RecomEventData> impressions = new List<RecomEventData>();
            List<RecomEventData> clicks = new List<RecomEventData>();
            List<string> impressionsJSON = new List<string>();
            List<string> clicksJSON = new List<string>();
            
            for (var i = 0; i < recomEvents.RecomEventsList.Count; i++)
            {
                if (recomEvents.RecomEventsList[i].RecomEventType == RecomEventType.CLICKS)
                {
                    clicks.Add(new RecomEventData(
                        recomEvents.RecomEventsList[i].Occurred, 
                        recomEvents.RecomEventsList[i].ProductId));
                }
                
                if (recomEvents.RecomEventsList[i].RecomEventType == RecomEventType.IMPRESSIONS)
                {
                    impressions.Add(new RecomEventData(
                        recomEvents.RecomEventsList[i].Occurred, 
                        recomEvents.RecomEventsList[i].ProductId));
                }
            }
            
            foreach (var impression in impressions)
            {
                string json = Json.Serialize(new
                {
                    occurred = impression.Occurred.ToString("o"), // ISO 8601
                    productId = impression.ProductId
                });
                impressionsJSON.Add(json);
            }

            foreach (var click in clicks)
            {
                string json = Json.Serialize(new
                {
                    occurred = click.Occurred.ToString("o"), // ISO 8601
                    productId = click.ProductId
                });
                clicksJSON.Add(json);
            }

            string[] impressionsArray = impressionsJSON.ToArray();
            string[] clicksArray = clicksJSON.ToArray();

            logRecommendations(recomEvents.RecomVariantId,
                impressionsArray, impressionsArray.Length,
                clicksArray, clicksArray.Length);
        }
        
        private class RecomEventData
        {
            public DateTimeOffset Occurred { get; set; }
            public string ProductId { get; set; }
           
            public RecomEventData(DateTimeOffset occurred, string productId)
            {
                Occurred = occurred;
                ProductId = productId;
            }
        }
    }
}