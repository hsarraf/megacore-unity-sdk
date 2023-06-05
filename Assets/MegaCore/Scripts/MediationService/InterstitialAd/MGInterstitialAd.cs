using UnityEngine;

using MegaCore.Logger;


namespace MegaCore.MediationService
{

    public class MGInterstitialAd : MGMediationAbsatrct, IMGInterstitialEvents
    {
        public void Initialize()
        {

#if UNITY_ANDROID
            _adUnitId = _adUnitIdAndroid;
#elif UNITY_IOS
            _adUnitId = _adUnitIdIOS;
#endif

            // Attach callbacks
            //
            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialFailedEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialFailedToDisplayEvent;
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialDismissedEvent;
            MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnInterstitialRevenuePaidEvent;
        }

        protected virtual void Start()
        {
            Initialize();
            Load();
        }

        public void Load()
        {
            if (string.IsNullOrEmpty(_adUnitId))
            {
                MGLogger.LogError("Interstitial Ad Unit ID is no defined!!", MegaCore.Module.mediation);
                Debug.LogError("MG: Mediation: Interstitial Ad Unit ID is no defined!!");
                return;
            }
            MGLogger.LogInfo("Interstitial is loadeding..", MegaCore.Module.mediation);
            MaxSdk.LoadInterstitial(_adUnitId);
        }

        public bool Show()
        {
            if (string.IsNullOrEmpty(_adUnitId))
            {
                MGLogger.LogError("Interstitial Ad Unit ID is no defined", MegaCore.Module.mediation);
                Debug.LogError("MG: Mediation: Interstitial Ad Unit ID is no defined!!");
                return false;
            }
            MGLogger.LogInfo("Interstitial is trying to show..", MegaCore.Module.mediation);
            if (MaxSdk.IsInterstitialReady(_adUnitId))
            {
                MGLogger.LogInfo("Interstitial is showing..", MegaCore.Module.mediation);
                MaxSdk.ShowInterstitial(_adUnitId);
                return true;
            }
            else
            {
                MGLogger.LogWarning("Interstitial is not ready to show", MegaCore.Module.mediation);
                return false;
            }

        }

        public virtual void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogInfo(string.Format("Interstitial loaded by {0}/{1}", adInfo.NetworkName, adInfo.NetworkPlacement), MegaCore.Module.mediation);

            _retryAttempt = 0;
        }

        public virtual void OnInterstitialFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            _retryAttempt++;
            double retryDelay = Mathf.Pow(2, Mathf.Min(6, _retryAttempt));

            MGLogger.LogWarning("Interstitial failed to load with error code: " + errorInfo.Code, MegaCore.Module.mediation);
            Debug.Log("MG: Mediation: Interstitial failed to load with error code: " + errorInfo.Message);
            Invoke("Load", (float)retryDelay);
        }

        public virtual void OnInterstitialFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogWarning("Interstitial failed to display with error code: " + errorInfo.Code, MegaCore.Module.mediation);

            Load();
        }

        public virtual void OnInterstitialDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogWarning("Interstitial Ad dismissed", MegaCore.Module.mediation);

            Load();
        }

        public virtual void OnInterstitialRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogInfo(string.Format("Interstitial revenue paid by {0}/{1}", adInfo.NetworkName, adInfo.NetworkPlacement), MegaCore.Module.mediation);
        }

    }

}