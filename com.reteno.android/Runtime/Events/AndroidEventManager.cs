using System.Collections.Generic;
using Reteno.Android.Utilities;
using Reteno.Events;
using UnityEngine;

namespace Reteno.Android.Events
{
    /// <summary>
    /// Manages the Events
    /// </summary>
    public class AndroidEventManager : IEventManager
    {
        public void LogEvent(CustomEvent customEvent)
        {
            List<AndroidJavaObject> paramsList = new List<AndroidJavaObject>();
            foreach(Parameter param in customEvent.Parameters)
            {
                paramsList.Add(GetEventParameter(param));
            }

            AndroidJavaObject retenoEvent = new AndroidJavaObject(
                "com.reteno.core.domain.model.event.Event.Custom",
                customEvent.EventTypeKey,
                customEvent.OccurredDate.ToAndroidJavaObject(),
                paramsList
            );

            AndroidJavaObject reteno = RetenoJavaInstance.Get();
            reteno.Call("logEvent", retenoEvent);
        }
        
        public void LogScreenView(string screenName)
        {
            AndroidJavaObject reteno = RetenoJavaInstance.Get();
            reteno.Call("logScreenView", screenName);
        }

        private AndroidJavaObject GetEventParameter(Parameter parameter)
        {
            return new AndroidJavaObject("com.reteno.core.domain.model.event.Parameter",
                parameter.Name,
                parameter.Value
            );
        }
    }
}