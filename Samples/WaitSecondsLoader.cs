using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace LoadingUtils.Samples
{
    public class WaitSecondsLoader : MonoBehaviour, ILoader
    {
        [SerializeField] private float _seconds;
        
        public async Task Load(CancellationToken ct)
        {
            var millis = (int) (_seconds * 1000);
            Debug.Log($"t {Time.time}");
            await Task.Delay(millis, ct);
            Debug.Log($"t {Time.time}");
        }

        public void PrintOnLoaded()
        {
            Debug.Log($"Loading finished. Time: {Time.time}");
        }

        public void PrintProgress(float progress)
        {
            Debug.Log($"Loading progress: {progress}. Time: {Time.time}");
        }
    }
}