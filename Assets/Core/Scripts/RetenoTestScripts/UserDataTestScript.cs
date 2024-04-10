using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserDataTestScript : MonoBehaviour
{
    public TextMeshProUGUI textMessage;

    public TMP_InputField inputExternalId;

    public TMP_InputField inputPhone;
    public TMP_InputField inputEmail;
    public TMP_InputField inputFirstName;
    public TMP_InputField inputLastName;

    public TMP_InputField inputRegion;
    public TMP_InputField inputTown;
    public TMP_InputField inputAddress;
    public TMP_InputField inputPostcode;

    public void SendUserData()
    {
        User user = new User();
        user.ExternalUserId = inputExternalId.text;

        UserAttributes attributes = new UserAttributes();
        attributes.Phone = inputPhone.text;
        attributes.Email = inputEmail.text;
        attributes.FirstName = inputFirstName.text;
        attributes.LastName = inputLastName.text;

        UserAddress address = new UserAddress();
        address.Region = inputRegion.text;
        address.Town = inputTown.text;
        address.Address = inputAddress.text;
        address.Postcode = inputPostcode.text;

        attributes.Address = address;
        user.UserAttributes = attributes;

        RetenoAndroid.SetUserAttributes(user.ExternalUserId, user);
        //RetenoSdk.SetUserAttributes(user);
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}
