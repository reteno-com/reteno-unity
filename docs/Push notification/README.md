# Push Notification

## Get Initial Notification

To run a logic after user click on notification, use the `OnNotificationClicked` method.

```csharp
using System;
using RetenoSdk;

public class YourClass : MonoBehaviour
{
    void OnApplicationFocus(bool hasFocus)
    {
        RetenoSdk.OnNotificationClicked();
    }
}
```