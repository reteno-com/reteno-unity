import Foundation
import Reteno

public class UserCustomFieldData : NSObject{
    
    public var key: String
    public var value: String
    
    public init(key: String, value: String) {
        self.key = key
        self.value = value
    }
}
