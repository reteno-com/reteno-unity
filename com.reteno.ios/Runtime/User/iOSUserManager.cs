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
                UpdateFirstName(user);
                UpdateLastName(user);
                UpdatePhone(user);
                UpdateEmail(user);
            }
            
            saveUserProfile();
        }

        public void SetAnonymousUserAttributes(UserAttributesAnonymous userAttributesAnonymous)
        {
            setAnonymous(true); 
            
            if (string.IsNullOrEmpty(userAttributesAnonymous.FirstName) == false)
                updateFirstName(userAttributesAnonymous.FirstName);
            
            if (string.IsNullOrEmpty(userAttributesAnonymous.LastName) == false)
                updateFirstName(userAttributesAnonymous.LastName);
            
          
            saveUserProfile();
        }

        private void UpdateFirstName(User user)
        {
            if (string.IsNullOrEmpty(user.UserAttributes.FirstName) == false)
                updateFirstName(user.UserAttributes.FirstName);
        }
        
        private void UpdateLastName(User user)
        {
            if (string.IsNullOrEmpty(user.UserAttributes.LastName) == false)
                updateLastName(user.UserAttributes.LastName);
        }
        
        private void UpdatePhone(User user)
        {
            if (string.IsNullOrEmpty(user.UserAttributes.Phone) == false)
                updatePhone(user.UserAttributes.Phone);
        }
        
        private void UpdateEmail(User user)
        {
            if (string.IsNullOrEmpty(user.UserAttributes.Email) == false)
                updateEmail(user.UserAttributes.Email);
        }
    }
}