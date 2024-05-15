package com.reteno.unity;

import android.content.Intent;
import android.os.Bundle;

import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;

public class RetenoCustomDataHandler {

    private Map<String, String> lastNotificationCustomData = null;
    private Map<String, String> lastInAppCustomData = null;

    private RetenoCustomDataListener notificationCustomDataReceiver = null;
    private RetenoCustomDataListener inAppMessageCustomDataReceiver = null;

    public void onNotificationReceived(Intent intent) {
        lastNotificationCustomData = parseIntent(intent);
        trySendNotificationCustomData();
    }

    public void onInAppCustomDataReceived(Intent intent) {
        lastInAppCustomData = parseIntent(intent);
        trySendInAppMessageCustomData();
    }

    public void setNotificationCustomDataReceiver(RetenoCustomDataListener receiver) {
        notificationCustomDataReceiver = receiver;
        trySendNotificationCustomData();
    }

    public void setInAppMessageCustomDataReceiver(RetenoCustomDataListener receiver) {
        inAppMessageCustomDataReceiver = receiver;
        trySendInAppMessageCustomData();
    }

    private void trySendNotificationCustomData() {
        if (lastNotificationCustomData != null && notificationCustomDataReceiver != null) {
            notificationCustomDataReceiver.onCustomDataReceived(lastNotificationCustomData);
            lastNotificationCustomData = null;
        }
    }

    private void trySendInAppMessageCustomData() {
        if (lastInAppCustomData != null && inAppMessageCustomDataReceiver != null) {
            inAppMessageCustomDataReceiver.onCustomDataReceived(lastInAppCustomData);
            lastInAppCustomData = null;
        }
    }

    private Map<String, String> parseIntent(Intent intent) {
        if (intent == null) return null;

        Bundle extras = intent.getExtras();

        if (extras == null || extras.isEmpty()) return null;

        HashMap<String, String> intentParams = new HashMap<>();
        Set<String> keys = extras.keySet();
        Iterator<String> keysIterator = keys.iterator();
        String key;
        while(keysIterator.hasNext()){
            key = keysIterator.next();
            intentParams.put(key, extras.getString(key));
        }

        return intentParams;
    }
}
