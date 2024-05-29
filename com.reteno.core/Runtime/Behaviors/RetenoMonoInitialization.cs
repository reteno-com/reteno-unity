using UnityEngine;

namespace Reteno.Core
{
    /// <summary>
    /// One of the behavioral ways of initializing Reteno
    /// </summary>
    public class RetenoMonoInitialization : MonoBehaviour
    {
        [SerializeField] 
        private string appId;

        private void Start()
        {
            RetenoSDK.Initialize(appId);
        }
    }
}