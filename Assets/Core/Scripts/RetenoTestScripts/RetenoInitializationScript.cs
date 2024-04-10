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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
