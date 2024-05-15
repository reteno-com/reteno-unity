using System;
using System.Linq;
using System.Reflection;
using Reteno.Notifications;
using Reteno.User;
using Reteno.Debug;
using UnityEngine;

namespace Reteno
{
    public static class Reteno
    {
        private static RetenoPlatform _platform;
        public static RetenoPlatform Platform
        {
            private get
            {
                if (_platform != null)
                    return _platform;

                string platformTypeName = GetPlatformName();
                if (platformTypeName == null)
                {
                    throw new InvalidOperationException("Unsupported platform");
                }

              AppDomain current = AppDomain.CurrentDomain;
              Assembly[] assemblies = current.GetAssemblies();

              Type platformType = null;
              
              foreach (var assembly in assemblies)
              {
                  if (assembly.FullName.Contains(GetPlatformName()))
                  {
                      Assembly assemblyReteno = Assembly.Load(assembly.FullName);
                      foreach (var type in assemblyReteno.GetTypes())
                      {
                          if (type.FullName != null && type.FullName.Contains(GetTypeName()))
                          {
                              platformType = type;
                          }
                      }
                  }
              }

                if (platformType == null)
                {
                    throw new InvalidOperationException("Platform type not found: " + platformTypeName);
                }
                
                return Activator.CreateInstance(platformType) as RetenoPlatform;
            }
            set => _platform = value;
        }

        public static INotificationsManager Notifications => Platform.Notifications;
        public static IUserManager UserManager => Platform.UserManager;

        public static void Initialize(string appId)
        {
           Platform.Initialize(appId);
        }

        private static string GetPlatformName()
        {
            return ApplicationUtil.Platform switch
            {
                RuntimePlatform.Android => "Reteno.Android",
                RuntimePlatform.IPhonePlayer => "Reteno.iOS",
                _ => null
            };
        }

        private static string GetTypeName()
        {
            return ApplicationUtil.Platform switch
            {
                RuntimePlatform.Android => "RetenoAndroid",
                RuntimePlatform.IPhonePlayer => "RetenoiOS",
                _ => null
            };
        }
    }
}