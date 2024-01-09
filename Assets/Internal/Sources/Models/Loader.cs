using Cysharp.Threading.Tasks;
using Rubius.Interfaces;
using System.Collections.Generic;

namespace Rubius.Models
{
    public abstract class Loader<T> : ILoader<T>
    {
        private ILoadStrategy<T> _loadStrategy;

        public Loader(ILoadStrategy<T> loadStrategy)
        {
            _loadStrategy = loadStrategy;
        }

        public void SetLoadStrategy(ILoadStrategy<T> strategy)
        {
            if (strategy != null)
            {
                _loadStrategy = strategy;
            }
        }

        public async UniTask Load(List<ILoadable<T>> loadDatas)
        {
            await _loadStrategy.Load(loadDatas);
        }

        public void CancelLoading()
        {
            _loadStrategy.CancelLoading();
        }
    }
}
