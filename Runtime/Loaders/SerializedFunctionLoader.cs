using System;
using System.Threading;
using System.Threading.Tasks;
using SerializableCallback;
using UnityEngine;

namespace LoadingUtils.Loaders
{
    [Serializable]
    public class SerializedFunctionLoader : ILoader
    {
        [SerializeField] private SerializableCallback<CancellationToken, Task> _function;
        
        public Task Load(CancellationToken ct)
        {
            return _function.Invoke(ct);
        }
    }
}