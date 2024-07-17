import Foundation
import Reteno

@objc(ProfileModel)
public class ProfileModel: NSObject {
    private var user: UserData
    private var isAnonymous: Bool = false
    
    @objc public override init() {
        self.user = UserData()
    }
    
    @objc public func updateExternalId(_ externalId: String?) {
        guard let externalId = externalId else { return }
        user.id = externalId
    }
    
    @objc public func updateFirstName(_ firstName: String) {
        user.firstName = firstName
    }
    
    @objc public func updateLastName(_ lastName: String) {
        user.lastName = lastName
    }
    
    @objc public func updatePhone(_ phone: String) {
        user.phone = phone
    }
    
    @objc public func updateEmail(_ email: String) {
        user.email = email
    }
    
    @objc public func generateId() -> String {
        return UUID().uuidString
    }
    
    @objc public func updateIsAnonymous(_ isAnonymous: Bool) {
        self.isAnonymous = isAnonymous
    }
    
    @objc public func saveUser() {
        if isAnonymous {
            if user.firstName != nil || user.lastName != nil {
                let attributes = AnonymousUserAttributes(
                    firstName: user.firstName,
                    lastName: user.lastName,
                    timeZone: TimeZone.current.identifier
                )
                Reteno.updateAnonymousUserAttributes(userAttributes: attributes)
            }
        } else {
            let attributes: UserAttributes? = {
                guard
                    user.phone != nil
                    || user.email != nil
                    || user.firstName != nil
                    || user.lastName != nil
                else { return nil }
                
                return .init(phone: user.phone, email: user.email, firstName: user.firstName, lastName: user.lastName)
            }()
            Reteno.updateUserAttributes(externalUserId: user.id, userAttributes: attributes)
        }
    }
}
