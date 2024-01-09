using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Rubius.Interfaces
{
    public interface ILoader<T>
    {
        public void SetLoadStrategy(ILoadStrategy<T> strategy);
        public UniTask Load(List<ILoadable<T>> loadDatas);
        public void CancelLoading();
    }
}
