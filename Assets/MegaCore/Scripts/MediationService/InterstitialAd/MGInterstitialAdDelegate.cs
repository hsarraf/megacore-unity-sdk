using UnityEngine;

namespace MegaCore.MediationService
{

    public class MGInterstitialAdDelegate : MGInterstitialAd
    {
        protected override void Start()
        {
            base.Start();
        }

        public override void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnInterstitialLoadedEvent(adUnitId, adInfo);
        }

        public override void OnInterstitialFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            base.OnInterstitialFailedEvent(adUnitId, errorInfo);
        }

        public override void OnInterstitialFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            base.OnInterstitialFailedToDisplayEvent(adUnitId, errorInfo, adInfo);
        }

        public override void OnInterstitialDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnInterstitialDismissedEvent(adUnitId, adInfo);
        }

        /* called after the player has watched the interstitial ad */
        public override void OnInterstitialRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnInterstitialRevenuePaidEvent(adUnitId, adInfo);
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
                Show();
        }
#endif

    }

}
