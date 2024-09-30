using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using Reteno.Debug;
using Reteno.InAppMessages;
using Reteno.Utilities;

namespace Reteno.iOS.InAppMessages
{
    public class iOSInAppMessagesManager : IInAppMessagesManager
    {
        private static iOSInAppMessagesManager currentInstance;
        
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        private static extern void setInAppMessagesPauseBehaviour(int behaviorInt);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        private static extern void setPauseInAppMessages(bool isPaused);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        private static extern void addInAppStatusCallback(InappStatusCallback callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void InappStatusCallback(string statusJSON);
        private static InappStatusCallback statusCallback;

        public event Action<Dictionary<string, string>> CustomData;
        
        public iOSInAppMessagesManager()
        {
            currentInstance = this;  // Set the current instance
        }

        public void Initialize()
        {
            statusCallback = OnInAppStatusReceived;
            addInAppStatusCallback(statusCallback);
        }
        
        public void SetInAppMessagesPauseBehaviour(InAppPauseBehaviour behaviour)
        {
            setInAppMessagesPauseBehaviour((int)behaviour);
        }
        
        public void PauseInAppMessages(bool isPaused)
        {
            setPauseInAppMessages(isPaused);
        }

        [MonoPInvokeCallback(typeof(InappStatusCallback))]
        private static void OnInAppStatusReceived(string statusJSON)
        {
            var jsonObject = Json.Deserialize(statusJSON) as Dictionary<string, object>;
            if (jsonObject == null)
            {
                SDKDebug.Error("Failed to parse JSON");
                return;
            }

            if (!jsonObject.TryGetValue("status", out var statusValue))
            {
                SDKDebug.Error("JSON does not contain 'status' field");
                return;
            }

            string status = statusValue as string;

            switch (status)
            {
                case "inAppShouldBeDisplayed":
                    // Handle the in-app message display logic
                    break;

                case "inAppIsDisplayed":
                    // Handle logic for when the in-app message is displayed
                    break;

                case "inAppShouldBeClosed":
                case "inAppIsClosed":
                    if (jsonObject.TryGetValue("action", out var actionValue) && actionValue is Dictionary<string, object> actionDict)
                    {
                        // Convert actionDict to Dictionary<string, string>
                        var actionData = new Dictionary<string, string>();
                        foreach (var kvp in actionDict)
                        {
                            actionData[kvp.Key] = kvp.Value.ToString();
                        }

                        // Raise the event
                        currentInstance.CustomData?.Invoke(actionData);
                    }
                    else
                    {
                        SDKDebug.Warn("Action data is missing or invalid");
                    }
                    break;

                case "inAppReceivedError":
                    if (jsonObject.TryGetValue("error", out var errorValue))
                    {
                        string error = errorValue as string;
                        SDKDebug.Error($"In-app message error: {error}");
                    }
                    else
                    {
                        SDKDebug.Warn("Error data is missing");
                    }
                    break;

                default:
                    SDKDebug.Warn($"Unknown status: {status}");
                    break;
            }
        }

    }
}