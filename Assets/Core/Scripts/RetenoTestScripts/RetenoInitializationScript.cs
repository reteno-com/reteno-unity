using System.Collections.Generic;
using UnityEngine;

public class RetenoInitializationScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject userDataMenu;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        userDataMenu.SetActive(false);

        RetenoAndroid.SetNotificationCustomDataListener(new NotificationCustomDataListener());
        RetenoAndroid.SetInAppMessageCustomDataListener(new InAppCustomDataListener());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private class NotificationCustomDataListener : RetenoCustomDataListener
    {
        public void OnCustomDataReceived(Dictionary<string, string> customData)
        {
            string log = "got custom data from notification: \n";
            
            foreach(var key in customData.Keys)
            {
                log += key + ": " + customData[key] + "\n";
            }
            Debug.Log(log);
        }
    }

    private class InAppCustomDataListener : RetenoCustomDataListener
    {
        public void OnCustomDataReceived(Dictionary<string, string> customData)
        {
            string log = "got custom data from in app: \n";

            foreach (var key in customData.Keys)
            {
                log += key + ": " + customData[key] + "\n";
            }
            Debug.Log(log);
        }
    }
}
