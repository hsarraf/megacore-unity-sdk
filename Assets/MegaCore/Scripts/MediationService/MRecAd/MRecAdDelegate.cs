using UnityEngine;

namespace MegaCore.MediationService
{

    public class MGMRecAdDelegate : MGMRecAd
    {
        protected override void Start()
        {
            base.Start();
        }

        public override void OnMRecAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnMRecAdLoadedEvent(adUnitId, adInfo);
        }

        public override void OnMRecAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            base.OnMRecAdFailedEvent(adUnitId, errorInfo);
        }

        public override void OnMRecAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnMRecAdClickedEvent(adUnitId, adInfo);
        }

        public override void OnMRecAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            base.OnMRecAdRevenuePaidEvent(adUnitId, adInfo);
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
                Show();
        }
#endif

    }

}
