using System;
using System.Collections.Generic;
using SerializableCallback;
using UnityEngine;

namespace LoadingUtils.LoadersWithProgress
{
    [Serializable]
    public class SerializedFunctionLoaderWithProgress : ILoaderProgress
    {
        [SerializeField] private SerializableCallback<IEnumerable<float>> _function;
        
        public IEnumerable<float> Load()
        {
            return _function.Invoke();
        }
    }
}