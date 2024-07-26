#import "EventManager.h"

@implementation EventManager

+ (void)logEvent:(const char *)eventTypeKey names:(const char **)names values:(const char **)values count:(int)count forcePush:(bool)forcePush {

    NSString *eventTypeKeyString = [NSString stringWithUTF8String:eventTypeKey];
    
    NSDateFormatter *formatter = [[NSDateFormatter alloc] init];
    [formatter setDateFormat:@"yyyy-MM-dd'T'HH:mm:ssZ"];
    
    NSMutableArray *parameters = [[NSMutableArray alloc] init];
    for (int i = 0; i < count; i++) {
        NSString *name = [NSString stringWithUTF8String:names[i]];
        NSString *value = [NSString stringWithUTF8String:values[i]];
        NSDictionary *param = @{@"name": name, @"value": value};
        [parameters addObject:param];
    }

    NSLog(@"Logging Event: %@", eventTypeKeyString);
    NSLog(@"Parameters: %@", parameters);
    
    if (forcePush) {
        NSLog(@"Force push is enabled");
    }

    
    [EventModel logEventWithEventTypeKey:eventTypeKeyString
                             parameters:parameters
                             forcePush:forcePush];
}

@end

#ifdef __cplusplus
extern "C" {
#endif
    
    void logEvent(const char *eventTypeKey, const char **names, const char **values, int count, bool forcePush) {
        @autoreleasepool {
            [EventManager logEvent:eventTypeKey names:names values:values count:count forcePush:forcePush];
        
        }
    }
    
#ifdef __cplusplus
}
#endif

