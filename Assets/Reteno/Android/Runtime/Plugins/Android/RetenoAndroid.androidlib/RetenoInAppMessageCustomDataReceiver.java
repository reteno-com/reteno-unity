package com.reteno.unity;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

public class RetenoInAppMessageCustomDataReceiver extends BroadcastReceiver {
    @Override
    public void onReceive(Context context, Intent intent) {
        try {
            RetenoCustomDataHandler customDataHandler = ((RetenoUnityApplication) context.getApplicationContext()).getCustomDataHandler();
            if (customDataHandler != null) {
                customDataHandler.onInAppCustomDataReceived(intent);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
