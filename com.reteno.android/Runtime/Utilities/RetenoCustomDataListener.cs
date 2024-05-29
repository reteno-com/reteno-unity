using System.Collections.Generic;

namespace Reteno.Android.Utilities
{
    public interface RetenoCustomDataListener
    {
        void OnCustomDataReceived(Dictionary<string, string> customData);
    }
}
