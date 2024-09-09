import Foundation
import Reteno
import Firebase
import FirebaseMessaging

@objc public class InitializationModel : NSObject, MessagingDelegate
{
    @objc public static let shared = InitializationModel()
    private var notificationPermissionCallback: ((Bool) -> Void)?
    
    private override init() {
        super.init()
            Messaging.messaging().delegate = self
    }
    
    @objc public func start(apikey: String)
    {
        Reteno.delayedSetup(apiKey: apikey)
    }
    
    @objc public func delayStart()
    {
        if FirebaseApp.app() == nil {
                FirebaseApp.configure()
                print("Configured Firebase")
            } else {
                print("Firebase has already been configured.")
            }
        
        Reteno.delayedStart();
    }
    
    @objc public func registerForNotifications()
    {
        Reteno.userNotificationService.registerForRemoteNotifications {[weak self] granted in
            self?.notificationPermissionCallback?(granted)
        };
    }
    
    @objc public func didReceiveNotificationUserInfo(_ callback: @escaping ([AnyHashable: Any]) -> Void)
    {
        Reteno.userNotificationService.didReceiveNotificationUserInfo = callback
    }
    
    @objc public func notificationsAddPermissionCallback(_ callback: @escaping (Bool) -> Void)
    {
        self.notificationPermissionCallback = callback
    }

    @objc public func messaging(_ messaging: Messaging, didReceiveRegistrationToken fcmToken: String?) {
      
        guard let fcmToken = Messaging.messaging().fcmToken else { return }
        
        print("FCM device token: ", fcmToken)
        
        Reteno.userNotificationService.processRemoteNotificationsToken(fcmToken)
    }
}
