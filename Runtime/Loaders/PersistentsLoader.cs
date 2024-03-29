﻿#if EXISTS_FACTICUS_SAVESYSTEM

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SaveSystem;
using UnityEngine;

namespace LoadingUtils.Loaders
{
    [Serializable]
    public class PersistentsLoader : ILoader
    {
        [SerializeField] private List<ScriptableObject> _persistents;
        
        public async Task Load(CancellationToken ct)
        {
            var tasks = _persistents.Select(LoadPersistent);
            await Task.WhenAll(tasks);
        }

        private async Task LoadPersistent(ScriptableObject persistent)
        {
            await persistent.LoadOrCreate();
        }
    }
}

#endif