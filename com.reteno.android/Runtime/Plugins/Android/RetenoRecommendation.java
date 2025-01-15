package com.reteno.unity;

import com.reteno.core.Reteno;
import com.reteno.core.RetenoApplication;
import com.reteno.core.domain.model.recommendation.post.RecomEvents;
import com.reteno.core.features.recommendation.GetRecommendationResponseCallback;
import com.reteno.core.data.remote.model.recommendation.get.RecomBase;
import com.reteno.core.data.remote.model.recommendation.get.Recoms;
import com.reteno.core.domain.model.recommendation.get.RecomRequest;
import com.google.gson.Gson;

public class RetenoRecommendation {
    private static RetenoRecommendation instance;
    private static Gson gson = new Gson();

    public static RetenoRecommendation getInstance() {
        if (instance == null) {
            instance = new RetenoRecommendation();
        }
        return instance;
    }

    public void fetchRecommendation(String recomVariantId, String recomRequestJSON, RetenoRecommendationCallback callback) {
        Reteno reteno = ((RetenoApplication) getInstance()).getRetenoInstance();
        RecomRequest recomRequest = gson.fromJson(recomRequestJSON, RecomRequest.class);
        if (recomRequest == null) {
            callback.onFailure("Invalid RecomRequest JSON");
            return;
        }

        reteno.getRecommendation().fetchRecommendation(
                recomVariantId,
                recomRequest,
                RecomBase.class,
                new GetRecommendationResponseCallback<RecomBase>() {
                    @Override
                    public void onSuccess(Recoms<RecomBase> response) {
                        String responseJSON = gson.toJson(response);
                        callback.onSuccess(responseJSON);
                    }

                    @Override
                    public void onSuccessFallbackToJson(String response) {
                        callback.onSuccess(response);
                    }

                    @Override
                    public void onFailure(Integer statusCode, String response, Throwable throwable) {
                        String errorMessage = "Error: " + response;
                        if (throwable != null) {
                            errorMessage += ", Exception: " + throwable.getMessage();
                        }
                        callback.onFailure(errorMessage);
                    }
                }
        );
    }

    public void logRecommendations(String recomEventsJSON) {
        Reteno reteno = ((RetenoApplication) getInstance()).getRetenoInstance();
        RecomEvents recomEvents = gson.fromJson(recomEventsJSON, RecomEvents.class);
        if (recomEvents != null) {
            reteno.getRecommendation().logRecommendations(recomEvents);
        }
    }
}
