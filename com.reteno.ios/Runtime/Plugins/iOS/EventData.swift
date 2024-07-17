import Foundation
import Reteno

@objcMembers
public class EventData: NSObject, Codable {
    
    @objc public class Parameter: NSObject, Codable {
        @objc public let name: String
        @objc public let value: String
        
        @objc public init(name: String, value: String) {
            self.name = name
            self.value = value
        }
    }
    
    @objc public let eventTypeKey: String
    @objc public let date: Date
    @objc public let parameters: [Parameter]
    @objc public let id: String
    
    @objc public init(eventTypeKey: String, date: Date, parameters: [Parameter]) {
        self.eventTypeKey = eventTypeKey
        self.date = date
        self.parameters = parameters
        self.id = UUID().uuidString
    }
    
    @objc public func toJSON() -> [String: Any] {
        var json: [String: Any] = [:]
        json["eventTypeKey"] = eventTypeKey
        json["occurred"] = DateFormatter.baseBEDateFormatter.string(from: date)
        if !parameters.isEmpty {
            json["params"] = parameters.map { ["name": $0.name, "value": $0.value] }
        }
        
        return json
    }
}

public extension DateFormatter {
    static let baseBEDateFormatter: DateFormatter = {
        let formatter = DateFormatter()
        formatter.dateFormat = "yyyy-MM-dd'T'HH:mm:ssZ"
        return formatter
    }()
}
