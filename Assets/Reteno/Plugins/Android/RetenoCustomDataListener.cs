using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface RetenoCustomDataListener
{
    void OnCustomDataReceived(Dictionary<string, string> customData);
}
