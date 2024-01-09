using Rubius.Models;

namespace Rubius.Interfaces
{
    public interface ILoadable<T>
    {
        public LoadData<T> LoadData { get; }
        public void SetLoadStatus(bool status);
    }
}
