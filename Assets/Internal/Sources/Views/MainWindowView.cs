using Rubius.ViewModels;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Rubius.Views
{
    public sealed class MainWindowView : MonoBehaviour
    {
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private TMP_Dropdown _dropDown;
        [SerializeField] private Transform _cardsRoot;

        private MainWindowViewModel _viewModel;
        private List<CardView> _cardViews;

        public Transform CardsRoot => _cardsRoot;

        private void OnEnable()
        {
            _loadButton.onClick.AddListener(OnLoadButtonClick);
            _cancelButton.onClick.AddListener(OnCancelButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
            _dropDown.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _loadButton.onClick.RemoveListener(OnLoadButtonClick);
            _cancelButton.onClick.RemoveListener(OnCancelButtonClick);
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
            _dropDown.onValueChanged.RemoveListener(OnValueChanged);
        }

        public void Init(MainWindowViewModel viewModel, List<CardView> cardViews, List<string> dropDownContent)
        {
            _viewModel = viewModel;
            _cardViews = cardViews;

            InitDropDown(dropDownContent);
            Subscribe();
        }

        private void InitDropDown(List<string> dropDownContent)
        {
            for (int i = 0; i < dropDownContent.Count; i++)
            {
                _dropDown.options.Add(new TMP_Dropdown.OptionData() { text = dropDownContent[i] });
            }
        }

        private void Subscribe()
        {
            _viewModel.IsAllCardsLoaded.ObserveEveryValueChanged(status => status.Value)
                .Subscribe(status =>
                {
                    if (status)
                    {
                        OnCardsLoaded();
                    }
                });
        }

        private void OnLoadButtonClick()
        {
            SetNotReadyToLoadState();
            TurnAllCardsBack();

            _viewModel.OnLoad();
        }

        private void OnCancelButtonClick()
        {
            SetReadyToLoadState();

            _viewModel.OnCancelLoading();
        }

        private void OnValueChanged(int value)
        {
            _viewModel.OnLoadStrategyChanged(_dropDown.options[value].text);
        }

        private void OnExitButtonClick()
        {
            _viewModel.OnExit();
        }

        private void OnCardsLoaded()
        {
            SetReadyToLoadState();
        }

        private void SetReadyToLoadState()
        {
            SwapButtonState(_loadButton, true);
            SwapButtonState(_cancelButton, false);
        }

        private void SetNotReadyToLoadState()
        {
            SwapButtonState(_loadButton, false);
            SwapButtonState(_cancelButton, true);
        }

        private void SwapButtonState(Button button, bool isActive)
        {
            button.interactable = isActive;
        }

        private void TurnAllCardsBack()
        {
            for (int i = 0; i < _cardViews.Count; i++)
            {
                _cardViews[i].TurnBack();
            }
        }
    }
}
