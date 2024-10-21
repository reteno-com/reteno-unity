using UnityEngine;

namespace Reteno.Example.Core
{
    public class UIButtonCloseApplication : MonoBehaviour
    {
        public void OnButtonClick()
        {
            Application.Quit();
        }
    }
}