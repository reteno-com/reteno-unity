using System.Collections.Generic;
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
        
        public void SetAnonymousUserAttributes(UserAttributesAnonymous userAttributesAnonymous)
        {
            AndroidJavaObject userJava = CreateUserAnonymousObject(userAttributesAnonymous);
            AndroidJavaObject reteno = RetenoJavaInstance.Get();
            reteno.Call("setAnonymousUserAttributes", userJava);
        }
        
        private AndroidJavaObject CreateUserObject(User user)
        {
            var paramsList = new AndroidJavaObject("java.util.ArrayList");
            foreach(Field param in user.UserAttributes.Fields)
            {
                paramsList.Call<bool>("add",GetFieldParameter(param));
            }
            
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
                paramsList
            );
            
            AndroidJavaObject userJava = new AndroidJavaObject(
                "com.reteno.core.domain.model.user.User",
                userAttributesJava,
                GetArrayListFrom(user.SubscriptionKeys),
                GetArrayListFrom(user.GroupNamesInclude),
                GetArrayListFrom(user.GroupNamesExclude)
            );

            return userJava;
        }
        
        AndroidJavaObject GetArrayListFrom(List<string> inputList)
        {
            var paramsList = new AndroidJavaObject("java.util.ArrayList");
            foreach (var element in inputList)
            {
                paramsList.Call<bool>("add", element);
            }

            return paramsList;
        }
        
        private AndroidJavaObject GetFieldParameter(Field field)
        {
            return new AndroidJavaObject("com.reteno.core.domain.model.user.UserCustomField",
                field.Key,
                field.Value
            );
        }
        
        private AndroidJavaObject CreateUserAnonymousObject(UserAttributesAnonymous userAttributesAnonymous)
        {
            var paramsList = new AndroidJavaObject("java.util.ArrayList");
            foreach(Field param in userAttributesAnonymous.Fields)
            {
                paramsList.Call<bool>("add",GetFieldParameter(param));
            }
            
            AndroidJavaObject addressJava = null;
            if (userAttributesAnonymous.Address != null)
            {
                addressJava = new AndroidJavaObject(
                    "com.reteno.core.domain.model.user.Address",
                    userAttributesAnonymous.Address.Region,
                    userAttributesAnonymous.Address.Town,
                    userAttributesAnonymous.Address.Address,
                    userAttributesAnonymous.Address.Postcode
                );
            }

            AndroidJavaObject userAttributesJava = new AndroidJavaObject(
                "com.reteno.core.domain.model.user.UserAttributesAnonymous",
                userAttributesAnonymous.FirstName,
                userAttributesAnonymous.LastName,
                userAttributesAnonymous.LanguageCode,
                userAttributesAnonymous.TimeZone,
                addressJava,
                paramsList
            );
            
            return userAttributesJava;
        }
    }
}