import Foundation

public class UserAdressData: NSObject {
 
    public var region: String?
    public var town: String?
    public var address: String?
    public var postcode: String?
    
    public init(region: String? = nil, town: String? = nil, address: String? = nil, postcode: String? = nil) {
        self.region = region
        self.town = town
        self.address = address
        self.postcode = postcode
    }
}
