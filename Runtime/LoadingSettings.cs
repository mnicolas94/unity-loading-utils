using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using ZeShmouttsAssets.DataContainers;

namespace LoadingUtils
{
    [CreateAssetMenu(fileName = "LoadingSettings", menuName = "Facticus/LoadingUtils/LoadingSettings", order = 0)]
    public class LoadingSettings : ScriptableObjectSingleton<LoadingSettings>
    {
        [Header("Bootstrapping")]
        [SerializeField] private bool _bootstrap;
        [SerializeField] private bool _bootstrapOnPlayerBuild;
        [SerializeField] private SceneData _boostrapScene;
        
        [Header("(Deprecated) Default loading scene")]
        [SerializeField] private string _defaultLoadingSceneName;
        [SerializeField] private float minLoadingTime;
        
        public string DefaultLoadingSceneName => _defaultLoadingSceneName;
        public float MinLoadingTime => minLoadingTime;
    }
}