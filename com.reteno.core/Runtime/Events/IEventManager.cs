namespace Reteno.Events
{
    public interface IEventManager
    {
        /// <summary>
        /// Sends the custom event to backend
        /// </summary>
        /// <param name="customEvent">The custom event</param>
        void LogEvent(CustomEvent customEvent);
        /// <summary>
        /// Sends the custom event using the specified custom events
        /// </summary>
        /// <param name="screenName">The custom events</param>
        void LogScreenView(string screenName);
    }
}