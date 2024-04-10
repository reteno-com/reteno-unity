package com.reteno.testunity;

import android.app.Application;
import android.content.Context;
import android.util.Log;
import androidx.annotation.NonNull;

import com.reteno.core.Reteno;
import com.reteno.core.RetenoApplication;
import com.reteno.core.RetenoImpl;

public class RetenoApp extends Application implements RetenoApplication {

    private Reteno retenoInstance;

    @Override
    public void onCreate() {
        super.onCreate();
        retenoInstance = new RetenoImpl(this, "f46c32c6-7d5e-4439-bba5-ce36ead04fc9");
    }

    @NonNull
    @Override
    public Reteno getRetenoInstance() {
        return retenoInstance;
    }

    public static void getReteno(Context unityAppContext){
        try {
            ((RetenoApplication) unityAppContext).getRetenoInstance();
        } catch (NullPointerException e){
            Log.d("UnityPlugin", "Error providing Reteno instance.");
            e.printStackTrace();
        }
    }
}
