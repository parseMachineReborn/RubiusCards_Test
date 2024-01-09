using Cysharp.Threading.Tasks;
using Rubius.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rubius.Models
{
    public class OneByOneLoadStrategy : ImageLoadStrategy
    {
        protected override async UniTask StartLoadingProcess(List<ILoadable<Sprite>> loadDatas)
        {
            try
            {
                for (int i = 0; i < loadDatas.Count; i++)
                {
                    LoadData<Sprite> loadData = loadDatas[i].LoadData;
                    Sprite loadedSprite = await LoadSprite(loadData.Url);
                    loadDatas[i].LoadData.Value = loadedSprite;
                    loadDatas[i].SetLoadStatus(true);
                }
            }
            catch (Exception exception)
            {
                Debug.Log($"ERROR OCCURED WHILE ONE BY ONE STRATEGY LOADING {exception.Message}");
            }
        }
    }
}
