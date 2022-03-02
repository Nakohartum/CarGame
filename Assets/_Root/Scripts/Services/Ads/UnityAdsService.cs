using UnityEngine.Advertisements;
using UnityEngine.Events;

namespace Services
{
    internal class UnityAdsService : IUnityAdsInitializationListener, IAdsService
    {
        public Settings Settings;
        public IAdsPlayer InterstitionalPlayer { get; private set; }
        public IAdsPlayer RewardedPlayer { get; private set; }
        public UnityEvent Initialized { get; private set; }
        public bool IsInitialized { get; private set; }
        private static UnityAdsService _instance;
        public static UnityAdsService Instance(Settings settings)
        {
            if (_instance == null)
            {
                _instance = new UnityAdsService(settings);
            }

            return _instance;
        }
        
        private UnityAdsService(Settings settings)
        {
            Settings = settings;
            InitializeAds();
            InitializePlayers();
        }
        
        private void InitializePlayers()
        {
            InterstitionalPlayer = Settings.Interstitial.IsEnabled
                ? new InterstitionalPlayer(Settings.Interstitial.ID)
                : null;
            RewardedPlayer = Settings.Interstitial.IsEnabled
                ? new RewardedPlayer(Settings.Rewarded.ID)
                : null;
        }

        private void InitializeAds()
        {
            Advertisement.Initialize(Settings.GameID, Settings.TestMode, Settings.EnanblePerPlacementMode, this);
        }

        public void OnInitializationComplete()
        {
            Initialized?.Invoke();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            throw new System.NotImplementedException();
        }
        
        
    }
}