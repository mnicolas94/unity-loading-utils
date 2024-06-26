﻿using System.Threading;
using AsyncUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadingUtils
{
    public static class SceneBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static async void Init()
        {
            var loadingSettings = LoadingSettings.Instance;
            
#if UNITY_EDITOR
            var bootstrap = loadingSettings.BootstrapInEditor;
            var reloadScene = loadingSettings.ReloadActiveSceneInEditor;
#else
            var bootstrap = loadingSettings.BootstrapInPlayerBuild;
            var reloadScene = loadingSettings.ReloadActiveSceneInPlayer;
#endif
            if (!bootstrap)
            {
                return;
            }

            var bootstrapScene = loadingSettings.BoostrapScene;
            var bootstrapUnityScene = SceneManager.GetSceneByName(bootstrapScene.SceneName);
            if (bootstrapUnityScene.isLoaded)
            {
                return;
            }
            
            if (reloadScene)
            {
                // disable all objects to avoid calling their Awake methods
                var allObjects = Object.FindObjectsOfType<GameObject>();
                foreach (var gameObject in allObjects)
                {
                    gameObject.SetActive(false);
                }
                
                // unload current scene and load bootstrap scene
                var currentScene = SceneManager.GetActiveScene();
                var currentSceneName = currentScene.name;
                SceneManager.LoadScene(bootstrapScene.SceneName, LoadSceneMode.Single);
                
                // load current scene again
                await SceneManager.LoadSceneAsync(currentScene.name, LoadSceneMode.Additive).AwaitAsync(CancellationToken.None);
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentSceneName));
            }
            else
            {
                // just load the bootstrap scene additively (MonoBehaviour messages on bootstrap scene will
                // execute after the ones on the active scene)
                SceneManager.LoadSceneAsync(bootstrapScene.SceneName, LoadSceneMode.Additive);
            }
        }
    }
}