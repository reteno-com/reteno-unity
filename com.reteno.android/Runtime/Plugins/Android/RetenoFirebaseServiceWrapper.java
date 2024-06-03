package com.reteno.unity;

import android.app.Application;
import android.content.Context;

import com.google.firebase.messaging.RemoteMessage;
import com.reteno.core.RetenoApplication;
import com.reteno.fcm.RetenoFirebaseServiceHandler;
import com.reteno.push.RetenoNotificationService;

public class RetenoFirebaseServiceWrapper {

    private RetenoFirebaseServiceHandler handler = null;

    public void onCreate(Context context, Application application) {
        RetenoNotificationService pushService = new RetenoNotificationService(context, ((RetenoApplication) application).getRetenoInstance());
        handler = new RetenoFirebaseServiceHandler(pushService);
    }

    public void onNewToken(String token) {
        handler.onNewToken(token);
    }

    public void onMessageReceived(RemoteMessage message) {
        handler.onMessageReceived(message);
    }
}
