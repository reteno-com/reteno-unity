using System.Linq;
using System.Runtime.InteropServices;
using Reteno.Events;

namespace Reteno.iOS.Events
{
    public class iOSEventManager : IEventManager
    {
        [DllImport("__Internal")]
        private static extern void logEvent(string eventTypeKey, string[] names, string[] values, int count, bool forcePush);

        public void LogEvent(CustomEvent customEvent)
        {
            if(customEvent == null) return;
            
            var eventTypeKey = customEvent.EventTypeKey;

            string[] names = { };
            string[] values = { };
            int count = 0;
            
            if (customEvent.Parameters.Count > 0)
            {
                names = customEvent.Parameters.Select(param => param.Name).ToArray();
                values  = customEvent.Parameters.Select(param => param.Value).ToArray();
                count = customEvent.Parameters.Count;
            }
          
            var forcePush = customEvent.ForcePush;

           logEvent(eventTypeKey, names, values, count, forcePush);
        }

        public void LogScreenView(string screenName)
        {
        }
    }
}