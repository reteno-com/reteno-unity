using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Core.Scripts.RetenoSdk.Dto;
using UnityEngine;

namespace Core.Scripts.RetenoSdk.Common
{
    /// <summary>
    /// The reteno data class
    /// </summary>
    public static class RetenoData
    {
        /// <summary>
        /// The interactions
        /// </summary>
        public static List<Interaction> Interactions;

        /// <summary>
        /// The access key
        /// </summary>
        public static string AccessKey;

        /// <summary>
        /// The external user id
        /// </summary>
        public static string ExternalUserId;

        /// <summary>
        /// The device id
        /// </summary>
        public static readonly string DeviceId;

        /// <summary>
        /// The interactions destination lock object
        /// </summary>
        private static readonly object InteractionsDestinationLockObject = new();

        /// <summary>
        /// The config destination lock object
        /// </summary>
        private static readonly object ConfigDestinationLockObject = new();

        /// <summary>
        /// The persistent data path
        /// </summary>
        private static readonly string InteractionsDestination;

        /// <summary>
        /// The persistent data path
        /// </summary>
        private static readonly string ConfigDestination;

        /// <summary>
        /// Initializes a new instance of the <see cref="RetenoData"/> class
        /// </summary>
        static RetenoData()
        {
            InteractionsDestination = Application.persistentDataPath + "/interactions.dat";
            ConfigDestination = Application.persistentDataPath + "/config.dat";
            Interactions = new List<Interaction>();
            DeviceId = SystemInfo.deviceUniqueIdentifier;
            LoadConfigFromFile();
            LoadInteractionsFromFile();

            Debug.Log($"RetenoInteractions loaded: {Interactions.Count}");
        }

        /// <summary>
        /// Sets the configurations using the specified access key
        /// </summary>
        /// <param name="accessKey">The access key</param>
        /// <param name="externalUserId">The external user id</param>
        public static void SetConfigurations(string accessKey, string externalUserId)
        {
            try
            {
                AccessKey = accessKey;
                ExternalUserId = externalUserId;
                SaveConfigToFile();
                Debug.Log("RetenoConfigurations saved");
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                Debug.LogError(exception);
            }
        }

        /// <summary>
        /// Adds the notification using the specified notification id
        /// </summary>
        /// <param name="interactionId">The interaction id</param>
        /// <param name="interactionStatus">The interaction status</param>
        /// <param name="isNeedToRedirect">The is need to redirect</param>
        /// <param name="isSentToApi">The is sent to api</param>
        public static void AddNotification(string interactionId, string interactionStatus, bool isNeedToRedirect, bool isSentToApi)
        {
            try
            {
                var interaction = new Interaction
                {
                    interactionId = interactionId,
                    interactionStatus = interactionStatus,
                    isNeedToRedirect = isNeedToRedirect,
                    isSentToApi = isSentToApi
                };
                Interactions.Add(interaction);
                SaveInteractionsToFile();
                Debug.Log($"RetenoInteractions added: {interactionId}");
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                Debug.LogError(exception);
            }
        }

        /// <summary>
        /// Updates the notification using the specified notification id
        /// </summary>
        /// <param name="interactionId">The notification id</param>
        /// <param name="interactionStatus">The interaction status</param>
        /// <param name="isNeedToRedirect">The is need to redirect</param>
        /// <param name="isSentToApi">The is sent to api</param>
        public static void UpdateNotification(string interactionId, string interactionStatus, bool isNeedToRedirect, bool isSentToApi)
        {
            try
            {
                var interaction = Interactions.FirstOrDefault(x => x.interactionId == interactionId);
                if (interaction != null)
                {
                    interaction.interactionStatus = interactionStatus;
                    interaction.isNeedToRedirect = isNeedToRedirect;
                    interaction.isSentToApi = isSentToApi;
                    SaveInteractionsToFile();
                }

                Debug.Log($"RetenoInteractions updated: {interactionId}");
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                Debug.LogError(exception);
            }
        }

        /// <summary>
        /// Saves the interactions to file
        /// </summary>
        private static void SaveInteractionsToFile()
        {
            lock (InteractionsDestinationLockObject)
            {
                var file = File.Exists(InteractionsDestination)
                    ? File.OpenWrite(InteractionsDestination)
                    : File.Create(InteractionsDestination);

                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(file, Interactions);
                file.Close();
            }
        }

        /// <summary>
        /// Loads the interactions from file
        /// </summary>
        private static void LoadInteractionsFromFile()
        {
            lock (InteractionsDestinationLockObject)
            {
                FileStream file;

                if (File.Exists(InteractionsDestination))
                {
                    file = File.OpenRead(InteractionsDestination);
                }
                else
                {
                    Debug.LogError("RetenoInteractions file not found");
                    return;
                }

                var binaryFormatter = new BinaryFormatter();
                Interactions = (List<Interaction>) binaryFormatter.Deserialize(file);
                file.Close();
            }
        }

        /// <summary>
        /// Saves the config to file
        /// </summary>
        private static void SaveConfigToFile()
        {
            lock (ConfigDestinationLockObject)
            {
                var file = File.Exists(ConfigDestination)
                    ? File.OpenWrite(ConfigDestination)
                    : File.Create(ConfigDestination);

                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(file, new Config
                {
                    accessKey = AccessKey,
                    externalUserId = ExternalUserId
                });
                file.Close();
            }
        }

        /// <summary>
        /// Loads the config from file
        /// </summary>
        private static void LoadConfigFromFile()
        {
            lock (ConfigDestinationLockObject)
            {
                FileStream file;

                if (File.Exists(ConfigDestination))
                {
                    file = File.OpenRead(ConfigDestination);
                }
                else
                {
                    Debug.LogError("RetenoConfig file not found");
                    return;
                }

                var binaryFormatter = new BinaryFormatter();
                var config = (Config) binaryFormatter.Deserialize(file);

                AccessKey = config.accessKey;
                ExternalUserId = config.externalUserId;

                file.Close();
            }
        }
    }
}