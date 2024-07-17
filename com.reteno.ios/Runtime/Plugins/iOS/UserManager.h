#import "Reteno-Swift.h"
#import <UnityFramework/UnityFramework-Swift.h>
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <UserNotifications/UserNotifications.h>

@interface UserManager : NSObject

+ (void)updateExternalId:(NSString *)externalId;
+ (void)updateFirstName:(NSString *)firstName;
+ (void)updateLastName:(NSString *)lastName;
+ (void)updatePhone:(NSString *)phone;
+ (void)updateEmail:(NSString *)email;
+ (void)updateIsAnonymous:(BOOL)isAnonymous;
+ (void)saveUser;

@end


#ifdef __cplusplus
extern "C" {
#endif

void updateExternalId(const char *externalId);

void updateFirstName(const char *firstName);

void updateLastName(const char *lastName);

void updatePhone(const char *phone);

void updateEmail(const char *email);

void setAnonymous(bool isAnonymous);

void saveUserProfile();


#ifdef __cplusplus
}
#endif
