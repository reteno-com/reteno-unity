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

        public void AddUserId(string userId)
        {
            UserId = userId;
            SetUserDataWithuserId(userId);
        }
    }
}