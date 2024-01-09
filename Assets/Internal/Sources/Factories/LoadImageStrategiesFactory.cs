using Rubius.Enums;
using Rubius.Interfaces;
using Rubius.Models;
using System;
using UnityEngine;

namespace Rubius.Factories
{
    public class LoadImageStrategiesFactory : LoadStrategiesFactory<Sprite>
    {
        public override ILoadStrategy<Sprite> GetStrategy(LoadType loadType)
        {
            switch (loadType)
            {
                case LoadType.AllAtOnce:
                    return new AllAtOnceLoadStrategy();
                case LoadType.OneByOne:
                    return new OneByOneLoadStrategy();
                case LoadType.WhenImageReady:
                    return new WhenAllImageReadyLoadStrategy();
            }

            throw new InvalidOperationException();
        }
    }
}
