using UnityEngine;

namespace MegaCore.MediationService
{

    public class MGRewardedInterstitialAdDelegate : MGRewardedInterstitialAd
    {
        protected override void Start()
        {
            base.Start();
        }

        public override void OnRewardedInterstitialAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnRewardedInterstitialAdLoadedEvent(adUnitId, adInfo);
        }

        public override void OnRewardedInterstitialAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            base.OnRewardedInterstitialAdFailedEvent(adUnitId, errorInfo);
        }

        public override void OnRewardedInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            base.OnRewardedInterstitialAdFailedToDisplayEvent(adUnitId, errorInfo, adInfo);
        }

        public override void OnRewardedInterstitialAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnRewardedInterstitialAdDisplayedEvent(adUnitId, adInfo);
        }

        public override void OnRewardedInterstitialAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnRewardedInterstitialAdClickedEvent(adUnitId, adInfo);
        }

        public override void OnRewardedInterstitialAdDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnRewardedInterstitialAdDismissedEvent(adUnitId, adInfo);
        }

        public override void OnRewardedInterstitialAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
        {
            base.OnRewardedInterstitialAdReceivedRewardEvent(adUnitId, reward, adInfo);
        }

        public override void OnRewardedInterstitialAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnRewardedInterstitialAdRevenuePaidEvent(adUnitId, adInfo);
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Y))
                Show();
        }
#endif

    }

}
