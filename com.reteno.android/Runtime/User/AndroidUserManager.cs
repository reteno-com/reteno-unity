using Reteno.Users;
using UnityEngine;

namespace Reteno.Android.Users
{
    /// <summary>
    /// Manages User Data
    /// </summary>
    public class AndroidUserManager : IUserManager
    {
        public string UserId { get; private set; } 
        
        public void SetUserAttributes(string externalUserId, User user)
        {
            UserId = externalUserId;
            
            AndroidJavaObject userJava = CreateUserObject(user);
            AndroidJavaObject reteno = RetenoJavaInstance.Get();
            reteno.Call("setUserAttributes", externalUserId, userJava);
        }
        
        public void SetAnonymousUserAttributes(User user)
        {
            AndroidJavaObject userJava = CreateUserObject(user);
            AndroidJavaObject reteno = RetenoJavaInstance.Get();
            reteno.Call("setAnonymousUserAttributes", userJava);
        }
        
        private AndroidJavaObject CreateUserObject(User user)
        {
            AndroidJavaObject addressJava = null;
            if (user.UserAttributes.Address != null)
            {
                addressJava = new AndroidJavaObject(
                    "com.reteno.core.domain.model.user.Address",
                    user.UserAttributes.Address.Region,
                    user.UserAttributes.Address.Town,
                    user.UserAttributes.Address.Address,
                    user.UserAttributes.Address.Postcode
                );
            }

            AndroidJavaObject userAttributesJava = new AndroidJavaObject(
                "com.reteno.core.domain.model.user.UserAttributes",
                user.UserAttributes.Phone,
                user.UserAttributes.Email,
                user.UserAttributes.FirstName,
                user.UserAttributes.LastName,
                user.UserAttributes.LanguageCode,
                user.UserAttributes.TimeZone,
                addressJava,
                user.UserAttributes.Fields
            );
            
            AndroidJavaObject userJava = new AndroidJavaObject(
                "com.reteno.core.domain.model.user.User",
                userAttributesJava,
                user.SubscriptionKeys,
                user.GroupNamesInclude,
                user.GroupNamesExclude
            );

            return userJava;
        }
    }
}