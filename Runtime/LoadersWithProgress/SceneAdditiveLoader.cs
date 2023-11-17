using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZeShmouttsAssets.DataContainers;

namespace LoadingUtils.LoadersWithProgress
{
    [Serializable]
    public class SceneAdditiveLoader : ILoaderProgress
    {
        [SerializeField] private List<SceneData> _scenes;

        public IEnumerable<float> Load()
        {
            var sceneTotalProgress = 1f / _scenes.Count;
            for (int i = 0; i < _scenes.Count; i++)
            {
                var scene = _scenes[i];
                
                var operation = SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive);
                while (!operation.isDone)
                {
                    var overallProgress = sceneTotalProgress * (i + operation.progress);
                    Debug.Log($"{i}: {overallProgress}");
                    yield return overallProgress;
                }
            }
        }
    }
}