using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadingUtils
{
    public static class SceneBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            var loadingSettings = LoadingSettings.Instance;
            
#if UNITY_EDITOR
            var bootstrap = loadingSettings.BootstrapInEditor;
#else
            var bootstrap = loadingSettings.BootstrapInPlayerBuild;
#endif
            if (!bootstrap)
            {
                return;
            }

            var bootstrapScene = loadingSettings.BoostrapScene;
            var bootstrapUnityScene = SceneManager.GetSceneByName(bootstrapScene.SceneName);
            
            // just load the bootstrap scene additively
            if (!bootstrapUnityScene.isLoaded)
            {
                SceneManager.LoadScene(bootstrapScene.SceneName, LoadSceneMode.Additive);
            }
        }
    }
}