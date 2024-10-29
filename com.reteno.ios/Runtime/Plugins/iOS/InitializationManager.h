#import "Reteno-Swift.h"
#import <UnityFramework/UnityFramework-Swift.h>
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <UserNotifications/UserNotifications.h>

#ifdef __cplusplus
extern "C" {
#endif


void startRetenoWithConfiguration(const char* apiKey,
                                  bool isAutomaticScreenReportingEnabled,
                                  bool isAutomaticAppLifecycleReportingEnabled,
                                  bool isAutomaticPushSubscriptionReportingEnabled,
                                  bool isAutomaticSessionReportingEnabled,
                                  bool isPausedInAppMessages,
                                  int inAppMessagesPauseBehaviour,
                                  bool isDebugMode);

void registerForNotifications(void);
void notificationsAddPermissionCallback(void(*callback)(bool));
void registerNotificationCallback(void(*callback)(const char*));

#ifdef __cplusplus
}
#endif

@interface InitializationManager : NSObject

+ (void)startRetenoWithApiKey:(NSString *)apiKey
 isAutomaticScreenReportingEnabled:(BOOL)isAutomaticScreenReportingEnabled
 isAutomaticAppLifecycleReportingEnabled:(BOOL)isAutomaticAppLifecycleReportingEnabled
 isAutomaticPushSubscriptionReportingEnabled:(BOOL)isAutomaticPushSubscriptionReportingEnabled
 isAutomaticSessionReportingEnabled:(BOOL)isAutomaticSessionReportingEnabled
 isPausedInAppMessages:(BOOL)isPausedInAppMessages
 inAppMessagesPauseBehaviour:(int)inAppMessagesPauseBehaviour
 isDebugMode:(BOOL)isDebugMode;

+ (void)registerForNotifications;
+ (void)delayStartReteno;
+ (void)notificationsAddPermissionCallback:(void(^)(BOOL))callback;
+ (void)registerNotificationCallback:(void(^)(NSDictionary *))callback;
+ (NSString *)serializeDictionaryToJSON:(NSDictionary *)userInfo;

@end

