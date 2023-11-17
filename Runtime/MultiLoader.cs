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
        [SerializeField] private bool _loadOnStart;
        
        [SerializeField, Tooltip("The initial progress to notify. Useful to boost progress bars at start.")]
        private float _progressSkip;
        
        [SerializeField] private List<SerializableInterface<ILoader>> _loaders;
        [SerializeField] private List<SerializableInterface<ILoaderProgress>> _loadersWithProgress;
        
        [Header("Events")]
        [SerializeField] private UnityEvent _onAllLoaded;
        [SerializeField] private UnityEvent<float> _onLoadingProgress;

        private Dictionary<object, float> _loadersProgress = new();
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
            _loadersProgress.Clear();
            var total = _loaders.Count + _loadersWithProgress.Count;
            var progress = _progressSkip;
            var allTasksTotalProgress = 1f - _progressSkip;
            var singleTaskTotalProgress = allTasksTotalProgress / total;

            _onLoadingProgress.Invoke(progress);

            void NotifyProgress()
            {
                var prog = _loadersProgress.Sum(x => x.Value) / _loadersProgress.Count;;
                _onLoadingProgress.Invoke(prog);
            }

            void NotifyFinishedTask(object loader)
            {
                _loadersProgress[loader] = 1f;
                NotifyProgress();
            }
            
            var tasks = _loaders.Select(loader =>
            {
                _loadersProgress.Add(loader, 0);
                var loadTask = loader.Value.Load(ct);
                return WaitTaskAndNotifyProgress(loadTask, () => NotifyFinishedTask(loader));
            });

            var tasksWithProgress = _loadersWithProgress.Select(loader =>
            {
                _loadersProgress.Add(loader, 0);
                
                var progressValues = loader.Value.Load();
                void ProgressCallback(float taskProgress)
                {
                    _loadersProgress[loader] = taskProgress;
                    NotifyProgress();
                }

                var loadTask = GetIEnumerableTask(progressValues, ProgressCallback);
                return WaitTaskAndNotifyProgress(loadTask, () => NotifyFinishedTask(loader));
            });

            var allTasks = tasks.Concat(tasksWithProgress);
            
            await Task.WhenAll(allTasks);
            _loadersProgress.Clear();
            _onLoadingProgress.Invoke(1f);
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

        private async Task GetIEnumerableTask(IEnumerable<float> enumerable, Action<float> progressAction)
        {
            foreach (var progressValue in enumerable)
            {
                progressAction(progressValue);
                await Task.Yield();
            }
        }
    }
}