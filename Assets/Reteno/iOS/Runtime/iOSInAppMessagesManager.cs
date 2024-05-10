using RetenoSDK.InAppMessages;

namespace RetenoSDK.iOS.InAppMessages
{
    internal sealed class iOSInAppMessagesManager : IInAppMessagesManager
    {
        private static iOSInAppMessagesManager _instance;

        public iOSInAppMessagesManager()
        {
            _instance = this;
        }
    }
}