package com.reteno.unity;

import com.google.firebase.messaging.RemoteMessage;
import com.google.firebase.messaging.cpp.ListenerService;

public class RetenoFirebaseListenerService extends ListenerService {

    private final RetenoFirebaseServiceWrapper retenoMessagingService = new RetenoFirebaseServiceWrapper();

    @Override
    public void onCreate() {
        super.onCreate();
        retenoMessagingService.onCreate(this, getApplication());
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