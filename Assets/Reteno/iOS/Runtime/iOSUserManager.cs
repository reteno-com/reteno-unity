using System.Runtime.InteropServices;
using Reteno.User;

namespace Reteno.iOS.User
{
    internal sealed class iOSUserManager : IUserManager
    {
        private static iOSUserManager _instance;
        public iOSUserManager() => _instance = this;

        public string UserId { get; private set; }
      
        [DllImport("__Internal")]
        private static extern void SetUserDataWithuserId(string userId);
      
        public void SetUserAttributes(string externalUserId, global::Reteno.User.User user)
        {
            
        }

        public void SetAnonymousUserAttributes(global::Reteno.User.User user)
        {
        }
    }
}