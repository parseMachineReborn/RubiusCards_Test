using Rubius.Interfaces;
using UniRx;
using UnityEngine;

namespace Rubius.Models
{
    public class Card : ILoadable<Sprite>
    {
        public Card(LoadData<Sprite> loadData, string description)
        {
            LoadData = loadData;
            Description = description;

            IsLoaded = new ReactiveProperty<bool>();
        }

        public LoadData<Sprite> LoadData { get; private set; }
        public string Description { get; private set; }

        public ReactiveProperty<bool> IsLoaded { get; private set; }

        public void SetLoadStatus(bool status)
        {
            IsLoaded.Value = status;
        }
    }
}