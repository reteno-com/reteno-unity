namespace Reteno.InAppMessages
{
    public interface IInAppMessagesManager
    {
        /// <summary>
        /// Change logic of In-App messages in paused state
        /// Default state is InAppPauseBehaviour.POSTPONE_IN_APPS
        /// </summary>
        /// <param name="isPaused">New pause value</param>
        void PauseInAppMessages(bool isPaused);
        /// <summary>
        /// Pause or unpause In-App messages showing during app runtime.
        /// </summary>
        /// <param name="behaviour">new behaviour</param>
        void SetInAppMessagesPauseBehaviour(InAppPauseBehaviour behaviour);
    }
}