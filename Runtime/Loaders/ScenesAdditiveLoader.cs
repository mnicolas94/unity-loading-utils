using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AsyncUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZeShmouttsAssets.DataContainers;

namespace LoadingUtils.Loaders
{
    [Serializable]
    public class ScenesAdditiveLoader : ILoader
    {
        [SerializeField] private List<SceneData> _scenes;

        public async Task Load(CancellationToken ct)
        {
            foreach (var scene in _scenes)
            {
                await SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive).AwaitAsync(ct);
            }
        }
    }
}