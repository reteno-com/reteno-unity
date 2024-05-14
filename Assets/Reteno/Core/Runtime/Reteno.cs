using System;
using System.Linq;
using RetenoSDK.Debug;
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
                    SDKDebug.Info($"Reteno set platform SDK{sdk.GetType()}");
                }
                else
                {
                    SDKDebug.Error("Could not find an implementation of Reteno SDK to use!");
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