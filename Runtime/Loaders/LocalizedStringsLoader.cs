#if ENABLED_LOCALIZATION

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;

namespace LoadingUtils.Loaders
{
    [Serializable]
    public class LocalizedStringsLoader : ILoader
    {
        [SerializeField] private List<LocalizedString> _strings;
        
        public async Task Load(CancellationToken ct)
        {
            var tasks = _strings.Select(LoadLocalizedString);
            await Task.WhenAll(tasks);
        }

        private async Task LoadLocalizedString(LocalizedString localizedString)
        {
            await localizedString.GetLocalizedStringAsync().Task;
        }
    }
}

#endif