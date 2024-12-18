using System;
using General;
using UnityEngine;
using Zenject;

namespace Core
{
    internal sealed class ControllersMediator : IControllersMediator
    {
        private readonly ITimeService _timeService;
        public event Action OnInterstitialAdCompleteEvent;
        public event Action OnRewardedAdCompleteEvent;
        public event Action OnRewardedAdsEvent;
        public event Action OnStartGameEvent;
        public event Action<Transform> OnHeroCreateEvent;
        public event Action OnArtifactPickUpEvent;
        public event Action OnWinGameEvent;
        public event Action OnGameOverEvent;
        public event Action OnSceneInitializeEvent;
        public event Action OnGameQuitEvent;
        public event Action OnPauseGameEvent;
        public event Action OnRestartGameEvent;
        public event Action OnContinueGameEvent;
        public bool IsArtifactPicked { get; private set; }
        public bool IsJoysticksUse { get; set; }
        public bool IsAdvertisementsInitialized { get; private set; }

        [Inject]
        public ControllersMediator(ITimeService timeService) => _timeService = timeService;

        public void ActivateRewardedAds() => OnRewardedAdsEvent?.Invoke();

        public void SetArtifactPicked()
        {
            IsArtifactPicked = true;
            OnArtifactPickUpEvent?.Invoke();
        }

        public void HeroCreated(IView hero) => OnHeroCreateEvent?.Invoke(hero.Transform);

        public void QuitGame() => OnGameQuitEvent?.Invoke();

        public void TryWin()
        {
            if (IsArtifactPicked)
                OnWinGameEvent?.Invoke();
        }

        public void GameOver() => OnGameOverEvent?.Invoke();
        public void SceneInitialized() => OnSceneInitializeEvent?.Invoke();
        public void StartGame()
        {
            ContinueGame();
            OnStartGameEvent?.Invoke();
        }

        public void PauseGame()
        {
            _timeService.PauseGame(true);
            OnPauseGameEvent?.Invoke();
        }

        public void ContinueGame()
        {
            _timeService.PauseGame(false);
            OnContinueGameEvent?.Invoke();
        }
        public void AdvertisementsInitialized() => IsAdvertisementsInitialized = true;
        public void InterstitialComplete() => OnInterstitialAdCompleteEvent?.Invoke();

        public void RewardedComplete()
        {
            SetArtifactPicked();
            OnRewardedAdCompleteEvent?.Invoke();
        }

        public void RestartGame()
        {
            IsArtifactPicked = false;
            OnRestartGameEvent?.Invoke();
        }
    }
}