#import <Foundation/Foundation.h>

#ifdef __cplusplus
extern "C" {
#endif

void getRecommendations(const char* recomVariantId,
                        const char** productIds, int productIdsCount,
                        const char* categoryId,
                        const char* filtersJSON,
                        const char** fields, int fieldsCount,
                        void(*callback)(const char*, const char*));

void logRecommendations(const char* recomVariantId,
                        const char** impressionsJSON, int impressionsCount,
                        const char** clicksJSON, int clicksCount);


#ifdef __cplusplus
}
#endif

@interface RecommendationsManager : NSObject

+ (void)getRecommendationsWithVariantId:(NSString *)recomVariantId
                             productIds:(NSArray<NSString *> *)productIds
                             categoryId:(NSString *)categoryId
                            filtersJSON:(NSString *)filtersJSON
                                 fields:(NSArray<NSString *> *)fields
                               callback:(void(^)(NSString * _Nullable jsonResult, NSString * _Nullable error))callback;

+ (void)logRecommendationWithVariantId:(NSString *)recomVariantId
                           impressions:(NSArray<NSString *> *)impressions
                                clicks:(NSArray<NSString *> *)clicks;
@end
