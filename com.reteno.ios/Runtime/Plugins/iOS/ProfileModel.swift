import Foundation
import Reteno

@objc(ProfileModel)
public class ProfileModel: NSObject {
    
    private var user: UserData
    private var userAdress: UserAdressData
    private var userCustomField: [UserCustomFieldData]
    private var subscriptionKeys: [String]
    private var groupNamesInclude: [String]
    private var groupNamesExclude: [String]
    
    private var isAnonymous: Bool = false
    
    @objc public override init() {
        self.user = UserData()
        self.userAdress = UserAdressData()
        self.userCustomField = []
        self.subscriptionKeys = []
        self.groupNamesInclude = []
        self.groupNamesExclude = []
    }
    
    @objc public func updateExternalId(_ externalId: String?) {
        guard let externalId = externalId else {
               return
           }
        
        user.id = externalId
    }
    
    @objc public func updateFirstName(_ firstName: String?) {
        user.firstName = firstName
    }
    
    @objc public func updateLastName(_ lastName: String?) {
        user.lastName = lastName
    }
    
    @objc public func updatePhone(_ phone: String?) {
        user.phone = phone
    }
    
    @objc public func updateEmail(_ email: String?) {
        user.email = email
    }
    
    @objc public func updateLanguageCode(_ languageCode: String?){
        user.languageCode = languageCode
    }
    
    @objc public func updateTimeZone(_ timeZone: String?){
        user.timeZone = timeZone
    }
    
    @objc public func updateRegion(_ region: String){
        userAdress.region = region
    }
    
    @objc public func updateTown(_ town: String){
        userAdress.town = town
    }
    
    @objc public func updateAddress(_ address: String){
        userAdress.address = address
    }
    
    @objc public func updatePostcode(_ postcode: String){
        userAdress.postcode = postcode
    }
    
    @objc public func updateSubscriptionKeys(_ subscriptionKeys: [String]){
        self.subscriptionKeys = subscriptionKeys
    }
    
    @objc public func updateGroupNamesInclude(_ groupNamesInclude: [String]){
        self.groupNamesInclude = groupNamesInclude
    }
    
    @objc public func updateGroupNamesExclude(_ groupNamesExclude: [String]){
        self.groupNamesExclude = groupNamesExclude
    }
    
    @objc public func updateUserCustomFields(_ parameters: [NSDictionary]){
        userCustomField.removeAll()
        var retenoParameters: [UserCustomFieldData] = []
        for dict in parameters {
            if let key = dict["key"] as? String, let value = dict["value"] as? String {
                let convertedParameter = UserCustomFieldData(key: key, value: value)
                retenoParameters.append(convertedParameter)
            }
        }
        userCustomField = retenoParameters
    }
    
    @objc public func generateId() -> String {
        return UUID().uuidString
    }
    
    @objc public func updateIsAnonymous(_ isAnonymous: Bool) {
        self.isAnonymous = isAnonymous
    }
    
    @objc public func saveUser() {
        var address = Address()
        var userCustomFields = [UserCustomField]()
        
        for item in userCustomField {
            userCustomFields.append(UserCustomField(key: item.key, value: item.value))
        }
      
        if(userAdress.region != nil || userAdress.town != nil || userAdress.address != nil || userAdress.postcode != nil) {
            address = Address(
                region: userAdress.region,
                town: userAdress.town,
                address: userAdress.address,
                postcode: userAdress.postcode
            )
        }
        
        if isAnonymous {
            if user.firstName != nil || user.lastName != nil {
                let attributes = AnonymousUserAttributes(
                    firstName: user.firstName,
                    lastName: user.lastName,
                    languageCode: user.languageCode,
                    timeZone: TimeZone.current.identifier,
                    address: address,
                    fields: userCustomFields
                )
                Reteno.updateAnonymousUserAttributes(userAttributes: attributes, subscriptionKeys: subscriptionKeys, groupNamesInclude: groupNamesInclude, groupNamesExclude: groupNamesExclude)
            }
        } else {
            let attributes: UserAttributes? = {
                guard user.phone != nil || user.email != nil || user.firstName != nil || user.lastName != nil else { return nil }
                
                return .init(phone: user.phone, email: user.email, firstName: user.firstName, lastName: user.lastName, languageCode: user.languageCode, timeZone: user.timeZone, address: address, fields: userCustomFields)
            }()
            
            guard let userAttributes = attributes else { return }
            
            Reteno.updateUserAttributes(externalUserId: user.id, userAttributes: userAttributes, subscriptionKeys: subscriptionKeys, groupNamesInclude: groupNamesInclude, groupNamesExclude: groupNamesExclude)
            }
        }
    }
