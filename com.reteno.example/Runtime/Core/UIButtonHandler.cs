using UnityEngine;

namespace Reteno.Example.Core
{
    public class UIButtonHandler : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;
        [SerializeField] private GameObject panelToOpen;

        public void OnButtonClick()
        {
            uiManager.OpenPanel(panelToOpen);
        }
    }
}