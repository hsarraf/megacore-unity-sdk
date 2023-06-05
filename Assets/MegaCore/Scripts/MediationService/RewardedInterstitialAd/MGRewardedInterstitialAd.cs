using UnityEngine;

using MegaCore.Logger;


namespace MegaCore.MediationService
{
    public class MGRewardedInterstitialAd : MGMediationAbsatrct, IMGRewardedInterstitialEvents
    {

        private int rewardedInterstitialRetryAttempt;

        public void Initialize()
        {

#if UNITY_ANDROID
            _adUnitId = _adUnitIdAndroid;
#elif UNITY_IOS
            _adUnitId = _adUnitIdIOS;
#endif

            MaxSdkCallbacks.RewardedInterstitial.OnAdLoadedEvent += OnRewardedInterstitialAdLoadedEvent;
            MaxSdkCallbacks.RewardedInterstitial.OnAdLoadFailedEvent += OnRewardedInterstitialAdFailedEvent;
            MaxSdkCallbacks.RewardedInterstitial.OnAdDisplayFailedEvent += OnRewardedInterstitialAdFailedToDisplayEvent;
            MaxSdkCallbacks.RewardedInterstitial.OnAdDisplayedEvent += OnRewardedInterstitialAdDisplayedEvent;
            MaxSdkCallbacks.RewardedInterstitial.OnAdClickedEvent += OnRewardedInterstitialAdClickedEvent;
            MaxSdkCallbacks.RewardedInterstitial.OnAdHiddenEvent += OnRewardedInterstitialAdDismissedEvent;
            MaxSdkCallbacks.RewardedInterstitial.OnAdReceivedRewardEvent += OnRewardedInterstitialAdReceivedRewardEvent;
            MaxSdkCallbacks.RewardedInterstitial.OnAdRevenuePaidEvent += OnRewardedInterstitialAdRevenuePaidEvent;
        }

        protected virtual void Start()
        {
            Initialize();
            Load();
        }

        public void Load()
        {
            MGLogger.LogInfo("Rewarded Interstitial Ad is attempting to load..", MegaCore.Module.mediation);
            if (string.IsNullOrEmpty(_adUnitId))
            {
                Debug.LogError("MG: Mediation: Rewarded Interstitial Ad Unit ID is no defined!!");
                return;
            }
            MaxSdk.LoadRewardedInterstitialAd(_adUnitId);
        }

        public void Show()
        {
            MGLogger.LogInfo("Rewarded Interstitial Ad is attempting to show..", MegaCore.Module.mediation);
            if (string.IsNullOrEmpty(_adUnitId))
            {
                Debug.LogError("MG: Mediation: Rewarded Interstitial Ad Unit ID is no defined!!");
                return;
            }
            if (MaxSdk.IsRewardedInterstitialAdReady(_adUnitId))
                MaxSdk.ShowRewardedInterstitialAd(_adUnitId);
        }

        public virtual void OnRewardedInterstitialAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogInfo(string.Format("Rewarded Interstitial Ad loaded by {0}/{1}", adInfo.NetworkName, adInfo.NetworkPlacement), MegaCore.Module.mediation);

            // Reset retry attempt
            rewardedInterstitialRetryAttempt = 0;
        }

        public virtual void OnRewardedInterstitialAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            MGLogger.LogWarning("Rewarded Interstitial ad failed to load with error code: " + errorInfo.Code, MegaCore.Module.mediation);

            // Rewarded interstitial ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
            rewardedInterstitialRetryAttempt++;
            double retryDelay = Mathf.Pow(2, Mathf.Min(6, rewardedInterstitialRetryAttempt));

            Invoke("Load", (float)retryDelay);
        }

        public virtual void OnRewardedInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogWarning("Rewarded interstitial Ad failed to display", MegaCore.Module.mediation);

            // Rewarded interstitial ad failed to display. We recommend loading the next ad
            Load();
        }

        public virtual void OnRewardedInterstitialAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogInfo(string.Format("Rewarded Ad displayed by {0}/{1}", adInfo.NetworkName, adInfo.NetworkPlacement), MegaCore.Module.mediation);
            //Load();
        }

        public virtual void OnRewardedInterstitialAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogInfo("Rewarded interstitial Ad clicked", MegaCore.Module.mediation);
            //Load();
        }

        public virtual void OnRewardedInterstitialAdDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded interstitial ad is hidden. Pre-load the next ad
            MGLogger.LogInfo("Rewarded interstitial Ad dismissed", MegaCore.Module.mediation);
            Load();
        }

        public virtual void OnRewardedInterstitialAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded interstitial ad was displayed and user should receive the reward
            MGLogger.LogInfo(string.Format("Rewarded interstitial Ad received reward by {0}/{1}", adInfo.NetworkName, adInfo.NetworkPlacement), MegaCore.Module.mediation);
        }

        public virtual void OnRewardedInterstitialAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded interstitial ad revenue paid. Use this callback to track user revenue.
            MGLogger.LogInfo(string.Format("Rewarded interstitial Ad revenue paid by {0}/{1}", adInfo.NetworkName, adInfo.NetworkPlacement), MegaCore.Module.mediation);
        }
    }


}