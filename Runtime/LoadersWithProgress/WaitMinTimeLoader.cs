using System;
using System.Collections.Generic;
using UnityEngine;

namespace LoadingUtils.LoadersWithProgress
{
    [Serializable]
    public class WaitMinTimeLoader : ILoaderProgress
    {
        [SerializeField] private float _timeToWait;

        public WaitMinTimeLoader()
        {
        }

        public WaitMinTimeLoader(float timeToWait)
        {
            _timeToWait = timeToWait;
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