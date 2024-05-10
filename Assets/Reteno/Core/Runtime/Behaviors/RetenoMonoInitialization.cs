using UnityEngine;

namespace RetenoSDK
{
    public class RetenoMonoInitialization : MonoBehaviour
    {
        [SerializeField] private string appId;

        private void Start()
        {
            Reteno.Initialize(appId);
        }
    }
}