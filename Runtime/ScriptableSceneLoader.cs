using System.Collections.Generic;
using System.IO;
using System.Threading;
using AsyncUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Attributes;

namespace LoadingUtils
{
    [CreateAssetMenu(fileName = "SceneLoader", menuName = "Facticus/LoadingUtils/Scene loader", order = 0)]
    public class ScriptableSceneLoader : ScriptableObject
    {
#if UNITY_EDITOR
        [Dropdown(nameof(GetScenesList))]
#endif
        [SerializeField] private string _sceneName;
        [SerializeField] private bool _loadAdditive;

        public void LoadScene()
        {
            var mode = _loadAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
            SceneManager.LoadScene(_sceneName, mode);
        }

        public async void LoadSceneAsync()
        {
            var mode = _loadAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
            await SceneManager.LoadSceneAsync(_sceneName, mode).AwaitAsync(CancellationToken.None);
        }
        
#if UNITY_EDITOR
        private List<string> GetScenesList()
        {
            var scenes = new List<string>();
            
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                var sceneName = Path.GetFileNameWithoutExtension(scenePath);
                scenes.Add(sceneName);
            }

            return scenes;
        }
#endif
    }
}