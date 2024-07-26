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

+ (void)updateLanguageCode:(NSString *)languageCode {
    [profileModel updateLanguageCode:languageCode];
}

+ (void)updateAddress:(NSString *)address {
    [profileModel updateAddress:address];
}

+ (void)updatePostcode:(NSString *)postcode {
    [profileModel updatePostcode:postcode];
}

+ (void)updateRegion:(NSString *)region {
    [profileModel updateRegion:region];
}

+ (void)updateTimeZone:(NSString *)timeZone {
    [profileModel updateTimeZone:timeZone];
}

+ (void)updateTown:(NSString *)town {
    [profileModel updateTown:town];
}

+ (void)updateGroupNamesInclude:(const char **)groupNamesInclude count:(int)count {
    
    NSMutableArray *groupNamesArray = [[NSMutableArray alloc] initWithCapacity:count];
    
    for (int i = 0; i < count; i++) {
        NSString *groupName = [NSString stringWithUTF8String:groupNamesInclude[i]];
        [groupNamesArray addObject:groupName];
    }

    [profileModel updateGroupNamesInclude:groupNamesArray];
}

+ (void)updateSubscriptionKeys:(const char **)subscriptionKeys count:(int)count {
   
    NSMutableArray *groupNamesArray = [[NSMutableArray alloc] initWithCapacity:count];
    
    for (int i = 0; i < count; i++) {
        NSString *groupName = [NSString stringWithUTF8String:subscriptionKeys[i]];
        [groupNamesArray addObject:groupName];
    }
    
    [profileModel updateSubscriptionKeys:groupNamesArray];
}

+ (void)updateGroupNamesExclude:(const char **)groupNamesExclude count:(int)count {
   
    NSMutableArray *groupNamesArray = [[NSMutableArray alloc] initWithCapacity:count];
    
    for (int i = 0; i < count; i++) {
        NSString *groupName = [NSString stringWithUTF8String:groupNamesExclude[i]];
        [groupNamesArray addObject:groupName];
    }
    
    [profileModel updateGroupNamesExclude:groupNamesArray];
}

+ (void)updateUserCustomFieldsWithKeys:(const char **)keys values:(const char **)values count:(int)count {
    
    NSMutableArray *parameters = [[NSMutableArray alloc] init];
    for (int i = 0; i < count; i++) {
        if (keys[i] != NULL && values[i] != NULL) {
            NSString *key = [NSString stringWithUTF8String:keys[i]];
            NSString *value = [NSString stringWithUTF8String:values[i]];
            NSDictionary *param = @{@"key": key, @"value": value};
            [parameters addObject:param];
        }
    }
    
    [profileModel updateUserCustomFields:parameters];
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
    
void updateLanguageCode(const char *languageCode){
    [UserManager updateLanguageCode:[NSString stringWithUTF8String:languageCode]];
}
    
void updateTimeZone(const char *timeZone){
    [UserManager updateTimeZone:[NSString stringWithUTF8String:timeZone]];
}
    
void updateRegion(const char *region){
    [UserManager updateRegion:[NSString stringWithUTF8String:region]];
}
    
void updateTown(const char *town){
    [UserManager updateTown:[NSString stringWithUTF8String:town]];
}
    
void updateAddress(const char *address){
    [UserManager updateAddress:[NSString stringWithUTF8String:address]];
}
    
void updatePostcode(const char *postcode){
    [UserManager updatePostcode:[NSString stringWithUTF8String:postcode]];
}
    
void updateSubscriptionKeys(const char **subscriptionKeys, int count){
    [UserManager updateSubscriptionKeys:subscriptionKeys count:count];
}

void updateGroupNamesInclude(const char **groupNamesInclude, int count){
    [UserManager updateGroupNamesInclude:groupNamesInclude count:count];
}

void updateGroupNamesExclude(const char **groupNamesExclude, int count){
    [UserManager updateGroupNamesExclude:groupNamesExclude count:count];
}
    
void updateUserCustomFields(const char **keys, const char **values, int count){
    [UserManager updateUserCustomFieldsWithKeys:keys values:values count:count];
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
