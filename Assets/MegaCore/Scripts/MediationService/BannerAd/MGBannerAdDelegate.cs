using UnityEngine;

namespace MegaCore.MediationService
{

    public class MGBannerAdDelegate : MGBannerAd
    {
        protected override void Start()
        {
            base.Start();
        }

        public override void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnBannerAdLoadedEvent(adUnitId, adInfo);
        }

        public override void OnBannerAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            base.OnBannerAdFailedEvent(adUnitId, errorInfo);
        }

        public override void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnBannerAdClickedEvent(adUnitId, adInfo);
        }

        public override void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnBannerAdRevenuePaidEvent(adUnitId, adInfo);
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.B))
                Show();
        }
#endif

    }

}
