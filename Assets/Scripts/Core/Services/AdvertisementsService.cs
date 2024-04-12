using System;
using MyGame.General.Service;
using UnityEngine;
using UnityEngine.Advertisements;

namespace MyGame.Core.Services
{
    internal sealed class AdvertisementsService : IAdvertisementsService, IUnityAdsInitializationListener, IUnityAdsShowListener, IUnityAdsLoadListener
    {
        public event Action<bool, string> OnInitializationEvent;
        public event Action<AdsResult, string> OnAdsStageEvent;
        public bool IsInitialized => Advertisement.isInitialized;

        public AdvertisementsService(string gameId) => Advertisement.Initialize(gameId, true, this);

        public void ShowAd(string adsId) => Advertisement.Show(adsId, this);

        public void LoadAd(string adsId) => Advertisement.Load(adsId, this);

        void IUnityAdsInitializationListener.OnInitializationComplete() => OnInitializationEvent?.Invoke(true, string.Empty);

        void IUnityAdsInitializationListener.OnInitializationFailed(UnityAdsInitializationError error, string message) => OnInitializationEvent?.Invoke(false, message);

        void IUnityAdsShowListener.OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) => OnAdsStageEvent?.Invoke(AdsResult.Fail, placementId);

        void IUnityAdsShowListener.OnUnityAdsShowStart(string placementId) => OnAdsStageEvent?.Invoke(AdsResult.Start, placementId);

        void IUnityAdsShowListener.OnUnityAdsShowClick(string placementId) => OnAdsStageEvent?.Invoke(AdsResult.Click, placementId);

        void IUnityAdsShowListener.OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) => OnAdsStageEvent?.Invoke(AdsResult.Complete, placementId);
        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("Loaded");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log("Failed");
        }
    }
}