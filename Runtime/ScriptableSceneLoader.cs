using System.Collections.Generic;
using UnityEditor.Build.Content;
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
        
#if UNITY_EDITOR
        private List<string> GetScenesList()
        {
            var scenes = new List<string>();
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                var scene = SceneManager.GetSceneByBuildIndex(i);
                scenes.Add(scene.name);
            }

            return scenes;
        }
#endif
    }
}