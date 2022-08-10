﻿using System;
using System.Threading.Tasks;
using AsyncUtils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace LoadingUtils
{
    public static class LoadingUtils
    {
        public static async Task LoadSceneAsync(string sceneToLoad, Task loadingTask = null, Action loadedCallback = null)
        {
            string loadingScene = LoadingSettings.Instance.DefaultLoadingSceneName;
            await LoadSceneAsync(loadingScene, sceneToLoad, loadingTask, loadedCallback);
        }
        
        public static async Task LoadSceneWithOverlapAsync(string sceneToLoad, Task loadingTask = null, Action loadedCallback = null)
        {
            string loadingScene = LoadingSettings.Instance.DefaultLoadingSceneName;
            await LoadSceneWithOverlapAsync(loadingScene, sceneToLoad, loadingTask, loadedCallback);
        }

        public static async Task LoadSceneAsync(
            string loadingScene,
            string sceneToLoad,
            Task loadingTask = null,
            Action loadedCallback = null)
        {
            float startTime = Time.time;
            var currentScene = SceneManager.GetActiveScene();
            // cargar escenena de carga
            if (!string.IsNullOrEmpty(loadingScene))
                await SceneManager.LoadSceneAsync(loadingScene, LoadSceneMode.Additive).AwaitAsync();
            
            // ejecutar proceso de carga opcional
            if (loadingTask != null)
                await loadingTask;
            
            // obtener barra de progreso
            // actualizar barra de progreso
            
            // descargar escena vieja
            await SceneManager.UnloadSceneAsync(currentScene).AwaitAsync();
            
            // cargar escena nueva
            await SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive).AwaitAsync();
            
            // ejecutar código de transición
            loadedCallback?.Invoke();

            await WaitMinLoadingTimeAsync(startTime);
            
            // descargar escena de carga
            if (!string.IsNullOrEmpty(loadingScene))
                await SceneManager.UnloadSceneAsync(loadingScene).AwaitAsync();
        }
        
        public static async Task LoadSceneWithOverlapAsync(
            string loadingScene,
            string sceneToLoad,
            Task loadingTask = null,
            Action loadedCallback = null)
        {
            float startTime = Time.time;
            var currentScene = SceneManager.GetActiveScene();
            // cargar escenena de carga
            if (!string.IsNullOrEmpty(loadingScene))
                await SceneManager.LoadSceneAsync(loadingScene, LoadSceneMode.Additive).AwaitAsync();
            
            // ejecutar proceso de carga opcional
            if (loadingTask != null)
                await loadingTask;
            
            // obtener barra de progreso
            // actualizar barra de progreso
            
            // cargar escena nueva
            await SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive).AwaitAsync();
            
            // ejecutar código de transición
            loadedCallback?.Invoke();
            
            // descargar escena vieja
            await SceneManager.UnloadSceneAsync(currentScene).AwaitAsync();

            // get new scene's event system and disable it
            var eventSystem = Object.FindObjectOfType<EventSystem>();
            if (eventSystem)
                eventSystem.gameObject.SetActive(false);

            await WaitMinLoadingTimeAsync(startTime);
            
            // descargar escena de carga
            if (!string.IsNullOrEmpty(loadingScene))
                await SceneManager.UnloadSceneAsync(loadingScene).AwaitAsync();
            
            // enable event system again. this prevents an issue
            if (eventSystem)
                eventSystem.gameObject.SetActive(true);
        }

        private static async Task WaitMinLoadingTimeAsync(float startTime)
        {
            float elapsedTime = Time.time - startTime;
            float difference = LoadingSettings.Instance.MinLoadingTime - elapsedTime;
            if (difference > 0)
                await Task.Delay((int) (difference * 1000));
        }
    }
}