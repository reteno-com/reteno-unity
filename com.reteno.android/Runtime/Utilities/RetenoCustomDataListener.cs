using System;
using System.Collections.Generic;

namespace Reteno.Android.Utilities
{
    public class RetenoCustomDataListener : IRetenoCustomDataListener
    {
        public event Action<Dictionary<string, string>> CustomData;

        public void OnCustomDataReceived(Dictionary<string, string> customData)
        {
            CustomData?.Invoke(customData);
        }
    }
}