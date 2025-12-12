using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoadingUtils
{
    public interface ILoader : ILoaderProgress
    {
        public Task Load(CancellationToken ct);
        
        IEnumerable<float> ILoaderProgress.Load()
        {
            var task = Load(CancellationToken.None);
            while (task.Status != TaskStatus.RanToCompletion && task.Status != TaskStatus.Faulted && task.Status != TaskStatus.Canceled)
            {
                yield return 0;
            }
            yield return 1f;
        }
    }
}