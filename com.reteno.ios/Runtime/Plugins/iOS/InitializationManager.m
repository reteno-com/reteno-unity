#import "InitializationManager.h"

@implementation InitializationManager


+ (void)startRetenoWithApiKey:(NSString *)apiKey debugMode:(BOOL)debugMode {
    [[InitializationModel shared] startWithApikey:apiKey];
}


+ (void)registerForNotifications{
    [[InitializationModel shared] registerForNotifications];
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

#ifdef __cplusplus
}
#endif
