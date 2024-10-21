using System.Collections.Generic;
using Reteno.Core;
using Reteno.Debug;
using Reteno.InAppMessages;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Reteno.Example
{
    public class InAppMessagesViewController : MonoBehaviour
    {
        [SerializeField] private TMP_Text customDataText;
        [SerializeField] private Toggle pauseToggle;
        [SerializeField] private TMP_Dropdown behaviourDropdown;
        [SerializeField] private Button sendButton;
        [SerializeField] private TMP_Text statusText;

        private bool _isPaused;
        private InAppPauseBehaviour _selectedBehaviour;

        void Start()
        {
            RetenoSDK.InAppCustomData += OnCustomDataReceived;

            pauseToggle.onValueChanged.AddListener(OnPauseToggleChanged);
            behaviourDropdown.onValueChanged.AddListener(OnBehaviourChanged);
            sendButton.onClick.AddListener(OnSendButtonClicked);

            behaviourDropdown.options.Clear();
            behaviourDropdown.options.Add(new TMP_Dropdown.OptionData(InAppPauseBehaviour.SKIP_IN_APPS.ToString()));
            behaviourDropdown.options.Add(new TMP_Dropdown.OptionData(InAppPauseBehaviour.POSTPONE_IN_APPS.ToString()));
            behaviourDropdown.value = (int)InAppPauseBehaviour.POSTPONE_IN_APPS;

            _isPaused = pauseToggle.isOn;
            _selectedBehaviour = (InAppPauseBehaviour)behaviourDropdown.value;
        }

        private void OnCustomDataReceived(Dictionary<string, string> data)
        {
            customDataText.text = "Custom Data:\n";
            foreach (var kvp in data)
            {
                customDataText.text += $"{kvp.Key}: {kvp.Value}\n";
            }
        }

        private void OnPauseToggleChanged(bool isPausedValue)
        {
            _isPaused = isPausedValue;
        }

        private void OnBehaviourChanged(int behaviourIndex)
        {
            _selectedBehaviour = (InAppPauseBehaviour)behaviourIndex;
        }

        private void OnSendButtonClicked()
        {
            RetenoSDK.PauseInAppMessages(_isPaused);
            RetenoSDK.SetInAppMessagesPauseBehaviour(_selectedBehaviour);

            string pauseStatus = _isPaused ? "Paused" : "Unpaused";
            string behaviourStatus = _selectedBehaviour.ToString();
            statusText.text = $"Status: {pauseStatus}, Behaviour: {behaviourStatus}";
            SDKDebug.Info($"In-App Messages Status: {pauseStatus}, Behaviour: {behaviourStatus}");
        }
    }
}