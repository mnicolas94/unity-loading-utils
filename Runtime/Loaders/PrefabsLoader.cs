using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace LoadingUtils.Loaders
{
    public class PrefabsLoader : MonoBehaviour, ILoader
    {
        [SerializeField] private List<GameObject> _prefabs;
        
        public async Task Load(CancellationToken ct)
        {
            var tasks = _prefabs.Select(LoadPrefab);
            await Task.WhenAll(tasks);
        }

        private async Task LoadPrefab(GameObject prefab)
        {
            var instance = Instantiate(prefab);
            instance.SetActive(false);
            await Task.Yield();
        }
    }
}