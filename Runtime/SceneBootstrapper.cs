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
            
#if UNITY_EDITOR
            var currentScene = SceneManager.GetActiveScene();
#endif

            var bootstrapperScene = loadingSettings.BoostrapScene;
            if (!SceneManager.GetSceneByName(bootstrapperScene.SceneName).isLoaded)
            {
                SceneManager.LoadSceneAsync(bootstrapperScene.SceneName, LoadSceneMode.Additive);
            }
        }
    }
}