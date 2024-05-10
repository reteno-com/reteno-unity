// NotificationManager.m

#import "NotificationManager.h"
#import <UnityFramework/UnityFramework-Swift.h>

@implementation NotificationManager

+ (instancetype)sharedManager {
    static NotificationManager *sharedManager = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedManager = [[self alloc] init];
    });
    return sharedManager;
}

- (void)startRetenoWithApiKey:(NSString *)apiKey debugMode:(BOOL)debugMode {
    
    RetenoLayer *layer = [[RetenoLayer alloc] init];
    [layer startWithApikey:apiKey];
    [layer remoteNotification];
}

- (void)SetUserDataWithuserId:(NSString *) userId
{
    RetenoLayer *layer = [[RetenoLayer alloc] init];
    [layer setUserDataWithUserID:userId];
}

- (void) registerForNotificationsWithApplication:(UIApplication *)application {
    UNUserNotificationCenter *center = [UNUserNotificationCenter currentNotificationCenter];
    UNAuthorizationOptions options = UNAuthorizationOptionSound | UNAuthorizationOptionAlert | UNAuthorizationOptionBadge;
    [center requestAuthorizationWithOptions:options completionHandler:^(BOOL granted, NSError * _Nullable error) {
        if (granted) {
            dispatch_async(dispatch_get_main_queue(), ^{
                [application registerForRemoteNotifications];
            });
        }
    }];
}

- (void)configureFirebase {
    //[FIRApp configure];
    //[FIRMessaging messaging].delegate = (id<FIRMessagingDelegate>)self;
}

- (void)application:(UIApplication *)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken {
    // Convert NSData to hex string
    NSMutableString *tokenString = [NSMutableString string];
    unsigned char *bytes = (unsigned char *)[deviceToken bytes];
    for (int i = 0; i < [deviceToken length]; i++) {
        [tokenString appendFormat:@"%02x", bytes[i]];
    }
    //[Reteno.userNotificationService processRemoteNotificationsToken:tokenString];
    NSLog(@"Device Token: %@", tokenString);
}

- (void)application:(UIApplication *)application didFailToRegisterForRemoteNotificationsWithError:(NSError *)error {
    NSLog(@"Failed to register for remote notifications: %@", error);
}

- (void)processDeviceToken:(NSData *)deviceToken {
}

@end
