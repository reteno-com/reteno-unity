using System.Collections.Generic;
using Reteno.Debug;
using Reteno.Users;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Reteno.Example.UserController
{
    public class UserInputView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField firstNameInput;
        [SerializeField] private TMP_InputField lastNameInput;
        [SerializeField] private TMP_InputField emailInput;
        [SerializeField] private TMP_InputField phoneInput; 
        
        [SerializeField] private TMP_InputField regionInput;
        [SerializeField] private TMP_InputField townInput;
        [SerializeField] private TMP_InputField addressInput;
        [SerializeField] private TMP_InputField postcodeInput;

        [SerializeField] private TMP_Dropdown languageDropdown;
        [SerializeField] private TMP_Dropdown timezoneDropdown;

        [SerializeField] private GameObject fieldsContainer; // Container for dynamic user fields
        [SerializeField] private GameObject fieldPrefab; // Prefab with two TMP_InputFields for Key and Value

        [SerializeField] private Button submitButton;
        [SerializeField] private TMP_Text submitButtonText;
        [SerializeField] private Toggle anonymousToggle;

        private bool isAnonymous = false;
        private UserAttributes userAttributes;
        private UserAttributesAnonymous anonymousAttributes;

        void Start()
        {
            anonymousToggle.onValueChanged.AddListener(OnToggleAnonymous);
            submitButton.onClick.AddListener(OnSubmit);

            UpdateSubmitButtonText();
        }

        private void OnToggleAnonymous(bool isAnonymousUser)
        {
            isAnonymous = isAnonymousUser;

            emailInput.gameObject.SetActive(!isAnonymous);
            phoneInput.gameObject.SetActive(!isAnonymous);

            UpdateSubmitButtonText();
        }

        private void UpdateSubmitButtonText()
        {
            if (isAnonymous)
            {
                submitButtonText.text = "Send Anonymous User";
            }
            else
            {
                submitButtonText.text = "Send User";
            }
        }

        private void OnSubmit()
        {
            if (isAnonymous)
            {
                SubmitAnonymous();
            }
            else
            {
                SubmitUser();
            }
        }

        private void SubmitUser()
        {
            userAttributes = new UserAttributes
            {
                FirstName = firstNameInput.text,
                LastName = lastNameInput.text,
                Email = emailInput.text,
                Phone = phoneInput.text,
                Address = new UserAddress
                {
                    Region = regionInput.text,
                    Town = townInput.text,
                    Address = addressInput.text,
                    Postcode = postcodeInput.text
                },
                LanguageCode = languageDropdown.options[languageDropdown.value].text,
                TimeZone = timezoneDropdown.options[timezoneDropdown.value].text,
                Fields = GetDynamicFields()
            };

            SDKDebug.Info("User Attributes Submitted");
        }

        private void SubmitAnonymous()
        {
            anonymousAttributes = new UserAttributesAnonymous
            {
                FirstName = firstNameInput.text,
                LastName = lastNameInput.text,
                Address = new UserAddress
                {
                    Region = regionInput.text,
                    Town = townInput.text,
                    Address = addressInput.text,
                    Postcode = postcodeInput.text
                },
                LanguageCode = languageDropdown.options[languageDropdown.value].text,
                TimeZone = timezoneDropdown.options[timezoneDropdown.value].text,
                Fields = GetDynamicFields()
            };

            SDKDebug.Info("Anonymous User Attributes Submitted");
        }

        private List<Field> GetDynamicFields()
        {
            List<Field> fields = new List<Field>();
            foreach (Transform fieldTransform in fieldsContainer.transform)
            {
                TMP_InputField keyInput = fieldTransform.Find("KeyInput").GetComponent<TMP_InputField>();
                TMP_InputField valueInput = fieldTransform.Find("ValueInput").GetComponent<TMP_InputField>();
                fields.Add(new Field { Key = keyInput.text, Value = valueInput.text });
            }

            return fields;
        }
    }
}