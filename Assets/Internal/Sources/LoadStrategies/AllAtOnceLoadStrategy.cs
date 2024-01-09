using Cysharp.Threading.Tasks;
using Rubius.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rubius.Models
{
    public class AllAtOnceLoadStrategy : ImageLoadStrategy
    {
        private List<Sprite> _loadedSprites;

        public AllAtOnceLoadStrategy()
        {
            _loadedSprites = new List<Sprite>();
        }

        protected override async UniTask StartLoadingProcess(List<ILoadable<Sprite>> loadDatas)
        {
            _loadedSprites.Clear();

            try
            {
                for (int i = 0; i < loadDatas.Count; i++)
                {
                    Sprite loadedSprite = await LoadSprite(loadDatas[i].LoadData.Url);
                    _loadedSprites.Add(loadedSprite);
                }

                for (int i = 0; i < loadDatas.Count; i++)
                {
                    loadDatas[i].LoadData.Value = _loadedSprites[i];
                    loadDatas[i].SetLoadStatus(true);
                }
            }
            catch (Exception exception)
            {
                Debug.Log($"ERROR OCCURED WHILE ALL AT ONCE STRATEGY LOADING {exception.Message}");
            }
        }
    }
}
