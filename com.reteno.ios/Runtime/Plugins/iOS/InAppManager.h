//
//  Header.h
//  UnityFramework
//
//  Created by Valentyn Rybachuk on 12.09.2024.
//


@interface InAppManager : NSObject

+ (void)setInAppMessagesPauseBehaviour:(int)behaviorInt;
+ (void)setPauseInAppMessages:(BOOL)isPaused;
+ (void)addInAppStatusCallback:(void (*)(const char *))callback;

@end


#ifdef __cplusplus
extern "C" {
#endif

void setInAppMessagesPauseBehaviour(int behaviorInt);
void setPauseInAppMessages(bool isPaused);
void addInAppStatusCallback(void(*callback)(const char*));

#ifdef __cplusplus
}
#endif
