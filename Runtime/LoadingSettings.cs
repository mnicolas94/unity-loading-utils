using UnityEngine;
using Utils;

namespace LoadingUtils
{
    [CreateAssetMenu(fileName = "LoadingSettings", menuName = "Facticus/LoadingUtils/LoadingSettings", order = 0)]
    public class LoadingSettings : ScriptableObjectSingleton<LoadingSettings>
    {
        [SerializeField] private string _defaultLoadingSceneName;
        
        [Space]
        
        [SerializeField] private float minLoadingTime;
        
        public string DefaultLoadingSceneName => _defaultLoadingSceneName;
        public float MinLoadingTime => minLoadingTime;
    }
}