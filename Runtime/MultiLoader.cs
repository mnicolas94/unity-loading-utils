using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace LoadingUtils
{
    public class MultiLoader : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private List<ILoader> _loaders;

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
            Load();
        }

        private async void Load()
        {
            var ct = _cts.Token;

            var total = _loaders.Count;
            var progress = 0;
            var tasks = _loaders.Select(loader =>
            {
                var loadTask = loader.Load(ct);
                void ProgressCallback()
                {
                    progress += 1 / total;
                    _onLoadingProgress.Invoke(progress);
                }

                return WaitTaskAndNotifyProgress(loadTask, ProgressCallback);
            });
            await Task.WhenAll(tasks);
        }

        private async Task WaitTaskAndNotifyProgress(Task task, Action progressCallback)
        {
            await task;
            progressCallback?.Invoke();
        }
    }
}