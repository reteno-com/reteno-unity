namespace RetenoSDK.Debug
{
    public static class SDKDebug
    {
        public static void Info(string message)
        {
            UnityEngine.Debug.Log(FormatMessage(message));
        }

        public static void Warn(string message)
        {
            UnityEngine.Debug.LogWarning(FormatMessage(message));
        }

        public static void Error(string message)
        {
            UnityEngine.Debug.LogError(FormatMessage(message));
        }

        private static string FormatMessage(string message)
        {
            return "[Reteno] " + message;
        }
    }
}