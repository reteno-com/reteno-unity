using UnityEngine;

namespace RetenoSDK
{
    [CreateAssetMenu(fileName = "RetenoSettings", menuName = "Reteno/Create Settings", order = 0)]
    public class RetenoSettings : ScriptableObject
    {
        [SerializeField] private string _apiKey;
        public string API_KEY => _apiKey;
    }
}