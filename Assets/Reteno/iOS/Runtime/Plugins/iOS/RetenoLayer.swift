//
//  RetenoLayer.swift
//  TestOBJc
//
//  Created by Oleh Mytsovda on 30.04.2024.
//

import Foundation
import Reteno


@objc public class RetenoLayer: NSObject
{
    @objc public func start(apikey: String) {
        Reteno.start(apiKey: apikey)
    }
    
    @objc public func remoteNotification()
    {
        Reteno.userNotificationService.registerForRemoteNotifications();
    }
    
   @objc public func setUserData(userID: String)
   {
       Reteno.updateUserAttributes(externalUserId: userID);
   }
}
