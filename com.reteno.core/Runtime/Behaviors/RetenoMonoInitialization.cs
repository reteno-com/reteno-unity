using Reteno.Core.Initialization;
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

        [SerializeField]
        private RetenoConfigurationConfig retenoConfigurationConfig;

        private void Start()
        {
            var retenoConfiguration = retenoConfigurationConfig != null 
                ? retenoConfigurationConfig.GetConfig() 
                : new RetenoConfiguration();
            
            RetenoSDK.Initialize(appId, retenoConfiguration);
        }
    }
}