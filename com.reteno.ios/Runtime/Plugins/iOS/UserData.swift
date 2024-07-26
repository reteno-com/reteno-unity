import Foundation

public class UserData: NSObject {
    
    public var id: String
    public var firstName: String?
    public var lastName: String?
    public var phone: String?
    public var email: String?
    public var languageCode: String?
    public var timeZone: String?
    
    public init(id: String = "", firstName: String? = nil, lastName: String? = nil, phone: String? = nil, email: String? = nil, languageCode: String? = nil, timeZone: String? = nil) {
        self.id = id
        self.firstName = firstName
        self.lastName = lastName
        self.phone = phone
        self.email = email
        self.languageCode = languageCode
        self.timeZone = timeZone
    }
}
