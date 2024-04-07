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
            
            if (loadingSettings.ReloadActiveScene) {
                var currentScene = SceneManager.GetActiveScene();
                
                // unload current scene and load bootstrap scene
                if (!bootstrapUnityScene.isLoaded)
                {
                    SceneManager.LoadScene(bootstrapScene.SceneName, LoadSceneMode.Single);
                    // load current scene again
                    SceneManager.LoadSceneAsync(currentScene.name, LoadSceneMode.Additive);
                }
            }
            else
            {
                // just load the bootstrap scene additively (MonoBehaviour messages on bootstrap scene will
                // execute after the ones on the active scene)
                if (!bootstrapUnityScene.isLoaded)
                {
                    SceneManager.LoadSceneAsync(bootstrapScene.SceneName, LoadSceneMode.Additive);
                }
            }
        }
    }
}