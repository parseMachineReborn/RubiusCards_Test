using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Rubius.Interfaces
{
    public interface ILoadStrategy<T>
    {
        public UniTask Load(List<ILoadable<T>> loadDatas);
        public void CancelLoading();
    }
}
