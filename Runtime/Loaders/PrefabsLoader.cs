using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LoadingUtils.Loaders
{
    [Serializable]
    public class PrefabsLoader : ILoader
    {
        [SerializeField] private List<GameObject> _prefabs;
        
        public async Task Load(CancellationToken ct)
        {
            var tasks = _prefabs.Select(LoadPrefab);
            await Task.WhenAll(tasks);
        }

        private async Task LoadPrefab(GameObject prefab)
        {
            var instance = Object.Instantiate(prefab);
            instance.SetActive(false);
            await Task.Yield();
        }
    }
}