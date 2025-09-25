using UnityEngine;
using UnityEngine.SceneManagement;
using ZeShmouttsAssets.DataContainers;

namespace LoadingUtils
{
    public class SceneUnloader : MonoBehaviour
    {
        public void UnloadCurrent()
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(currentScene.name);
        }
        
        public void Unload(SceneData scene)
        {
            SceneManager.UnloadSceneAsync(scene.SceneName);
        }

        public void UnloadAllExcept(SceneData except)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.buildIndex == except.SceneIndex)
                    continue;
                SceneManager.UnloadSceneAsync(scene.name);
            }
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