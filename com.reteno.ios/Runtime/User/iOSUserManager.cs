using System.Linq;
using System.Runtime.InteropServices;
using Reteno.Users;

namespace Reteno.iOS.Users
{
    public class iOSUserManager : IUserManager
    {
        public string UserId { get; private set; }
        
        [DllImport("__Internal")]
        private static extern void updateExternalId(string externalId);
        
        [DllImport("__Internal")]
        private static extern void updateFirstName(string firstName);

        [DllImport("__Internal")]
        private static extern void updateLastName(string lastName);

        [DllImport("__Internal")]
        private static extern void updatePhone(string phone);

        [DllImport("__Internal")]
        private static extern void updateEmail(string email);

        [DllImport("__Internal")]
        private static extern void updateLanguageCode(string languageCode);

        [DllImport("__Internal")]
        private static extern void updateTimeZone(string timeZone);

        [DllImport("__Internal")]
        private static extern void updateRegion(string region);

        [DllImport("__Internal")]
        private static extern void updateTown(string town);

        [DllImport("__Internal")]
        private static extern void updateAddress(string address);

        [DllImport("__Internal")]
        private static extern void updatePostcode(string postcode);
        
        [DllImport("__Internal")]
        private static extern void updateSubscriptionKeys(string[]subscriptionKeys, int count);

        [DllImport("__Internal")]
        private static extern void updateGroupNamesInclude(string[]groupNamesInclude, int count);

        [DllImport("__Internal")]
        private static extern void updateGroupNamesExclude(string[]groupNamesExclude, int count);

        [DllImport("__Internal")]
        private static extern void updateUserCustomFields(string[] keys, string[] values, int count);

        [DllImport("__Internal")]
        private static extern void setAnonymous(bool isAnonymous);

        [DllImport("__Internal")]
        private static extern void saveUserProfile();
        
        public void SetUserAttributes(string externalUserId, User user)
        {
            setAnonymous(false);
            
            UserId = externalUserId;
            updateExternalId(externalUserId);
            
            if (user is { UserAttributes: not null })
            {
                UpdateFirstName(user.UserAttributes.FirstName);
                UpdateLastName(user.UserAttributes.LastName);
                UpdatePhone(user.UserAttributes.Phone);
                UpdateEmail(user.UserAttributes.Email);
                UpdateLanguageCode(user.UserAttributes.LanguageCode);
                UpdateTimeZone(user.UserAttributes.TimeZone);
                
                if (user.UserAttributes.Address != null)
                {
                    UpdateRegion(user.UserAttributes.Address.Region);
                    UpdateTown(user.UserAttributes.Address.Town);
                    UpdateAddress(user.UserAttributes.Address.Address);
                    UpdatePostcode(user.UserAttributes.Address.Postcode);
                }
                
                UpdateUserCustomFields(user.UserAttributes.Fields.ToArray());
                UpdateGroupNamesExclude(user.GroupNamesExclude.ToArray());
                UpdateGroupNamesInclude(user.GroupNamesInclude.ToArray());
                UpdateSubscriptionKeys(user.SubscriptionKeys.ToArray());
            }
            
            saveUserProfile();
        }

        public void SetAnonymousUserAttributes(UserAttributesAnonymous userAttributesAnonymous)
        {
            setAnonymous(true); 
            
            if (userAttributesAnonymous != null)
            {
                UpdateFirstName(userAttributesAnonymous.FirstName);
                UpdateLastName(userAttributesAnonymous.LastName);
                UpdateLanguageCode(userAttributesAnonymous.LanguageCode);
                UpdateTimeZone(userAttributesAnonymous.TimeZone);
                
                if (userAttributesAnonymous.Address != null)
                {
                    UpdateRegion(userAttributesAnonymous.Address.Region);
                    UpdateTown(userAttributesAnonymous.Address.Town);
                    UpdateAddress(userAttributesAnonymous.Address.Address);
                    UpdatePostcode(userAttributesAnonymous.Address.Postcode);
                }
                
                UpdateUserCustomFields(userAttributesAnonymous.Fields.ToArray());
               
            }

            saveUserProfile();
        }

    
        private static void UpdateFirstName(string firstName)
        {
            if (string.IsNullOrEmpty(firstName) == false)
                updateFirstName(firstName);
        }
        
        private static void UpdateLastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName) == false)
                updateLastName(lastName);
        }
        
        private static void UpdatePhone(string phone)
        {
            if (string.IsNullOrEmpty(phone) == false)
                updatePhone(phone);
        }
        
        private static void UpdateEmail(string email)
        {
            if (string.IsNullOrEmpty(email) == false)
                updateEmail(email);
        }

        private static void UpdateLanguageCode(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode) == false)
                updateLanguageCode(languageCode);
        }

        private static void UpdateTimeZone(string timeZone)
        {
            if (string.IsNullOrEmpty(timeZone) == false)
                updateTimeZone(timeZone);
        }

        private static void UpdateRegion(string region)
        {
            if (string.IsNullOrEmpty(region) == false)
                updateRegion(region);
        }

        private static void UpdateTown(string town)
        {
            if (string.IsNullOrEmpty(town) == false)
                updateTown(town);
        }

        private static void UpdateAddress(string address)
        {
            if (string.IsNullOrEmpty(address) == false)
                updateAddress(address);
        }

        private static void UpdatePostcode(string postcode)
        {
            if (string.IsNullOrEmpty(postcode) == false)
                updatePostcode(postcode);
        }

        private static void UpdateSubscriptionKeys(string[] subscriptionKeys)
        {
            if (subscriptionKeys.Length > 0)
                updateSubscriptionKeys(subscriptionKeys, subscriptionKeys.Length);
        }

        private static void UpdateGroupNamesInclude(string[] groupNamesInclude)
        {
            if(groupNamesInclude.Length > 0)
                updateGroupNamesInclude(groupNamesInclude, groupNamesInclude.Length);
        }

        private static void UpdateGroupNamesExclude(string[] groupNamesExclude)
        {
            if(groupNamesExclude.Length > 0)
                updateGroupNamesExclude(groupNamesExclude, groupNamesExclude.Length);
        }
        
        private static void UpdateUserCustomFields(Field[] fields)
        {
            if (fields == null) return;
            if (fields.Length > 0)
            {
                string[] keys = fields.Select(field => field.Key).ToArray();
                string[] values = fields.Select(field => field.Value).ToArray();
                int count = fields.Length;
                
                updateUserCustomFields(keys, values, count);
            }
        }
    }
}