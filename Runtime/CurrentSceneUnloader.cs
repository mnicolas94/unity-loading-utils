using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadingUtils
{
    public class CurrentSceneUnloader : MonoBehaviour
    {
        public void Unload()
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(currentScene.name);
        }
    }
}