import Foundation
import Reteno

@objcMembers
public class EventModel: NSObject {
    
    public static func logEvent(eventTypeKey: String, parameters: [EventData.Parameter], forcePush: Bool = false) {
        
        var retenoParameters: [Event.Parameter] = []
       
        for parameter in parameters {
            let convertedParameter = Event.Parameter(name: parameter.name, value: parameter.value)
                  retenoParameters.append(convertedParameter)
        }

        Reteno.logEvent(eventTypeKey: eventTypeKey, parameters: retenoParameters, forcePush: forcePush)
        
        print("Logging event: \(eventTypeKey),Parameters: \(parameters), ForcePush: \(forcePush)")
    }
}
