package com.reteno.unity;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

public class RetenoNotificationClickedReceiver extends BroadcastReceiver {
    @Override
    public void onReceive(Context context, Intent intent) {
        try {
            RetenoCustomDataHandler customDataHandler = ((RetenoUnityApplication) context.getApplicationContext()).getCustomDataHandler();
            if (customDataHandler != null) {
                customDataHandler.onNotificationReceived(intent);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
