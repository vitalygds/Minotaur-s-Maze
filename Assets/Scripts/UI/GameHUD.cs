using System.Threading;
using Cysharp.Threading.Tasks;
using MyGame.Extensions;
using MyGame.General.Controller;
using MyGame.General.Data;
using UnityEngine;
using UnityEngine.UIElements;

namespace MyGame.UI
{
    public sealed class GameHUD : MonoBehaviour
    {
        private const string c_harp = "Harp",
            c_scroll = "Scroll",
            c_text = "Text",
            c_adsBtn = "AdsButton",
            c_pauseBtn = "PauseButton";

        private const int c_gameOverDelay = 5000;
        private Button _adsButton;
        private Button _pauseButton;
        private VisualElement _root;
        private VisualElement _harpPicture;
        private VisualElement _scrollView;
        private TextElement _scrollText;
        private IControllersMediator _mediator;
        private IHudData _hudData;
        private CancellationTokenSource _cts;
        private PauseMenu _pauseMenu;

        public void Construct(IControllersMediator mediator, IHudData hudData, PauseMenu pauseMenu)
        {
            _mediator = mediator;
            _hudData = hudData;
            _pauseMenu = pauseMenu;
            InitializeElements();
            HideElements();
            _mediator.OnStartGameEvent += RefreshHud;
            _mediator.OnArtifactPickUpEvent += OnPickUpArtifact;
            //_mediator.OnStartGameEvent += ShowAdsButton;
            _mediator.OnWinGameEvent += ShowWin;
            _mediator.OnContinueGameEvent += ShowPauseButton;
            _mediator.OnRewardedAdsEvent += HideAdsButton;
            _mediator.OnGameOverEvent += OnGameOver;
            _adsButton.clicked += ShowAdvertisements;
            _pauseButton.clicked += ShowPauseMenu;
        }

        private void OnGameOver() => ShowPauseMenuAsync().Forget();

        private async UniTaskVoid ShowPauseMenuAsync()
        {
            DisposeToken();
            _cts = new CancellationTokenSource();
            _pauseMenu.SetGameOver();
            await UniTask.Delay(c_gameOverDelay, cancellationToken:_cts.Token);
            if (!_cts.IsCancellationRequested) 
                ShowPauseMenu();
            DisposeToken();
        }
        
        private void HideAdsButton() => _adsButton.ShowElement(false);

        private void ShowPauseButton() => _root.FlexElement(true);

        private void ShowPauseMenu()
        {
            _pauseMenu.Show(true);
            _mediator.PauseGame();
            _root.FlexElement(false);
        }
        
        private void ShowAdsButton() => ActivateAdsButtonAsync().Forget();

        private async UniTaskVoid ActivateAdsButtonAsync()
        {
            _cts = new CancellationTokenSource();
            await UniTask.Delay(_hudData.AdsButtonShowDelayTimeMillisecond, cancellationToken: _cts.Token);
            if (_mediator.IsAdvertisementsInitialized) 
                _adsButton.ShowElement(true);
            DisposeToken();
        }

        private void HideElements()
        {
            _harpPicture.ShowElement(false);
            _scrollView.ShowElement(false);
            _adsButton.ShowElement(false);
        }

        private void ShowAdvertisements() => _mediator.ActivateRewardedAds();

        private void ShowWin() => ShowScroll(_hudData.WinMessage);

        private void RefreshHud()
        {
            DisposeToken();
            ShowScroll(_hudData.StartMessage);
        }

        private void OnPickUpArtifact()
        {
            _harpPicture.ShowElement(true);
            ShowScroll(_hudData.OnArtifactPickMessage);
        }

        private void InitializeElements()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _harpPicture = _root.Q<VisualElement>(c_harp);
            _scrollView = _root.Q<VisualElement>(c_scroll);
            _scrollText = _scrollView.Q<TextElement>(c_text);
            _adsButton = _root.Q<VisualElement>(c_adsBtn).Q<Button>();
            _pauseButton = _root.Q<VisualElement>(c_pauseBtn).Q<Button>();
        }
        
        private void ShowScroll(string message) => ShowMessageAsync(message).Forget();

        private async UniTaskVoid ShowMessageAsync(string message)
        {
            _cts = new CancellationTokenSource();
            _scrollView.ShowElement(true);
            _scrollText.text = message;
            await UniTask.Delay(_hudData.MessageShowDelayTimeMillisecond, cancellationToken: _cts.Token);
            _scrollView.ShowElement(false);
            DisposeToken();
        }

        private void OnDestroy()
        {
            DisposeToken();
            UnsubscribeMediator();
        }

        private void DisposeToken()
        {
            if (_cts == null) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }

        private void UnsubscribeMediator()
        {
            _mediator.OnStartGameEvent -= RefreshHud;
            _mediator.OnArtifactPickUpEvent -= OnPickUpArtifact;
            _mediator.OnStartGameEvent -= ShowAdsButton;
            _mediator.OnWinGameEvent -= ShowWin;
            _mediator.OnContinueGameEvent -= ShowPauseButton;
            _mediator.OnRewardedAdsEvent -= HideAdsButton;
            _mediator.OnGameOverEvent -= OnGameOver;
            _adsButton.clicked -= ShowAdvertisements;
            _pauseButton.clicked -= ShowPauseMenu;
        }
    }
}