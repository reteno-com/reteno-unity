package com.reteno.unity;

import android.content.Context;
import android.util.Log;

import com.reteno.core.Reteno;
import com.reteno.core.RetenoApplication;
import com.reteno.core.RetenoConfig;
import com.reteno.core.domain.model.event.LifecycleTrackingOptions;

public class RetenoProvider {
    public static Reteno getReteno(Context unityAppContext){
        try {
            return ((RetenoApplication) unityAppContext).getRetenoInstance();
        } catch (NullPointerException e){
            Log.d("UnityPlugin", "Error providing Reteno instance.");
            e.printStackTrace();
        }
        return null;
    }

      public static void initReteno(Context unityAppContext, String apiKey){
        try {
            RetenoConfig config = new RetenoConfig(
            false,
            null,
            LifecycleTrackingOptions.Companion.getNONE(),
            apiKey
            );

             ((RetenoApplication) unityAppContext).getRetenoInstance().initWith(config);
        } catch (NullPointerException e){
            Log.d("UnityPlugin", "Error providing Reteno instance.");
            e.printStackTrace();
        }
    }

    public static RetenoCustomDataHandler getRetenoCustomDataHandler(Context unityAppContext){
        try {
            return ((RetenoUnityApplication) unityAppContext).getCustomDataHandler();
        } catch (NullPointerException e){
            Log.d("UnityPlugin", "Error providing RetenoCustomDataHandler instance.");
            e.printStackTrace();
        }
        return null;
    }
}
