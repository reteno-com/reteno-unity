using System;
using UnityEngine;

namespace Reteno.Android.Recommendations
{
    public class RecommendationCallback : AndroidJavaProxy
    {
        private Action<string> OnSuccess;
        private Action<string> OnFailure;

        public RecommendationCallback(Action<string> onSuccess, Action<string> onFailure)
            : base("com.reteno.unity.RetenoRecommendationCallback")
        {
            OnSuccess = onSuccess;
            OnFailure = onFailure;
        }

        public void onSuccess(string responseJSON)
        {
            OnSuccess?.Invoke(responseJSON);
        }

        public void onFailure(string error)
        {
            OnFailure?.Invoke(error);
        }
    }
}