using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RetenoAndroid
{
    /// <summary>
    /// Sets the user attributes using the specified user
    /// </summary>
    /// <param name="user">The user</param>
    public static void SetUserAttributes(string externalUserId, User user)
    {
        AndroidJavaObject userJava = CreateUserObject(user);
        AndroidJavaObject reteno = GetRetenoInstance();
        reteno.Call("setUserAttributes", externalUserId, userJava);
    }


    /// <summary>
    /// Sets the anonymous user attributes using the specified user
    /// </summary>
    /// <param name="user">The user</param>
    public static void SetAnonymousUserAttributes(User user)
    {
        AndroidJavaObject userJava = CreateUserObject(user);
        AndroidJavaObject reteno = GetRetenoInstance();
        reteno.Call("setAnonymousUserAttributes", userJava);
    }

    /// <summary>
    /// Sends the custom event to backend
    /// </summary>
    /// <param name="customEvent">The custom event</param>
    public static void LogEvent(CustomEvent customEvent)
    {
        List<AndroidJavaObject> paramsList = new List<AndroidJavaObject>();
        foreach(Parameter param in customEvent.Parameters)
        {
            paramsList.Add(GetEventParameter(param));
        }

        AndroidJavaObject retenoEvent = new AndroidJavaObject(
                "com.reteno.core.domain.model.event.Event.Custom",
                customEvent.EventTypeKey,
                customEvent.OccurredDate.toAndroidJavaObject(),
                paramsList
            );

        AndroidJavaObject reteno = GetRetenoInstance();
        reteno.Call("logEvent", retenoEvent);
    }

    /// <summary>
    /// Sends the custom event using the specified custom events
    /// </summary>
    /// <param name="customEvents">The custom events</param>
    public static void LogScreenView(string screenName)
    {
        AndroidJavaObject reteno = GetRetenoInstance();
        reteno.Call("logScreenView", screenName);
    }

    /// <summary>
    /// Change logic of In-App messages in paused state
    /// Default state is InAppPauseBehaviour.POSTPONE_IN_APPS
    /// </summary>
    /// <param name="isPaused">New pause value</param>
    public static void PauseInAppMessages(bool isPaused)
    {
        AndroidJavaObject reteno = GetRetenoInstance();
        reteno.Call("pauseInAppMessages", isPaused);
    }

    /// <summary>
    /// Pause or unpause In-App messages showing during app runtime.
    /// </summary>
    /// <param name="behaviour">new behaviour</param>
    public static void SetInAppMessagesPauseBehaviour(InAppPauseBehaviour behaviour)
    {
        AndroidJavaObject reteno = GetRetenoInstance();
        // TODO not implemented yet
        //reteno.Call("setInAppMessagesPauseBehaviour", behaviour);
    }

    public static void SetNotificationCustomDataListener(RetenoCustomDataListener listener)
    {
        AndroidJavaObject customDataHandler = GetRetenoCustomDataHandlerInstance();
        if (listener == null)
        {
            customDataHandler.Call("setNotificationCustomDataReceiver", null);
        } else
        {
            customDataHandler.Call("setNotificationCustomDataReceiver", new CustomDataProxy(listener));
        }
    }

    public static void SetInAppMessageCustomDataListener(RetenoCustomDataListener listener)
    {
        AndroidJavaObject customDataHandler = GetRetenoCustomDataHandlerInstance();
        if (listener == null)
        {
            customDataHandler.Call("setNotificationCustomDataReceiver", null);
        }
        else
        {
            customDataHandler.Call("setNotificationCustomDataReceiver", new CustomDataProxy(listener));
        }
    }

    private static AndroidJavaObject CreateUserObject(User user)
    {
        AndroidJavaObject addressJava = null;
        if (user.UserAttributes.Address != null)
        {
            addressJava = new AndroidJavaObject(
                "com.reteno.core.domain.model.user.Address",
                user.UserAttributes.Address.Region,
                user.UserAttributes.Address.Town,
                user.UserAttributes.Address.Address,
                user.UserAttributes.Address.Postcode
            );
        }

        AndroidJavaObject userAttributesJava = new AndroidJavaObject(
                "com.reteno.core.domain.model.user.UserAttributes",
                user.UserAttributes.Phone,
                user.UserAttributes.Email,
                user.UserAttributes.FirstName,
                user.UserAttributes.LastName,
                user.UserAttributes.LanguageCode,
                user.UserAttributes.TimeZone,
                addressJava,
                user.UserAttributes.Fields
            );
        AndroidJavaObject userJava = new AndroidJavaObject(
                "com.reteno.core.domain.model.user.User",
                userAttributesJava,
                user.SubscriptionKeys,
                user.GroupNamesInclude,
                user.GroupNamesExclude
            );

        return userJava;
    }

    private static AndroidJavaObject GetEventParameter(Parameter parameter)
    {
        return new AndroidJavaObject("com.reteno.core.domain.model.event.Parameter",
                parameter.Name,
                parameter.Value
            );
    }

    private static AndroidJavaObject GetRetenoInstance()
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaClass pluginClass = new AndroidJavaClass("com.reteno.unity.RetenoProvider");
        return pluginClass.CallStatic<AndroidJavaObject>("getReteno", context);
    }

    private static AndroidJavaObject GetRetenoCustomDataHandlerInstance()
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaClass pluginClass = new AndroidJavaClass("com.reteno.unity.RetenoProvider");
        return pluginClass.CallStatic<AndroidJavaObject>("getRetenoCustomDataHandler", context);
    }
}
