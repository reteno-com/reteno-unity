using System;
using System.Collections.Generic;
using Reteno.Android.Utilities;
using UnityEngine;

namespace Reteno.Android
{
    public class CustomDataProxy : AndroidJavaProxy
    {
        private IRetenoCustomDataListener listener;

        public CustomDataProxy(IRetenoCustomDataListener listener) : base("com.reteno.unity.RetenoCustomDataListener")
        {
            this.listener = listener;
        }

        public void onCustomDataReceived(AndroidJavaObject hashMap)
        {
            listener.OnCustomDataReceived(ParseHashMap(hashMap));
        }

        private Dictionary<string, string> ParseHashMap(AndroidJavaObject hashMap)
        {
            Dictionary<string, string> customData = new Dictionary<string, string>();

            AndroidJavaObject entrySet = hashMap.Call<AndroidJavaObject>("entrySet");
            AndroidJavaObject iterator = entrySet.Call<AndroidJavaObject>("iterator");
            AndroidJavaObject entry;

            while (iterator.Call<bool>("hasNext"))
            {
                entry = iterator.Call<AndroidJavaObject>("next");
                string key = entry.Call<string>("getKey");
                string value = entry.Call<string>("getValue");
                customData.Add(key, value);
            }
            
            return customData;
        }
    }
}
