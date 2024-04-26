package com.reteno.unity;

import android.content.Context;
import android.util.Log;

import com.reteno.core.Reteno;
import com.reteno.core.RetenoApplication;

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
