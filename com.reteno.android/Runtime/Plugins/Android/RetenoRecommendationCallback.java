package com.reteno.unity;

public interface RetenoRecommendationCallback {
    void onSuccess(String responseJSON);
    void onFailure(String error);
}
