#import "Reteno-Swift.h"
#import <UnityFramework/UnityFramework-Swift.h>
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <UserNotifications/UserNotifications.h>

#ifdef __cplusplus
extern "C" {
#endif

void logEvent(const char *eventTypeKey, const char **names, const char **values, int count, bool forcePush);

#ifdef __cplusplus
}
#endif

@interface EventManager : NSObject

+ (void)logEvent:(const char *)eventTypeKey names:(const char **)names values:(const char **)values count:(int)count forcePush:(bool)forcePush;

@end
