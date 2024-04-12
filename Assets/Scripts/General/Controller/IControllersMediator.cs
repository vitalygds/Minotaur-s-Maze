using System;
using MyGame.General.View;
using UnityEngine;

namespace MyGame.General.Controller
{
    public interface IControllersMediator
    {
        event Action OnRewardedAdsEvent;
        event Action OnStartGameEvent;
        event Action<Transform> OnHeroCreateEvent;
        event Action OnArtifactPickUpEvent;
        event Action OnWinGameEvent;
        event Action OnGameOverEvent;
        event Action OnSceneInitializeEvent;
        event Action OnGameQuitEvent;
        event Action OnPauseGameEvent;
        event Action OnRestartGameEvent;
        event Action OnContinueGameEvent;
        bool IsJoysticksUse { get; set; }
        bool IsAdvertisementsInitialized { get; }
        bool IsArtifactPicked { get; }
        event Action OnInterstitialAdCompleteEvent;
        event Action OnRewardedAdCompleteEvent;
        void ActivateRewardedAds();
        void SetArtifactPicked();
        void HeroCreated(IView heroView);
        void QuitGame();
        void TryWin();
        void GameOver();
        void SceneInitialized();
        void StartGame();
        void PauseGame();
        void ContinueGame();
        void AdvertisementsInitialized();
        void InterstitialComplete();
        void RewardedComplete();
        void RestartGame();
    }
}