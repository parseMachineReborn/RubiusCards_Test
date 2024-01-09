using Rubius.Enums;
using Rubius.Factories;
using Rubius.Interfaces;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Rubius.Models
{
    public sealed class MainWindow
    {
        private readonly ILoader<Sprite> _loader;
        private readonly List<ILoadable<Sprite>> _loadData;
        private readonly Dictionary<string, LoadType> _loadTypes;
        private readonly LoadStrategiesFactory<Sprite> _strategiesFactory;

        public MainWindow(ILoader<Sprite> spriteLoader, List<ILoadable<Sprite>> loadData,
            LoadStrategiesFactory<Sprite> strategiesFactory, Dictionary<string, LoadType> loadTypes)
        {
            _loader = spriteLoader;
            _loadData = loadData;
            _strategiesFactory = strategiesFactory;
            _loadTypes = loadTypes;

            IsAllCardsLoaded = new ReactiveProperty<bool>();
        }

        public ReactiveProperty<bool> IsAllCardsLoaded { get; private set; }

        public async void StartLoading()
        {
            IsAllCardsLoaded.Value = false;
            await _loader.Load(_loadData);
            IsAllCardsLoaded.Value = true;
        }

        public void CancelLoading()
        {
            _loader.CancelLoading();
        }

        public void ChangeLoadStrategy(string value)
        {
            if (_loadTypes.TryGetValue(value, out LoadType loadType))
            {
                ILoadStrategy<Sprite> strategy = _strategiesFactory.GetStrategy(loadType);

                if (strategy != null)
                {
                    _loader.SetLoadStrategy(strategy);
                }
            }
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
