using System;
using System.Collections.Generic;

namespace Reteno.Android.Utilities
{
    public interface IRetenoCustomDataListener
    {
        event Action<Dictionary<string, string>> CustomData;
        void OnCustomDataReceived(Dictionary<string, string> customData);
    }
}
