import Foundation
import Reteno
import Firebase
import FirebaseMessaging

@objc public class InitializationModel : NSObject, MessagingDelegate
{
    @objc public static let shared = InitializationModel()
    
    private override init() {
        super.init()
        
        if FirebaseApp.app() == nil {
                FirebaseApp.configure()
                print("Configured Firebase")
            } else {
                print("Firebase has already been configured.")
            }
            
            Messaging.messaging().delegate = self
    }
    
    @objc public func start(apikey: String)
    {
        Reteno.start(apiKey: apikey)
    }
    
    @objc public func registerForNotifications()
    {
        Reteno.userNotificationService.registerForRemoteNotifications { granted in
            //let grantedString = String(granted)
            //self.sendUnityMessage(gameObject: "iOSPushNotificationPermissionManager", methodName: "ChangePermissionStatus", message: //grantedString)
        };
    }
    
   @objc func sendUnityMessage(gameObject: String, methodName: String, message: String) {
        UnityFramework.getInstance()?.sendMessageToGO(
            withName: gameObject,
            functionName: methodName,
            message: message
        )
    }
    @objc public func messaging(_ messaging: Messaging, didReceiveRegistrationToken fcmToken: String?) {
      
        guard let fcmToken = Messaging.messaging().fcmToken else { return }
        
        print("FCM device token: ", fcmToken)
        
        Reteno.userNotificationService.processRemoteNotificationsToken(fcmToken)
    }
}
