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
+ (void)updateLanguageCode:(NSString *)languageCode;
+ (void)updateTimeZone:(NSString *)timeZone;
+ (void)updateRegion:(NSString *)region;
+ (void)updateTown:(NSString *)town;
+ (void)updateAddress:(NSString *)address;
+ (void)updatePostcode:(NSString *)postcode;
+ (void)updateSubscriptionKeys:(const char **)subscriptionKeys count:(int)count;
+ (void)updateGroupNamesInclude:(const char **)groupNamesInclude count:(int)count;
+ (void)updateGroupNamesExclude:(const char **)groupNamesExclude count:(int)count;
+ (void)updateUserCustomFieldsWithKeys:(const char **)keys values:(const char **)values count:(int)count;
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

void updateLanguageCode(const char *languageCode);

void updateTimeZone(const char *timeZone);

void updateRegion(const char *region);

void updateTown(const char *town);

void updateAddress(const char *address);

void updatePostcode(const char *postcode);

void updateSubscriptionKeys(const char **subscriptionKeys, int count);

void updateGroupNamesInclude(const char **groupNamesInclude, int count);

void updateGroupNamesExclude(const char **groupNamesExclude, int count);

void updateUserCustomFields(const char **keys, const char **values, int count);

void setAnonymous(bool isAnonymous);

void saveUserProfile();


#ifdef __cplusplus
}
#endif
