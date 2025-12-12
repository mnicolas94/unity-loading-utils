using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SerializableCallback;
using TNRD;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using Utils.Attributes;

namespace LoadingUtils
{
    public class MultiLoader : MonoBehaviour
    {
        [SerializeField] private bool _loadOnStart;
        
        [SerializeField, Tooltip("The initial progress to notify. Useful to boost progress bars at start.")]
        private float _progressSkip;
        
        [SerializeField] private List<SerializableInterface<ILoaderProgress>> _loadersWithProgress;

        [ReadOnly]
        [SerializeField] private List<SerializableInterface<ILoader>> _loaders;
        [ReadOnly]
        [SerializeField] private List<SerializableCallback<CancellationToken, Task>> _loadersFunctions;
        [ReadOnly]
        [SerializeField] private List<SerializableCallback<IEnumerable<float>>> _loadersWithProgressFunctions;

        [Header("Events")]
        [SerializeField] private UnityEvent _onAllLoaded;
        [SerializeField] private UnityEvent<float> _onLoadingProgress;

        private readonly Dictionary<object, float> _loadersProgress = new();
        private bool _loading;
        
        private void Start()
        {
            if (_loadOnStart)
            {
                Load();
            }
        }

        public async void Load()
        {
            await Load(destroyCancellationToken);
        }
        
        public async Task Load(CancellationToken ct)
        {
            if (_loading)
            {
                return;  // avoid loading more than once as it could result in issues
            }
            _loading = true;
            
            _loadersProgress.Clear();
            _onLoadingProgress.Invoke(_progressSkip);

            using var _ = ListPool<Task>.Get(out var tasksWithProgress);
            foreach (var loaderWithProgress in _loadersWithProgress)
            {
                tasksWithProgress.Add(StartLoaderAndGetTask(ct, loaderWithProgress));
            }
            
            await Task.WhenAll(tasksWithProgress);
            _loadersProgress.Clear();
            _onLoadingProgress.Invoke(1f);
            _onAllLoaded.Invoke();
        
            _loading = false;
        }

        private Task StartLoaderAndGetTask(CancellationToken ct, SerializableInterface<ILoaderProgress> loader)
        {
            _loadersProgress.Add(loader, 0);

            var progressValues = loader.Value.Load();

            void ProgressCallback(float taskProgress)
            {
                _loadersProgress[loader] = taskProgress;
                NotifyProgress();
            }

            var loadTask = GetIEnumerableTask(progressValues, ProgressCallback, ct);
            return WaitTaskAndNotifyProgress(loadTask, loader);
        }

        private void NotifyProgress()
        {
            var tasksProgress = _loadersProgress.Sum(x => x.Value) / _loadersProgress.Count;;
            var progress = _progressSkip + (1 - _progressSkip) * tasksProgress;
            _onLoadingProgress.Invoke(progress);
        }

        private void SetTaskProgressToCompletedAndNotify(object loader)
        {
            _loadersProgress[loader] = 1f;
            NotifyProgress();
        }

        private async Task WaitTaskAndNotifyProgress(Task task, object loader)
        {
            await task;
            SetTaskProgressToCompletedAndNotify(loader);
        }

        private async Task GetIEnumerableTask(IEnumerable<float> enumerable, Action<float> progressAction,
            CancellationToken ct)
        {
            foreach (var progressValue in enumerable)
            {
                if (ct.IsCancellationRequested)
                {
                    break;
                }
                progressAction(progressValue);
                await Task.Yield();
            }
        }
    }
}