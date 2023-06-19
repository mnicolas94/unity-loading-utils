using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TNRD;
using UnityEngine;
using UnityEngine.Events;

namespace LoadingUtils
{
    public class MultiLoader : MonoBehaviour, ILoader
    {
        [SerializeField] private List<SerializableInterface<ILoader>> _loaders;

        [SerializeField] private bool _loadOnStart;
        
        [Header("Events")]
        [SerializeField] private UnityEvent _onAllLoaded;
        [SerializeField] private UnityEvent<float> _onLoadingProgress;

        private CancellationTokenSource _cts;

        private void OnEnable()
        {
            _cts = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }

            _cts.Dispose();
            _cts = null;
        }
        
        private void Start()
        {
            if (_loadOnStart)
            {
                Load();
            }
        }

        public async Task Load(CancellationToken ct)
        {
            var total = _loaders.Count;
            var progress = 0;
            _onLoadingProgress.Invoke(0);
            
            var tasks = _loaders.Select(loader =>
            {
                var loadTask = loader.Value.Load(ct);
                void ProgressCallback()
                {
                    progress += 1 / total;
                    _onLoadingProgress.Invoke(progress);
                }
                
                return WaitTaskAndNotifyProgress(loadTask, ProgressCallback);
            });
            await Task.WhenAll(tasks);
            _onAllLoaded.Invoke();
        }

        public async void Load()
        {
            var ct = _cts.Token;
            await Load(ct);
        }

        private async Task WaitTaskAndNotifyProgress(Task task, Action progressCallback)
        {
            await task;
            progressCallback?.Invoke();
        }
    }
}