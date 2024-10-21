using System;
using Reteno.Core;
using Reteno.Debug;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Reteno.Example.PushNotification
{
    public class PushNotificationViewController : MonoBehaviour
    {
        [SerializeField] private Button requestPushButton;
        [SerializeField] private Button updateStatusButton;
        [SerializeField] private TMP_Text permissionStatusText;
        [SerializeField] private TMP_Text dataReceivedText;

        void Start()
        {
            requestPushButton.onClick.AddListener(RequestPushPermission);
            updateStatusButton.onClick.AddListener(UpdatePushPermissionStatus);
        }

        private void RequestPushPermission()
        {
            RequestPush(
                onPermissionGranted: () =>
                {
                    permissionStatusText.text = "Permission Granted";
                    permissionStatusText.color = Color.green;
                },
                onPermissionDenied: () =>
                {
                    permissionStatusText.text = "Permission Denied";
                    permissionStatusText.color = Color.red;
                },
                onPermissionDeniedAndDontAskAgain: () =>
                {
                    permissionStatusText.text = "Permission Denied (Don't Ask Again)";
                    permissionStatusText.color = Color.gray;
                },
                onDataReceived: (data) =>
                {
                    dataReceivedText.text = "Data Received: " + data;
                }
            );
        }

        private void RequestPush(Action onPermissionGranted = null,
            Action onPermissionDenied = null, Action onPermissionDeniedAndDontAskAgain = null, Action<string> onDataReceived = null)
        {
            SDKDebug.Info("Requesting Push Notification Permission...");
            RetenoSDK.RequestPushPermission(onPermissionGranted, onPermissionDenied, onPermissionDeniedAndDontAskAgain, onDataReceived);
        }

        private void UpdatePushPermissionStatus()
        {
            SDKDebug.Info("Updating Push Notification Permission Status...");
            RetenoSDK.UpdatePushPermissionStatus();
        }
    }
}