#import "InitializationManager.h"

@implementation InitializationManager


+ (void)startRetenoWithApiKey:(NSString *)apiKey debugMode:(BOOL)debugMode {
    [[InitializationModel shared] startWithApikey:apiKey];
}

+ (void)registerForNotifications{
    [[InitializationModel shared] registerForNotifications];
}

+ (void)delayStartReteno{
    [[InitializationModel shared] delayStart];
}

+ (void)notificationsAddPermissionCallback:(void (^__strong)(BOOL))callback {
    [[InitializationModel shared] notificationsAddPermissionCallback:callback];
}

+ (void)registerNotificationCallback:(void (^__strong)(NSDictionary *__strong))callback {
    [[InitializationModel shared] didReceiveNotificationUserInfo:callback];
}

+ (NSString *)serializeDictionaryToJSON:(NSDictionary *)userInfo {
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:userInfo options:0 error:&error];
    if (!jsonData) {
        NSLog(@"Failed to serialize dictionary to JSON: %@", error);
        return nil;
    } else {
        NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        return jsonString;
    }
}


@end

#ifdef __cplusplus
extern "C" {
    #endif

void startReteno(const char* apiKey, bool debugMode) {
    NSString *apiKeyString = [NSString stringWithUTF8String:apiKey];

    [InitializationManager startRetenoWithApiKey:apiKeyString debugMode:debugMode];
}

void registerForNotifications(void) {
    [InitializationManager registerForNotifications];
}
    
void notificationsAddPermissionCallback(void(*callback)(bool)){
    [InitializationManager notificationsAddPermissionCallback:^(BOOL granted) {
        callback(granted ? true : false);
    }];
}
    
void registerNotificationCallback(void(*callback)(const char*)){
    [InitializationManager registerNotificationCallback:^(NSDictionary *userInfo) {
            NSString *jsonString = [InitializationManager serializeDictionaryToJSON:userInfo];
            if (jsonString) {
                callback([jsonString UTF8String]);
            }
        }];
}

#ifdef __cplusplus

#endif
