using UnityEngine;

using MegaCore.Logger;


namespace MegaCore.MediationService
{

    public class MGRewardedAd : MGMediationAbsatrct, IMGRewardedEvents
    {

        int _rewardedRetryAttempt;
       
        public void Initialize()
        {

#if UNITY_ANDROID
            _adUnitId = _adUnitIdAndroid;
#elif UNITY_IOS
            _adUnitId = _adUnitIdIOS;
#endif

            // Attach callbacks
            //
            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdFailedEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
            MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdRevenuePaidEvent;

            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdDismissedEvent;
            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
        }

        protected virtual void Start()
        {
            Initialize();
            Load();
        }


        public void Load()
        {
            MGLogger.LogInfo("Rewarded Ad is attempting to load..", MegaCore.Module.mediation);
            if (string.IsNullOrEmpty(_adUnitId))
            {
                Debug.LogError("MG: Mediation: Rewarded Ad Unit ID is no defined!!");
                return;
            }
            MaxSdk.LoadRewardedAd(_adUnitId);
        }

        public void Show()
        {
            MGLogger.LogInfo("Rewarded Ad is attempting to show..", MegaCore.Module.mediation);
            if (string.IsNullOrEmpty(_adUnitId))
            {
                Debug.LogError("MG: Mediation: Rewarded Ad Unit ID is no defined!!");
                return;
            }
            if (MaxSdk.IsRewardedAdReady(_adUnitId))
            {
                MaxSdk.ShowRewardedAd(_adUnitId);
            }
        }

        public virtual void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogInfo(string.Format("Rewarded Ad loaded by {0}/{1}", adInfo.NetworkName, adInfo.NetworkPlacement), MegaCore.Module.mediation);

            // Reset retry attempt
            _rewardedRetryAttempt = 0;
        }

        public virtual void OnRewardedAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            MGLogger.LogWarning("Rewarded ad failed to load with error code: " + errorInfo.Code, MegaCore.Module.mediation);

            _rewardedRetryAttempt++;
            double retryDelay = Mathf.Pow(2, Mathf.Min(6, _rewardedRetryAttempt));
            Invoke("Load", (float)retryDelay);
        }

        public virtual void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogWarning("Rewarded Ad failed to display", MegaCore.Module.mediation);
            Load();
        }

        public virtual void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogInfo(string.Format("Rewarded Ad displayed by {0}/{1}", adInfo.NetworkName, adInfo.NetworkPlacement), MegaCore.Module.mediation);
            //Load();
        }

        public virtual void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogInfo("Rewarded Ad clicked", MegaCore.Module.mediation);
            //Load();
        }

        public virtual void OnRewardedAdDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad is hidden. Pre-load the next ad
            MGLogger.LogInfo("Rewarded Ad dismissed", MegaCore.Module.mediation);
            Load();
        }

        public virtual void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdkBase.Reward reward,  MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad was displayed and user should receive the reward
            MGLogger.LogInfo(string.Format("Rewarded Ad received reward by {0}/{1}", adInfo.NetworkName, adInfo.NetworkPlacement), MegaCore.Module.mediation);
        }

        public virtual void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad revenue paid. Use this callback to track user revenue.
            MGLogger.LogInfo(string.Format("Rewarded Ad revenue paid by {0}/{1}", adInfo.NetworkName, adInfo.NetworkPlacement), MegaCore.Module.mediation);
        }
    }

}