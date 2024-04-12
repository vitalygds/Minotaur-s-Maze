using System;
using MyGame.General.Data;

namespace MyGame.General.Service
{
    public interface IInAppServicesController
    {
        void Initialize(IAdvertisementsData advertisementsData);
        bool TryShowInterstitial(Action onAdComplete, Action onAdFailure);
    }
}