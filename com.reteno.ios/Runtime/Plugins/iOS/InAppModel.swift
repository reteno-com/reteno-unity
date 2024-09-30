import Foundation
import Reteno

@objc public class InAppModel : NSObject {
    @objc public static let shared = InAppModel()
    
    @objc public func setInAppMessagesPauseBehaviour(_ behaviorInt: Int) {
        guard let behavior = PauseBehaviour(rawValue: InAppModel.mapIntToRawValue(behaviorInt)) else {
            print("Invalid pause behavior")
            return
        }
        
        Reteno.setInAppMessagesPauseBehaviour(pauseBehaviour: behavior)
    }
    
    private static func mapIntToRawValue(_ int: Int) -> String {
        switch int {
        case 0:
            return PauseBehaviour.skipInApps.rawValue
        case 1:
            return PauseBehaviour.postponeInApps.rawValue
        default:
            print("Unrecognized behavior integer: \(int)")
            return "" // Handle default case or throw an error
        }
    }
    
    @objc public static func addInAppStatusHandler(_ handler: @escaping (String) -> Void) {
        Reteno.addInAppStatusHandler { status in
            let serializedStatus = serializeStatus(status)
            handler(serializedStatus)
        }
    }
    
    private static func serializeStatus(_ status: InAppMessageStatus) -> String {
        var statusDict = [String: Any]()
        
        switch status {
        case .inAppShouldBeDisplayed:
            statusDict["status"] = "inAppShouldBeDisplayed"
        case .inAppIsDisplayed:
            statusDict["status"] = "inAppIsDisplayed"
        case .inAppShouldBeClosed(let action):
            statusDict["status"] = "inAppShouldBeClosed"
            statusDict["action"] = serializeAction(action)
        case .inAppIsClosed(let action):
            statusDict["status"] = "inAppIsClosed"
            statusDict["action"] = serializeAction(action)
        case .inAppReceivedError(let error):
            statusDict["status"] = "inAppReceivedError"
            statusDict["error"] = error
        }

        if let jsonData = try? JSONSerialization.data(withJSONObject: statusDict, options: []),
           let jsonString = String(data: jsonData, encoding: .utf8) {
            return jsonString
        }
        
        return "{}"
    }
    
    private static func serializeAction(_ action: InAppMessageAction) -> [String: Any] {
        return [
            "isCloseButtonClicked": action.isCloseButtonClicked,
            "isButtonClicked": action.isButtonClicked,
            "isOpenUrlClicked": action.isOpenUrlClicked
        ]
    }
    
    @objc public func setPauseInAppMessages(isPaused: Bool) {
        Reteno.pauseInAppMessages(isPaused: isPaused)
    }
}
