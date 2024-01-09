using Rubius.Models;
using UniRx;
using UnityEngine;

namespace Rubius.ViewModels
{
    public sealed class CardViewModel
    {
        private Card _card;

        public CardViewModel(Card card)
        {
            _card = card;
            IsLoaded = new ReactiveProperty<bool>();

            Subscribe();
        }

        public Sprite LoadedSprite { get; private set; }
        public ReactiveProperty<bool> IsLoaded { get; private set; }

        private void Subscribe()
        {
            _card.IsLoaded.ObserveEveryValueChanged(status => status.Value)
                .Subscribe(status =>
                {
                    IsLoaded.Value = status;
                    LoadedSprite = _card.LoadData.Value;
                });
        }
    }
}
