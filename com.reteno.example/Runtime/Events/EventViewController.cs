using System.Collections.Generic;
using Reteno.Core;
using Reteno.Debug;
using Reteno.Events;
using Reteno.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Reteno.Example.EventController
{
    public class EventViewController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField eventTypeKeyInput;
        [SerializeField] private TMP_InputField yearInput;
        [SerializeField] private TMP_InputField monthInput;
        [SerializeField] private TMP_InputField dayInput;
        [SerializeField] private TMP_InputField hourInput;
        [SerializeField] private TMP_InputField minuteInput;
        [SerializeField] private TMP_InputField secondInput;
        [SerializeField] private Toggle forcePushToggle;

        [SerializeField] private GameObject parametersContainer;
        [SerializeField] private GameObject parameterPrefab;

        [SerializeField] private Button logEventButton;

        private List<Parameter> parameters = new List<Parameter>();

        private void Start()
        {
            logEventButton.onClick.AddListener(OnLogEvent);
        }

        public void AddParameter()
        {
            GameObject newParameter = Instantiate(parameterPrefab, parametersContainer.transform);
            TMP_InputField nameInput = newParameter.transform.Find("NameInput").GetComponent<TMP_InputField>();
            TMP_InputField valueInput = newParameter.transform.Find("ValueInput").GetComponent<TMP_InputField>();

            parameters.Add(new Parameter { Name = nameInput.text, Value = valueInput.text });
        }

        private void OnLogEvent()
        {
            int year = int.Parse(yearInput.text);
            int month = int.Parse(monthInput.text);
            int day = int.Parse(dayInput.text);
            int hour = int.Parse(hourInput.text);
            int minute = int.Parse(minuteInput.text);
            int second = int.Parse(secondInput.text);

            DateAndTime occurredDate =  DateAndTime.Of(year, month, day, hour, minute, second);

            CustomEvent customEvent = new CustomEvent
            {
                EventTypeKey = eventTypeKeyInput.text,
                OccurredDate = occurredDate,
                Parameters = parameters,
                ForcePush = forcePushToggle.isOn
            };

            LogEvent(customEvent);
        }

        private void LogEvent(CustomEvent customEvent)
        {
            SDKDebug.Info("Event Logged: " + customEvent.EventTypeKey);
            RetenoSDK.LogEvent(customEvent);
        }
    }
}