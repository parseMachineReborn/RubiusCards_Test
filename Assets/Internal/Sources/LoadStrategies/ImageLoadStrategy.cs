using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

namespace Rubius.Models
{
    public class ImageLoadStrategy : BaseLoadStrategy<Sprite>
    {
        protected async UniTask<Sprite> LoadSprite(string url)
        {
            var texture = await LoadTexture(url);
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }

        protected async UniTask<Texture2D> LoadTexture(string url)
        {
            UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            _cancellationSources.Add(cancellationTokenSource);

            try
            {
                await webRequest.SendWebRequest().WithCancellation(cancellationTokenSource.Token);

                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log($"ERROR APPEARED WHILE DOWNLOADING {webRequest.error}");
                    return null;
                }

                return ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
            }
            finally
            {
                Debug.Log("WEB REQUEST WAS CANCELED OR FINISHED");
                webRequest.Dispose();
            }
        }
    }
}
