using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LoadingUtils.Loaders;
using UnityEngine;

namespace LoadingUtils.Samples
{
    public class LoaderSerializableFunctions : MonoBehaviour
    {
        [SerializeField] private float _timeToWait;
        
        public async Task WaitTime(CancellationToken ct)
        {
            await new WaitMinTimeLoader(_timeToWait).Load(ct);
        }
        
        public IEnumerable<float> WaitTimeWithProgress()
        {
            return new LoadersWithProgress.WaitMinTimeLoader(_timeToWait).Load();
        }
    }
}