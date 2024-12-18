using System.Collections.Generic;
using General;
using UnityEngine.Analytics;
using UnityEngine.Device;

namespace Core
{
    internal sealed class UnityAnalyticsService : IAnalyticsService
    {
        public void GameStarted()
        {
            var eventData = new Dictionary<string, object>
            {
                ["DeviceName"] = SystemInfo.deviceName,
                ["DeviceModel"] = SystemInfo.deviceModel
            };
            Analytics.CustomEvent("GameStarted", eventData);
        }

        public void AdvertisementsInitialization(bool isInitialized, string message)
        {
            var eventData = new Dictionary<string, object>
            {
                ["Success"] = isInitialized,
                ["Message"] = message
            };
            Analytics.CustomEvent("AdvertisementsInitialization", eventData);
        }
    }
}