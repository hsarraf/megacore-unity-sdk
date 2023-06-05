using UnityEngine;

using MegaCore.Logger;


namespace MegaCore.MediationService
{

    public class MGMRecAd : MGMediationAbsatrct, IMGMRecEvents
    {

        private bool _isMRecShowing;

        public MaxSdkBase.AdViewPosition _mrecPosition;

        public void Initialize()
        {

#if UNITY_ANDROID
            _adUnitId = _adUnitIdAndroid;
#elif UNITY_IOS
            _adUnitId = _adUnitIdIOS;
#endif

            if (string.IsNullOrEmpty(_adUnitId))
            {
                Debug.LogError("MG: Mediation: MRed Ad Unit ID is no defined!!");
                return;
            }
            // Attach Callbacks
            MaxSdkCallbacks.MRec.OnAdLoadedEvent += OnMRecAdLoadedEvent;
            MaxSdkCallbacks.MRec.OnAdLoadFailedEvent += OnMRecAdFailedEvent;
            MaxSdkCallbacks.MRec.OnAdClickedEvent += OnMRecAdClickedEvent;
            MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent += OnMRecAdRevenuePaidEvent;

            //// MRECs are automatically sized to 300x250.
            MaxSdk.CreateMRec(_adUnitId, _mrecPosition);
        }

        protected virtual void Start()
        {
            Initialize();
        }

        public void Show()
        {
            MaxSdk.ShowMRec(_adUnitId);
        }

        public void Hide()
        {
            MaxSdk.HideMRec(_adUnitId);
        }

        public virtual void OnMRecAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogInfo(string.Format("MRec ad loaded by {0}/{1}", adInfo.NetworkName, adInfo.NetworkPlacement), MegaCore.Module.mediation);
        }

        public virtual void OnMRecAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            MGLogger.LogError("MRec ad loading failed", MegaCore.Module.mediation);
        }

        public virtual void OnMRecAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogInfo("MRec ad clicked", MegaCore.Module.mediation);
        }

        public virtual void OnMRecAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogInfo(string.Format("MRec ad revenue paid by {0}/{1}", adInfo.NetworkName, adInfo.NetworkPlacement), MegaCore.Module.mediation);
        }
    }

}