//
//  RetenoInitConfiguration.swift
//  Unity-iPhone
//
//  Created by Valentyn Rybachuk on 26.10.2024.
//
import Reteno

@objc public enum PushNotificationProvider: Int {
    case apns = 0
    case fcm = 1
}

@objc public class RetenoInitConfiguration: NSObject {
    let apiKey: String
    let isAutomaticScreenReportingEnabled: Bool
    let isAutomaticAppLifecycleReportingEnabled: Bool
    let isAutomaticPushSubscriptionReportingEnabled: Bool
    let isAutomaticSessionReportingEnabled: Bool
    let isPausedInAppMessages: Bool
    let inAppMessagesPauseBehaviour: PauseBehaviour
    let isDebugMode: Bool
    let pushNotificationProvider: PushNotificationProvider

    @objc public init(apiKey: String,
                      isAutomaticScreenReportingEnabled: Bool,
                      isAutomaticAppLifecycleReportingEnabled: Bool,
                      isAutomaticPushSubscriptionReportingEnabled: Bool,
                      isAutomaticSessionReportingEnabled: Bool,
                      isPausedInAppMessages: Bool,
                      inAppMessagesPauseBehaviour: Int,
                      isDebugMode: Bool,
                      pushNotificationProvider: String) {
        
        
        let pauseBehaviour: PauseBehaviour
        switch inAppMessagesPauseBehaviour {
        case 0:
            pauseBehaviour = .postponeInApps
        case 1:
            pauseBehaviour = .skipInApps
        default:
            pauseBehaviour = .postponeInApps
        }
        
        self.apiKey = apiKey
        self.isAutomaticScreenReportingEnabled = isAutomaticScreenReportingEnabled
        self.isAutomaticAppLifecycleReportingEnabled = isAutomaticAppLifecycleReportingEnabled
        self.isAutomaticPushSubscriptionReportingEnabled = isAutomaticPushSubscriptionReportingEnabled
        self.isAutomaticSessionReportingEnabled = isAutomaticSessionReportingEnabled
        self.isPausedInAppMessages = isPausedInAppMessages
        self.inAppMessagesPauseBehaviour = pauseBehaviour
        self.isDebugMode = isDebugMode
        
        switch pushNotificationProvider.lowercased() {
        case "apns":
            self.pushNotificationProvider = .apns
        case "fcm":
            self.pushNotificationProvider = .fcm
        default:
            self.pushNotificationProvider = .fcm
        }
    }
}
