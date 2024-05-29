using Reteno.Utilities;
using UnityEngine;

namespace Reteno.Android.Utilities
{
    public static class DateAndTimeExtensions
    {
        public static AndroidJavaObject ToAndroidJavaObject(this DateAndTime dateAndTime)
        {
            if (dateAndTime.UseCurrentTime)
            {
                AndroidJavaClass zonedDateTime = new AndroidJavaClass("java.time.ZonedDateTime");
                return zonedDateTime.CallStatic<AndroidJavaObject>("now");
            } else
            {
                AndroidJavaClass zoneId = new AndroidJavaClass("java.time.ZoneId");
                AndroidJavaObject defaultZoneId = zoneId.CallStatic<AndroidJavaObject>("systemDefault");
                AndroidJavaClass zonedDateTime = new AndroidJavaClass("java.time.ZonedDateTime");
                return zonedDateTime.CallStatic<AndroidJavaObject>(
                    "of", dateAndTime.Year, dateAndTime.Month, 
                    dateAndTime.Day, dateAndTime.Hour, dateAndTime.Minute, dateAndTime.Second, 0, defaultZoneId
                );
            }
        }
    }
}