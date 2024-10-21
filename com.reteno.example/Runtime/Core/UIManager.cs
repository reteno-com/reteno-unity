using System.Collections.Generic;
using UnityEngine;

namespace Reteno.Example.Core
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> uiPanels;

        public void OpenPanel(GameObject panelToOpen)
        {
            foreach (var panel in uiPanels)
            {
                if (panel == panelToOpen)
                {
                    panel.SetActive(true);
                }
                else
                {
                    panel.SetActive(false);
                }
            }
        }

        public void CloseAllPanels()
        {
            foreach (var panel in uiPanels)
            {
                panel.SetActive(false);
            }
        }
    }
}