#import "Reteno-Swift.h"
#import <UnityFramework/UnityFramework-Swift.h>
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <UserNotifications/UserNotifications.h>

#ifdef __cplusplus
extern "C" {
    #endif

    void startReteno(const char* apiKey, bool debugMode);
    void registerForNotifications(void);
    void notificationsAddPermissionCallback(void(*callback)(bool));
    void registerNotificationCallback(void(*callback)(const char*));

    #ifdef __cplusplus
}
#endif

@interface InitializationManager : NSObject

+ (void)startRetenoWithApiKey:(NSString *)apiKey debugMode:(BOOL)debugMode;
+ (void)registerForNotifications;
+ (void)delayStartReteno;
+ (void)notificationsAddPermissionCallback:(void(^)(BOOL))callback;
+ (void)registerNotificationCallback:(void(^)(NSDictionary *))callback;
+ (NSString *)serializeDictionaryToJSON:(NSDictionary *)userInfo;

@end

