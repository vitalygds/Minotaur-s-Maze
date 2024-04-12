using MyGame.General.Data;
using UnityEngine;

namespace MyGame.Data
{
    [CreateAssetMenu(fileName = nameof(AdsData), menuName = "ProjectData/" + nameof(AdsData))]
    public sealed class AdsData : ScriptableObject, IAdvertisementsData
    {
        [SerializeField] private string _androidGameId;
        [SerializeField] private string _iOSGameId;
        [SerializeField] private string _interstitialAndroid;
        [SerializeField] private string _interstitialIOS;
        [SerializeField] private string _rewardedAndroid;
        [SerializeField] private string _rewardedIOS;
        [SerializeField] private string _bannerAndroid;
        [SerializeField] private string _bannerIOS;
        
        public string GameId =>
#if UNITY_EDITOR
            _androidGameId;
#else
            Application.platform switch
            {
                RuntimePlatform.Android      => _androidGameId,
                RuntimePlatform.IPhonePlayer => _iOSGameId,
                _                            => ""
            };
#endif
        public string Interstitial =>
#if UNITY_EDITOR
            _interstitialAndroid;
#else
            Application.platform switch
            {
                RuntimePlatform.Android      => _interstitialAndroid,
                RuntimePlatform.IPhonePlayer => _interstitialIOS,
                _                            => ""
            };
#endif
        public string Rewarded =>
#if UNITY_EDITOR
            _rewardedAndroid;
#else
            Application.platform switch
            {
                RuntimePlatform.Android      => _rewardedAndroid,
                RuntimePlatform.IPhonePlayer => _rewardedIOS,
                _                            => ""
            };
#endif

        public string Banner =>
#if UNITY_EDITOR
            _bannerAndroid;
#else
        Application.platform switch
        {
            RuntimePlatform.Android      => _bannerAndroid,
            RuntimePlatform.IPhonePlayer => _bannerIOS,
            _                            => ""
        };
#endif
    }
}