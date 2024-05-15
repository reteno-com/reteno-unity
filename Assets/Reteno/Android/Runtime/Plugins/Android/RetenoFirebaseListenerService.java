package com.reteno.unity;

import com.google.firebase.messaging.RemoteMessage;
import com.google.firebase.messaging.cpp.ListenerService;
import com.reteno.fcm.RetenoFirebaseMessagingService;

public class RetenoFirebaseListenerService extends ListenerService {

    private RetenoFirebaseMessagingService retenoMessagingService = new RetenoFirebaseMessagingService();

    @Override
    public void onCreate() {
        super.onCreate();
        retenoMessagingService.onCreate();
    }

    @Override
    public void onMessageReceived(RemoteMessage message) {
	boolean hasInteractionId = message.getData().containsKey("es_interaction_id");
        if (hasInteractionId) {
            retenoMessagingService.onMessageReceived(message);
        } else {
            super.onMessageReceived(message);
        }
    }

    @Override
    public void onNewToken(String token) {
        retenoMessagingService.onNewToken(token);
        super.onNewToken(token);
    }
}