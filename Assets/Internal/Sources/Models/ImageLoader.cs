using Rubius.Interfaces;
using UnityEngine;

namespace Rubius.Models
{
    public class ImageLoader : Loader<Sprite>
    {
        public ImageLoader(ILoadStrategy<Sprite> loadStrategy) : base(loadStrategy)
        {
        }
    }
}
