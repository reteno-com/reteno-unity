using Reteno.User;
using UnityEngine;

namespace Reteno.Android.User
{
    public class UserManager : IUserManager
    {
        public string UserId { get; }
      
        public void AddUserId(string userId)
        {
        }
        
        /// <summary>
        /// Sets the user attributes using the specified user
        /// </summary>
        /// <param name="user">The user</param>
        public void SetUserAttributes(string externalUserId, global::Reteno.User.User user)
        {
            AndroidJavaObject userJava = CreateUserObject(user);
            AndroidJavaObject reteno = RetenoJavaInstance.Get();
            reteno.Call("setUserAttributes", externalUserId, userJava);
        }
        
        /// <summary>
        /// Sets the anonymous user attributes using the specified user
        /// </summary>
        /// <param name="user">The user</param>
        public void SetAnonymousUserAttributes(global::Reteno.User.User user)
        {
            AndroidJavaObject userJava = CreateUserObject(user);
            AndroidJavaObject reteno = RetenoJavaInstance.Get();
            reteno.Call("setAnonymousUserAttributes", userJava);
        }
        
        private AndroidJavaObject CreateUserObject(global::Reteno.User.User user)
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