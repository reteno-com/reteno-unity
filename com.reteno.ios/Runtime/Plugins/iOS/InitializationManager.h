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

    #ifdef __cplusplus
}
#endif

@interface InitializationManager : NSObject

+ (void)startRetenoWithApiKey:(NSString *)apiKey debugMode:(BOOL)debugMode;
+ (void)registerForNotifications;

@end

