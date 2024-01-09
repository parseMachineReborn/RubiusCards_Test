using Rubius.Enums;
using Rubius.Factories;
using Rubius.Interfaces;
using Rubius.Models;
using Rubius.Storage;
using Rubius.ViewModels;
using Rubius.Views;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rubius
{
    public sealed class Launcher : MonoBehaviour
    {
        [SerializeField] private Config _config;
        [SerializeField] private CardView _cardViewTemplate;
        [SerializeField] private MainWindowView _mainWindowView;

        private string _commonUrl;

        private void Awake()
        {
            _commonUrl = $"{_config.ImageResource}{_config.ImageSize}";

            Launch();
        }

        private void Launch()
        {
            List<ILoadable<Sprite>> loadDatas = new List<ILoadable<Sprite>>();
            List<CardView> cardViews = new List<CardView>();

            for (int i = 0; i < _config.CardsAmount; i++)
            {
                LoadData<Sprite> loadData = new LoadData<Sprite>(_commonUrl);
                Card newCard = new Card(loadData, "Default Description");
                loadDatas.Add(newCard);

                CardViewModel cardViewModel = new CardViewModel(newCard);
                CardView cardView = Instantiate(_cardViewTemplate, _mainWindowView.CardsRoot);
                cardView.Init(cardViewModel);
                cardViews.Add(cardView);
            }

            DropDownContentParser dropDownContentParser = new DropDownContentParser(_config);
            Dictionary<string, LoadType> dropDownContent = dropDownContentParser.GetContentFromConfig();

            LoadStrategiesFactory<Sprite> spriteLoadStrategiesFactory = new LoadImageStrategiesFactory();
            ILoadStrategy<Sprite> spriteLoadStrategy = spriteLoadStrategiesFactory.GetStrategy(LoadType.AllAtOnce);
            ILoader<Sprite> spriteLoader = new ImageLoader(spriteLoadStrategy);

            MainWindow mainWindow = new MainWindow(spriteLoader, loadDatas, spriteLoadStrategiesFactory, dropDownContent);
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(mainWindow);
            _mainWindowView.Init(mainWindowViewModel, cardViews, dropDownContent.Keys.ToList());
        }
    }
}
