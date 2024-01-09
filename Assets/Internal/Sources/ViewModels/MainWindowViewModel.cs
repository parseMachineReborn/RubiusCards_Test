using Rubius.Models;
using UniRx;

namespace Rubius.ViewModels
{
    public sealed class MainWindowViewModel
    {
        private MainWindow _mainWindow;

        public MainWindowViewModel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;

            IsAllCardsLoaded = new ReactiveProperty<bool>();

            Subscribe();
        }

        public ReactiveProperty<bool> IsAllCardsLoaded { get; private set; }

        public void OnLoad()
        {
            _mainWindow.StartLoading();
        }

        public void OnCancelLoading()
        {
            _mainWindow.CancelLoading();
        }

        public void OnLoadStrategyChanged(string value)
        {
            _mainWindow.ChangeLoadStrategy(value);
        }

        public void OnExit()
        {
            _mainWindow.Exit();
        }

        private void Subscribe()
        {
            _mainWindow.IsAllCardsLoaded.ObserveEveryValueChanged(status => status.Value)
                .Subscribe(status =>
                {
                    IsAllCardsLoaded.Value = status;
                });
        }
    }
}
