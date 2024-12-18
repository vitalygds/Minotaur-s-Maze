using System;

namespace General
{
    public interface IInAppServicesController
    {
        void Initialize(IAdvertisementsData advertisementsData);
        bool TryShowInterstitial(Action onAdComplete, Action onAdFailure);
    }
}