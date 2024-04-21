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
        [SerializeField] private bool _bootstrapInEditor;
        public bool BootstrapInEditor => _bootstrapInEditor;
        
        [SerializeField] private bool _bootstrapInPlayerBuild;
        public bool BootstrapInPlayerBuild => _bootstrapInPlayerBuild;
        
        [SerializeField, Tooltip("Whether to unload and reload the first scene after the bootstrap scene, in Editor")]
        private bool _reloadActiveSceneInEditor;
        public bool ReloadActiveSceneInEditor => _reloadActiveSceneInEditor;
        
        [SerializeField, Tooltip("Whether to unload and reload the first scene after the bootstrap scene, in Player")]
        private bool _reloadActiveSceneInPlayer;
        public bool ReloadActiveSceneInPlayer => _reloadActiveSceneInPlayer;

        [SerializeField] private SceneData _boostrapScene;
        public SceneData BoostrapScene => _boostrapScene;

        [Header("(Deprecated) Default loading scene")]
        [SerializeField] private string _defaultLoadingSceneName;
        public string DefaultLoadingSceneName => _defaultLoadingSceneName;
        
        [SerializeField] private float minLoadingTime;
        public float MinLoadingTime => minLoadingTime;
    }
}