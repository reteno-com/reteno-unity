using Reteno.InAppMessages;

namespace Reteno.iOS.InAppMessages
{
    internal sealed class iOSInAppMessagesManager : IInAppMessagesManager
    {
        private static iOSInAppMessagesManager _instance;

        public iOSInAppMessagesManager()
        {
            _instance = this;
        }

        public void PauseInAppMessages(bool isPaused)
        {
        }

        public void SetInAppMessagesPauseBehaviour(InAppPauseBehaviour behaviour)
        {
        }

        public void SetInAppMessageCustomDataListener(RetenoCustomDataListener listener)
        {
        }
    }
}