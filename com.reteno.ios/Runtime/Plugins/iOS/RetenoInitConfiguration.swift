//
//  ObjCConfiguration.swift
//  Unity-iPhone
//
//  Created by Valentyn Rybachuk on 26.10.2024.
//
import Reteno

@objc public class RetenoInitConfiguration: NSObject {
    let apiKey: String
    let isAutomaticScreenReportingEnabled: Bool
    let isAutomaticAppLifecycleReportingEnabled: Bool
    let isAutomaticPushSubscriptionReportingEnabled: Bool
    let isAutomaticSessionReportingEnabled: Bool
    let isPausedInAppMessages: Bool
    let inAppMessagesPauseBehaviour: PauseBehaviour
    let isDebugMode: Bool

    @objc public init(apiKey: String,
                      isAutomaticScreenReportingEnabled: Bool,
                      isAutomaticAppLifecycleReportingEnabled: Bool,
                      isAutomaticPushSubscriptionReportingEnabled: Bool,
                      isAutomaticSessionReportingEnabled: Bool,
                      isPausedInAppMessages: Bool,
                      inAppMessagesPauseBehaviour: Int,
                      isDebugMode: Bool) {
        
        
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
    }
}
