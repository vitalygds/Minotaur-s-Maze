﻿namespace General
{
    public interface IAnalyticsService
    {
        void GameStarted();
        void AdvertisementsInitialization(bool isInitialized, string message);
    }
}