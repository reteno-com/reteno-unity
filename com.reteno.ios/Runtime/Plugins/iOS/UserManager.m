#import "UserManager.h"

@implementation UserManager

static ProfileModel *profileModel = nil;

+ (void)initialize {
    if (self == [UserManager self]) {
        profileModel = [[ProfileModel alloc] init];
    }
}

+ (void)updateExternalId:(NSString *)externalId{
    [profileModel updateExternalId:externalId];
}

+ (void)updateFirstName:(NSString *)firstName {
    [profileModel updateFirstName:firstName];
}

+ (void)updateLastName:(NSString *)lastName {
    [profileModel updateLastName:lastName];
}

+ (void)updatePhone:(NSString *)phone {
    [profileModel updatePhone:phone];
}

+ (void)updateEmail:(NSString *)email {
    [profileModel updateEmail:email];
}

+ (void)updateIsAnonymous:(BOOL)isAnonymous {
    [profileModel updateIsAnonymous:isAnonymous];
}

+ (void)saveUser {
    [profileModel saveUser];
}

@end

#ifdef __cplusplus
extern "C" {
#endif
    
void updateExternalId(const char *externalId){
    [UserManager updateExternalId:[NSString stringWithUTF8String:externalId]];
}

void updateFirstName(const char *firstName) {
    [UserManager updateFirstName:[NSString stringWithUTF8String:firstName]];
}

void updateLastName(const char *lastName) {
    [UserManager updateLastName:[NSString stringWithUTF8String:lastName]];
}

void updatePhone(const char *phone) {
    [UserManager updatePhone:[NSString stringWithUTF8String:phone]];
}

void updateEmail(const char *email) {
    [UserManager updateEmail:[NSString stringWithUTF8String:email]];
}

void setAnonymous(bool isAnonymous) {
    [UserManager updateIsAnonymous:isAnonymous];
}

void saveUserProfile() {
    [UserManager saveUser];
}

#ifdef __cplusplus
}
#endif
