package com.reteno.unity;

import android.app.Application;
import android.content.Context;
import android.util.Log;
import androidx.annotation.NonNull;

import com.reteno.core.Reteno;
import com.reteno.core.RetenoApplication;
import com.reteno.core.RetenoImpl;

public class RetenoApp extends Application implements RetenoApplication, RetenoUnityApplication {

    private Reteno retenoInstance;
    private RetenoCustomDataHandler retenoCustomDataHandler;

    @Override
    public void onCreate() {
        super.onCreate();
        retenoInstance = new RetenoImpl(this);
        retenoCustomDataHandler = new RetenoCustomDataHandler();
    }

    @NonNull
    @Override
    public Reteno getRetenoInstance() {
        return retenoInstance;
    }

    @Override
    public RetenoCustomDataHandler getCustomDataHandler() {
        return retenoCustomDataHandler;
    }
}
