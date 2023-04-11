using System.Threading;
using System.Threading.Tasks;

namespace LoadingUtils
{
    public interface ILoader
    {
        public Task Load(CancellationToken ct);
    }
}