using System;
using Core.Scripts.RetenoSdk.Dto;

namespace Core.Scripts.RetenoSdk.Common
{
    /// <summary>
    /// The reteno json deserializer class
    /// </summary>
    public static class RetenoJsonDeserializer
    {
        /// <summary>
        /// Deserializes the intent data using the specified json string
        /// </summary>
        /// <param name="jsonString">The json string</param>
        /// <returns>The intent data</returns>
        public static IntentData DeserializeIntentData(string jsonString)
        {
            var intentData = new IntentData();

            var interactionIdStartIndex = jsonString.IndexOf("\"interactionId\":\"", StringComparison.Ordinal) + "\"interactionId\":\"".Length;
            var interactionIdEndIndex = jsonString.IndexOf("\"", interactionIdStartIndex, StringComparison.Ordinal);
            intentData.InteractionId = jsonString.Substring(interactionIdStartIndex, interactionIdEndIndex - interactionIdStartIndex);

            var linkStartIndex = jsonString.IndexOf("\"link\":\"", StringComparison.Ordinal) + "\"link\":\"".Length;
            var linkEndIndex = jsonString.IndexOf("\"", linkStartIndex, StringComparison.Ordinal);
            intentData.Link = jsonString.Substring(linkStartIndex, linkEndIndex - linkStartIndex);

            return intentData;
        }

        /// <summary>
        /// Parses the json array using the specified json string
        /// </summary>
        /// <param name="jsonString">The json string</param>
        /// <returns>The string array</returns>
        public static string[] ParseJsonArray(string jsonString)
        {
            return jsonString.Trim('[', ']').Replace("\"", "").Split(',');
        }
    }
}