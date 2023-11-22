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
    }
}