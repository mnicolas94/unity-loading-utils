using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace LoadingUtils.LoadersWithProgress
{
    [Serializable]
    public class WaitMinTimeLoader : ILoaderProgress
    {
        [SerializeField] private float _timeToWait;
        
        public async Task Load(CancellationToken ct)
        {
            await AsyncUtils.Utils.Delay(_timeToWait, ct);
        }

        public IEnumerable<float> Load()
        {
            var start = Time.time;
            var elapsed = 0f;
            while (elapsed < _timeToWait)
            {
                elapsed = Time.time - start;
                var normalized = elapsed / _timeToWait;
                yield return normalized;
            }
        }
    }
}