using UnityEngine;

namespace Reteno.Android
{
    public class RetenoJavaInstance
    {
        public static AndroidJavaObject Get()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
            AndroidJavaClass pluginClass = new AndroidJavaClass("com.reteno.unity.RetenoProvider");
            return pluginClass.CallStatic<AndroidJavaObject>("getReteno", context);
        }

        public static AndroidJavaObject GetCustomDataHandler()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
            AndroidJavaClass pluginClass = new AndroidJavaClass("com.reteno.unity.RetenoProvider");
            return pluginClass.CallStatic<AndroidJavaObject>("getRetenoCustomDataHandler", context);
        }
    }
}