using System;

namespace Core.Scripts.RetenoSdk.Dto
{
    /// <summary>
    /// The interaction class
    /// </summary>
    [Serializable]
    public class Interaction
    {
        /// <summary>
        /// The interaction id
        /// </summary>
        public string interactionId;

        /// <summary>
        /// The interaction status
        /// </summary>
        public string interactionStatus;

        /// <summary>
        /// The is need to redirect
        /// </summary>
        public bool isNeedToRedirect;

        /// <summary>
        /// The is sent to api
        /// </summary>
        public bool isSentToApi;
    }
}