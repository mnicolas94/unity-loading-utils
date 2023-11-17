using System.Collections.Generic;

namespace LoadingUtils
{
    public interface ILoaderProgress
    {
        IEnumerable<float> Load();
    }
}