using Rubius.Models;
using System.Collections.Generic;
using UnityEngine;

namespace Rubius.Storage
{
    [CreateAssetMenu(fileName = "Config", menuName = "Configs/Config")]
    public sealed class Config : ScriptableObject
    {
        [SerializeField] private List<LoadTypeConfiguration> _loadTypeConfigurations;
        [SerializeField] private string _imageResource = "https://picsum.photos/";
        [SerializeField] private int _imageSize = 250;
        [SerializeField] private int _cardsAmount = 5;

        public List<LoadTypeConfiguration> LoadTypeConfigurations => _loadTypeConfigurations;
        public string ImageResource => _imageResource;
        public int ImageSize => _imageSize;
        public int CardsAmount => _cardsAmount;
    }
}

