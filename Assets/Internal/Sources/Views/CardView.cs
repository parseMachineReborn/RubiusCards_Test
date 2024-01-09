using Cysharp.Threading.Tasks;
using DG.Tweening;
using Rubius.ViewModels;
using UniRx;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

namespace Rubius.Views
{
    public sealed class CardView : MonoBehaviour
    {
        [SerializeField] private Transform _frontSide;
        [SerializeField] private Transform _backSide;
        [SerializeField] private ProceduralImage _cardAvatar;
        [SerializeField] private Vector3 _turnDegree = new Vector3(90f, 0f, 0f);
        [SerializeField] private Vector3 _largeScale = new Vector3(1.2f, 1.2f, 1.2f);

        private CardViewModel _viewModel;

        private const float _TurnDuration = 0.2f;
        private const float _ShrinkDuration = 0.3f;

        public void Init(CardViewModel viewModel)
        {
            _viewModel = viewModel;

            Subscribe();
        }

        public void TurnFront()
        {
            _ = Turn(_backSide, _frontSide);
        }

        public void TurnBack()
        {
            _ = Turn(_frontSide, _backSide);
        }

        private void SetupAvatar(Sprite avatar)
        {
            _cardAvatar.sprite = avatar;
        }

        private async UniTask Turn(Transform currentSide, Transform nextSide)
        {
            if (currentSide.transform.eulerAngles != _turnDegree)
            {
                await gameObject.transform.DOScale(_largeScale, _ShrinkDuration).AsyncWaitForCompletion();

                currentSide.DOLocalRotate(_turnDegree, _TurnDuration).OnComplete(async () =>
                {
                    await nextSide.DORotate(Vector3.zero, _TurnDuration).AsyncWaitForCompletion();
                    await gameObject.transform.DOScale(Vector3.one, _ShrinkDuration).AsyncWaitForCompletion();
                });
            }
        }

        private void Subscribe()
        {
            _viewModel.IsLoaded.ObserveEveryValueChanged(status => status.Value)
                .Subscribe(status =>
                {
                    if (status)
                    {
                        SetupAvatar(_viewModel.LoadedSprite);
                        TurnFront();
                    }
                });
        }

#if UNITY_EDITOR
        [Space]
        [SerializeField, Button(nameof(TurnFront))]
        private bool _isFrontTurned;

        [Space]
        [SerializeField, Button(nameof(TurnBack))]
        private bool _isBackTurned;
#endif
    }
}

