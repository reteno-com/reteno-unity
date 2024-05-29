namespace Reteno.Debug
{
    public static class SDKDebug
    {
        /// <summary>
        ///  Use for Info Log in RetenoSDK
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            UnityEngine.Debug.Log(FormatMessage(message));
        }

        /// <summary>
        ///  Use for Warning Log in RetenoSDK
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(string message)
        {
            UnityEngine.Debug.LogWarning(FormatMessage(message));
        }

        /// <summary>
        /// Use for Error Log in RetenoSDK
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            UnityEngine.Debug.LogError(FormatMessage(message));
        }

        /// <summary>
        /// Format message for RetenoSDK
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string FormatMessage(string message)
        {
            return "[Reteno] " + message;
        }
    }
}