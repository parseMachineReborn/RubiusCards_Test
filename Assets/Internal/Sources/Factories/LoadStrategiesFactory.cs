using Rubius.Enums;
using Rubius.Interfaces;

namespace Rubius.Factories
{
    public abstract class LoadStrategiesFactory<T>
    {
        public abstract ILoadStrategy<T> GetStrategy(LoadType loadType);
    }
}


