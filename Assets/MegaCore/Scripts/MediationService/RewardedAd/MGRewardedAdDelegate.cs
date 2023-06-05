using UnityEngine;

namespace MegaCore.MediationService
{

    public class MGRewardedAdDelegate : MGRewardedAd
    {
        protected override void Start()
        {
            base.Start();
        }

        public override void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnRewardedAdLoadedEvent(adUnitId, adInfo);
        }

        public override void OnRewardedAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            base.OnRewardedAdFailedEvent(adUnitId, errorInfo);
        }

        public override void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            base.OnRewardedAdFailedToDisplayEvent(adUnitId, errorInfo, adInfo);
        }

        public override void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnRewardedAdDisplayedEvent(adUnitId, adInfo);
        }

        public override void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnRewardedAdClickedEvent(adUnitId, adInfo);
        }

        public override void OnRewardedAdDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnRewardedAdDismissedEvent(adUnitId, adInfo);
        }

        /* called after the player has watched the rewarded ad */
        public override void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdkBase.Reward reward, MaxSdkBase.AdInfo adInfo)
        {
            base.OnRewardedAdReceivedRewardEvent(adUnitId, reward, adInfo);
        }

        public override void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnRewardedAdRevenuePaidEvent(adUnitId, adInfo);
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                Show();
        }
#endif

    }

}
