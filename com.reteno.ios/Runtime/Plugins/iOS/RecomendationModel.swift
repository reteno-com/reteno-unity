import Foundation
import Reteno

@objc public class RecomendationModel: NSObject {
    @objc public static let shared = RecomendationModel()
    
    private var recommendationCallback: ((String?, String?) -> Void)?

    @objc public func getRecommendations(
        recomVariantId: String,
        productIds: [String],
        categoryId: String?,
        filtersJSON: String?,
        fields: [String]?,
        completionHandler: @escaping (String?, String?) -> Void
    ) {
        self.recommendationCallback = completionHandler

        var recomFilters: [RecomFilter]? = nil
        if let filtersJSON = filtersJSON,
           let data = filtersJSON.data(using: .utf8) {
            do {
                let decoder = JSONDecoder()
                let recomFilterDataArray = try decoder.decode([RecomFilterData].self, from: data)
                recomFilters = recomFilterDataArray.map { $0.toRecomFilter() }
            } catch {
                print("Не вдалося розпарсити JSON фільтрів: \(error)")
                completionHandler(nil, "Не вдалося розпарсити JSON фільтрів: \(error)")
                return
            }
        }
        
        Reteno.recommendations().getRecomJSONs(
            recomVariantId: recomVariantId,
            productIds: productIds,
            categoryId: categoryId,
            filters: recomFilters,
            fields: fields
        ) { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .success(let recommendations):
                do {
                    let data = try JSONSerialization.data(withJSONObject: recommendations, options: [])
                    let jsonString = String(data: data, encoding: .utf8)
                    self.recommendationCallback?(jsonString, nil)
                } catch {
                    self.recommendationCallback?(nil, "Помилка серіалізації рекомендацій у JSON")
                }
            case .failure(let error):
                self.recommendationCallback?(nil, error.localizedDescription)
            }
            self.recommendationCallback = nil
        }
    }
    
    @objc public func logRecommendations(_ variantId: String,
                                            impressionsJSON: [String],
                                            clicksJSON: [String]) {
           let decoder = JSONDecoder()
           decoder.dateDecodingStrategy = .iso8601
           
           var impressions: [RecomEvent] = []
           for impressionJSON in impressionsJSON {
               if let data = impressionJSON.data(using: .utf8) {
                   do {
                       let event = try decoder.decode(RecomEvent.self, from: data)
                       impressions.append(event)
                   } catch {
                       print("Не вдалося десеріалізувати impression: \(error.localizedDescription)")
                   }
               } else {
                   print("Невірний JSON рядок в impressions.")
               }
           }
           

           var clicks: [RecomEvent] = []
           for clickJSON in clicksJSON {
               if let data = clickJSON.data(using: .utf8) {
                   do {
                       let event = try decoder.decode(RecomEvent.self, from: data)
                       clicks.append(event)
                   } catch {
                       print("Не вдалося десеріалізувати click: \(error.localizedDescription)")
                   }
               } else {
                   print("Невірний JSON рядок в clicks.")
               }
           }
           
        
           Reteno.recommendations().logEvent(recomVariantId: variantId, impressions: impressions, clicks: clicks)
           
       }
    
    struct RecomFilterData: Codable {
        let name: String
        let values: [String]
        
        func toRecomFilter() -> RecomFilter {
            return RecomFilter(name: name, values: values)
        }
    }
    
}
