using System;

namespace MyGame.General.Service
{
    public interface IAdvertisementsService
    {
        event Action<bool, string> OnInitializationEvent;
        event Action<AdsResult, string> OnAdsStageEvent;
        bool IsInitialized { get; }
        void ShowAd(string adsId);
        void LoadAd(string adsId);
    }
}