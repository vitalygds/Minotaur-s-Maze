using System;
using MyGame.General.Controller;
using MyGame.General.Data;
using MyGame.General.Service;
using UnityEngine;
using Zenject;

namespace MyGame.Core.Services
{
    internal sealed class InAppServicesController : IInAppServicesController, IDisposable
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly IControllersMediator _mediator;
        private IAdvertisementsService _adsService;
        private IAdvertisementsData _advertisementsData;
        private Action _succeedCallback;
        private Action _failureCallback;

        [Inject]
        public InAppServicesController(IAnalyticsService analyticsService, IControllersMediator mediator)
        {
            _analyticsService = analyticsService;
            _mediator = mediator;
        }

        public void Initialize(IAdvertisementsData advertisementsData)
        {
            _advertisementsData = advertisementsData;
            if (_adsService == null)
            {
                _adsService = new AdvertisementsService(advertisementsData.GameId);
                _adsService.OnInitializationEvent += OnInitializeAds;
                _adsService.OnAdsStageEvent += OnAdsStage;
                _mediator.OnRewardedAdsEvent += ShowRewardedAds;
            }
        }
        
        public bool TryShowInterstitial(Action onAdComplete, Action onAdFailed = null)
        {
            if (!_adsService.IsInitialized) return false;
            _adsService.ShowAd(_advertisementsData.Interstitial);
            _succeedCallback = onAdComplete;
            _failureCallback = onAdFailed;
            return true;
        }

        private void OnAdsStage(AdsResult adsResult, string id)
        {
            if (adsResult is AdsResult.Click or AdsResult.Start) return;
            ImplementCallback(adsResult);
            if (adsResult == AdsResult.Complete)
            {
                if (id == _advertisementsData.Interstitial)
                {
                    _mediator.InterstitialComplete();
                }
                else if (id == _advertisementsData.Rewarded)
                {
                    _mediator.RewardedComplete();
                }
            }
        }

        private void ImplementCallback(AdsResult adsResult)
        {
            if (adsResult == AdsResult.Fail)
                _succeedCallback?.Invoke();
            else if (adsResult == AdsResult.Complete)
                _failureCallback?.Invoke();
            _succeedCallback = null;
            _failureCallback = null;
        }

        private void ShowRewardedAds()
        {
            _adsService.ShowAd(_advertisementsData.Rewarded);
            _adsService.LoadAd(_advertisementsData.Rewarded);
        }

        private void OnInitializeAds(bool isInitialized, string message)
        {
            _adsService.OnInitializationEvent -= OnInitializeAds;
            if (isInitialized)
            {
                PreLoad();
                _mediator.AdvertisementsInitialized();
            }
            else
                _adsService = null;

            _analyticsService.AdvertisementsInitialization(isInitialized, message);
        }


        private void PreLoad()
        {
            _adsService.LoadAd(_advertisementsData.Interstitial);
            _adsService.LoadAd(_advertisementsData.Rewarded);
        }

        public void Dispose()
        {
            _adsService.OnInitializationEvent -= OnInitializeAds;
            _adsService.OnAdsStageEvent -= OnAdsStage;
            _mediator.OnRewardedAdsEvent -= ShowRewardedAds;
        }
    }
}