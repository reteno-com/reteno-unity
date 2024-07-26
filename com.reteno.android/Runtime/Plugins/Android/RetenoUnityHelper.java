package com.reteno.unity;

import com.reteno.core.domain.model.event.Event;
import com.reteno.core.domain.model.event.Parameter;

import java.time.ZonedDateTime;
import java.util.ArrayList;

public class RetenoUnityHelper {

    public static Event.Custom createCustomEvent(
            String typeKey,
            ZonedDateTime dateOccurred,
            ArrayList<Parameter> parameters
    ) {
        return new Event.Custom(typeKey, dateOccurred, parameters);
    }
}
