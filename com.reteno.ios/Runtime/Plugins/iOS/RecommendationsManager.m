//
//  InAppManager.m
//  UnityFramework
//
//  Created by Valentyn Rybachuk on 12.09.2024.
//

#import <UnityFramework/UnityFramework-Swift.h>
#import "RecommendationsManager.h"

@implementation RecommendationsManager

+ (void)getRecommendationsWithVariantId:(NSString *)recomVariantId
                             productIds:(NSArray<NSString *> *)productIds
                             categoryId:(NSString *)categoryId
                            filtersJSON:(NSString *)filtersJSON
                                 fields:(NSArray<NSString *> *)fields
                               callback:(void(^)(NSString * _Nullable jsonResult, NSString * _Nullable error))callback {
    [[RecomendationModel shared] getRecommendationsWithRecomVariantId:recomVariantId
                                                           productIds:productIds
                                                           categoryId:categoryId
                                                          filtersJSON:filtersJSON
                                                               fields:fields
                                                     completionHandler:callback];
}

+ (void)logRecommendationWithVariantId:(NSString *)recomVariantId
                           impressions:(NSArray<NSString *> *)impressions
                                clicks:(NSArray<NSString *> *)clicks {
    
    [[RecomendationModel shared] logRecommendations:recomVariantId
                                    impressionsJSON:impressions
                                         clicksJSON:clicks];
}

@end

#ifdef __cplusplus
extern "C" {
#endif

void getRecommendations(const char* recomVariantId,
                        const char** productIds, int productIdsCount,
                        const char* categoryId,
                        const char* filtersJSON,
                        const char** fields, int fieldsCount,
                        void(*callback)(const char*, const char*)) {
    
    NSString *variantIdStr = [NSString stringWithUTF8String:recomVariantId];
    
    NSMutableArray<NSString *> *productIdsArray = [NSMutableArray array];
    for (int i = 0; i < productIdsCount; i++) {
        NSString *productId = [NSString stringWithUTF8String:productIds[i]];
        [productIdsArray addObject:productId];
    }
    
    NSString *categoryIdStr = categoryId ? [NSString stringWithUTF8String:categoryId] : nil;
    NSString *filtersJSONStr = filtersJSON ? [NSString stringWithUTF8String:filtersJSON] : nil;
    
    NSMutableArray<NSString *> *fieldsArray = [NSMutableArray array];
    for (int i = 0; i < fieldsCount; i++) {
        NSString *field = [NSString stringWithUTF8String:fields[i]];
        [fieldsArray addObject:field];
    }
    
    [RecommendationsManager getRecommendationsWithVariantId:variantIdStr
                                                 productIds:productIdsArray
                                                 categoryId:categoryIdStr
                                                filtersJSON:filtersJSONStr
                                                     fields:fieldsArray
                                                   callback:^(NSString * _Nullable jsonResult, NSString * _Nullable error) {
        if (callback) {
            const char *jsonResultCStr = jsonResult ? [jsonResult UTF8String] : NULL;
            const char *errorCStr = error ? [error UTF8String] : NULL;
            callback(jsonResultCStr, errorCStr);
        }
    }];
}
    
    void logRecommendations(const char* recomVariantId,
                            const char** impressionsJSON, int impressionsCount,
                            const char** clicksJSON, int clicksCount) {
        
        NSString *variantIdStr = recomVariantId ? [NSString stringWithUTF8String:recomVariantId] : @"";
        
        NSMutableArray<NSString *> *impressionsArray = [NSMutableArray array];
        for (int i = 0; i < impressionsCount; i++) {
            if (impressionsJSON[i]) {
                NSString *impression = [NSString stringWithUTF8String:impressionsJSON[i]];
                [impressionsArray addObject:impression];
            }
        }
        
        NSMutableArray<NSString *> *clicksArray = [NSMutableArray array];
        for (int i = 0; i < clicksCount; i++) {
            if (clicksJSON[i]) {
                NSString *click = [NSString stringWithUTF8String:clicksJSON[i]];
                [clicksArray addObject:click];
            }
        }
        
        [RecommendationsManager logRecommendationWithVariantId:variantIdStr
                                                   impressions:impressionsArray clicks:clicksArray];
    }

#ifdef __cplusplus
}
#endif
