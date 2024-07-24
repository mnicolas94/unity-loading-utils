using UnityEngine;
using UnityEngine.SceneManagement;
using ZeShmouttsAssets.DataContainers;

namespace LoadingUtils
{
    public class CurrentSceneUnloader : MonoBehaviour
    {
        public void Unload()
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(currentScene.name);
        }
        
        public void Unload(SceneData scene)
        {
            SceneManager.UnloadSceneAsync(scene.SceneName);
        }

        public void UnloadAll()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                SceneManager.UnloadSceneAsync(scene.name);
            }
        }
    }
}