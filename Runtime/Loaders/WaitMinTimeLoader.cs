using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace LoadingUtils.Loaders
{
    [Obsolete]
    [Serializable]
    public class WaitMinTimeLoader : ILoader
    {
        [SerializeField] private float _timeToWait;

        public WaitMinTimeLoader()
        {
        }

        public WaitMinTimeLoader(float timeToWait)
        {
            _timeToWait = timeToWait;
        }

        public async Task Load(CancellationToken ct)
        {
            await AsyncUtils.Utils.Delay(_timeToWait, ct);
        }
    }
}