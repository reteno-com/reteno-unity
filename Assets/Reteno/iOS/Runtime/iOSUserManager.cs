using System.Runtime.InteropServices;
using RetenoSDK.User;

namespace RetenoSDK.iOS.User
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