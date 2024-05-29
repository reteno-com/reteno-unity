package com.reteno.unity;

import android.content.Context;
import android.content.Intent;
import android.util.Log;

import com.reteno.core.Reteno;
import com.reteno.core.RetenoApplication;
import com.reteno.core.RetenoConfig;
import com.reteno.core.domain.model.event.LifecycleTrackingOptions;

public class RetenoProvider {
    public static Reteno getReteno(Context unityAppContext) {
        try {
            return ((RetenoApplication) unityAppContext.getApplicationContext()).getRetenoInstance();
        } catch (NullPointerException | ClassCastException e) {
            Log.d("RETENO", "Error init.");
            e.printStackTrace();
        }
        return null;
    }

    public static void initReteno(Context unityAppContext, String apiKey) {
        try {
            RetenoConfig config = new RetenoConfig(
                false,
                null,
                LifecycleTrackingOptions.Companion.getNONE(),
                apiKey
            );

            ((RetenoApplication) unityAppContext.getApplicationContext()).getRetenoInstance().initWith(config);
            Log.d("RETENO", "successfully init Reteno with key " + apiKey);

        } catch (NullPointerException | ClassCastException e) {
            Log.d("RETENO", "Error init.");
            e.printStackTrace();
        }
    }

    public static RetenoCustomDataHandler getRetenoCustomDataHandler(Context unityAppContext) {
        try {
            return ((RetenoUnityApplication) unityAppContext).getCustomDataHandler();
        } catch (NullPointerException | ClassCastException e) {
            Log.d("RETENO", "Error init.");
            e.printStackTrace();
        }
        return null;
    }
}