using Cysharp.Threading.Tasks;
using Rubius.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rubius.Models
{
    public class WhenAllImageReadyLoadStrategy : ImageLoadStrategy
    {
        protected override async UniTask StartLoadingProcess(List<ILoadable<Sprite>> loadDatas)
        {
            try
            {
                await UniTask.WhenAll(loadDatas.Select(LoadParallel));
            }
            catch (Exception exception)
            {
                Debug.Log($"ERROR OCCURED WHILE WHEN ALL READY STRATEGY LOADING {exception.Message}");
            }
        }

        private async UniTask LoadParallel(ILoadable<Sprite> loadable)
        {
            var loadedSprite = await LoadSprite(loadable.LoadData.Url);
            loadable.LoadData.Value = loadedSprite;
            loadable.SetLoadStatus(true);
        }
    }
}
