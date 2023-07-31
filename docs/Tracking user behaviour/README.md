# Tracking User Behaviour

## Track Custom Events

Reteno SDK provides the ability to track custom events.

```csharp
using System;
using System.Collections.Generic;
using RetenoSdk;

public class YourClass
{
    public void LogCustomEvent()
    {
        var customEvent = new CustomEvent
        {
            EventTypeKey = eventTypeKey,
            Occurred = occurred,
            Params = new List<Param>
            {
                new Param
                {
                    Name = "name0",
                    Value = "value0"
                },
                new Param
                {
                    Name = "name1",
                    Value = "value1"
                }
            }
        };

        RetenoSdk.SendCustomEvent(new List<CustomEvent> {customEvent});
    }
}
```

The CustomEventParameter class structure:

```csharp
public class Param
{
    public string Name { get; set; }
    public string Value { get; set; }
}
```

Note:
- date: The date should be in ISO8601 format.