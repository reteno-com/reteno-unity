import Foundation
import Reteno

@objcMembers
public class EventModel: NSObject {
    
    public static func logEvent(eventTypeKey: String, parameters: [NSDictionary], forcePush: Bool = false) {
     
        var retenoParameters: [Event.Parameter] = []
        for dict in parameters {
            if let name = dict["name"] as? String, let value = dict["value"] as? String {
                let convertedParameter = Event.Parameter(name: name, value: value)
                retenoParameters.append(convertedParameter)
            }
        }
        

        Reteno.logEvent(eventTypeKey: eventTypeKey, parameters: retenoParameters, forcePush: forcePush)
        print("Logging event: \(eventTypeKey), Parameters: \(retenoParameters), Force Push: \(forcePush)")
    }
}
