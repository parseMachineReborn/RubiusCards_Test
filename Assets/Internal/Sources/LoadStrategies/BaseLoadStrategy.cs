using Cysharp.Threading.Tasks;
using Rubius.Interfaces;
using System.Collections.Generic;
using System.Threading;

namespace Rubius.Models
{
    public abstract class BaseLoadStrategy<T> : ILoadStrategy<T>
    {
        protected List<CancellationTokenSource> _cancellationSources;

        public BaseLoadStrategy()
        {
            _cancellationSources = new List<CancellationTokenSource>();
        }

        public async UniTask Load(List<ILoadable<T>> loadDatas)
        {
            PreLoad(loadDatas);
            await StartLoadingProcess(loadDatas);
        }

        public void CancelLoading()
        {
            for (int i = 0; i < _cancellationSources.Count; i++)
            {
                _cancellationSources[i].Cancel();
            }

            _cancellationSources.Clear();
        }

        protected virtual UniTask StartLoadingProcess(List<ILoadable<T>> loadDatas)
        {
            return new UniTask();
        }

        private void PreLoad(List<ILoadable<T>> loadDatas)
        {
            for (int i = 0; i < loadDatas.Count; i++)
            {
                loadDatas[i].SetLoadStatus(false);
            }
        }
    }
}
