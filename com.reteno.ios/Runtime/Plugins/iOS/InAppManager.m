//
//  InAppManager.m
//  UnityFramework
//
//  Created by Valentyn Rybachuk on 12.09.2024.
//

#import <Foundation/Foundation.h>
#import "InAppManager.h"
#import <UnityFramework/UnityFramework-Swift.h>

@implementation InAppManager

+ (void)setInAppMessagesPauseBehaviour:(int)behaviorInt {
    [[InAppModel shared] setInAppMessagesPauseBehaviour:behaviorInt];
}

+ (void)setPauseInAppMessages:(BOOL)isPaused {
    [[InAppModel shared] setPauseInAppMessagesWithIsPaused:isPaused];
}

+ (void)addInAppStatusCallback:(void (*)(const char *))callback {
    [InAppModel addInAppStatusHandler:^(NSString *statusJSON) {
        if (callback) {
            callback([statusJSON UTF8String]);
        }
    }];
}

@end

#ifdef __cplusplus
extern "C" {
#endif
    
void setInAppMessagesPauseBehaviour(int behaviorInt){
    [InAppManager setInAppMessagesPauseBehaviour:behaviorInt];
}

void setPauseInAppMessages(bool isPaused){
    [InAppManager setPauseInAppMessages:isPaused];
}
    
void addInAppStatusCallback(void(*callback)(const char*)) {
    [InAppManager addInAppStatusCallback:callback];
}

#ifdef __cplusplus
}
#endif
