using System;
using System.Linq;
using RetenoSDK.Notifications;
using RetenoSDK.User;

namespace RetenoSDK
{
    public static class Reteno
    {
        private const string AssemblyFilter = "Reteno";

        private static RetenoPlatform _platform;
        public static RetenoPlatform Platform
        {
            private get
            {
                if (_platform != null)
                    return _platform;

                var availableSDKs = ReflectionHelpers.FindAllAssignableTypes<RetenoPlatform>(AssemblyFilter);
                if (Activator.CreateInstance(availableSDKs.First()) is RetenoPlatform sdk)
                {
                    _platform = sdk;
                }

                return _platform;
            }
            set => _platform = value;
        }

        public static INotificationsManager Notifications => Platform.Notifications;
        public static IUserManager UserManager => Platform.UserManager;

        public static void Initialize(string appId)
        {
            Platform.Initialize(appId);
        }
    }
}