// NotificationManager.h

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <UserNotifications/UserNotifications.h>
//#import <Firebase/Firebase.h>
#import <Reteno/Reteno-Swift.h>

@interface NotificationManager : NSObject

+ (instancetype)sharedManager;

- (void)startRetenoWithApiKey:(NSString *)apiKey debugMode:(BOOL)debugMode;
- (void)registerForNotificationsWithApplication:(UIApplication *)application;
- (void)configureFirebase;
- (void)SetUserDataWithuserId:(NSString *) userId;
- (void)processDeviceToken:(NSData *)deviceToken;

@end
