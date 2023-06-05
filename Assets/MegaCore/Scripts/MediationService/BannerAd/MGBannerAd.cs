using UnityEngine;

using MegaCore.Logger;


namespace MegaCore.MediationService
{

    public class MGBannerAd : MGMediationAbsatrct, IMGBannerEvents
    {
        private bool _isBannerShowing;

        public Color _backgroundColor = Color.black;
        public MaxSdkBase.BannerPosition _bannerPosition = MaxSdkBase.BannerPosition.TopCenter;


        public void Initialize()
        {

#if UNITY_ANDROID
            _adUnitId = _adUnitIdAndroid;
#elif UNITY_IOS
            _adUnitId = _adUnitIdIOS;
#endif

            // Attach Callbacks
            //
            MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerAdLoadedEvent;
            MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerAdFailedEvent;
            MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerAdClickedEvent;
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;

            //// Banners are automatically sized to 320x50 on phones and 728x90 on tablets.
            //// You may use the utility method `MaxSdkUtils.isTablet()` to help with view sizing adjustments.
            MaxSdk.CreateBanner(_adUnitId, _bannerPosition);

            //// Set background or background color for banners to be fully functional.
            MaxSdk.SetBannerBackgroundColor(_adUnitId, _backgroundColor);
        }

        protected virtual void Start()
        {
            Initialize();
        }

        public void Show()
        {
            MaxSdk.ShowBanner(_adUnitId);
        }

        public void Hide()
        {
            MaxSdk.HideBanner(_adUnitId);
        }

        public virtual void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogInfo(string.Format("Banner Ad loaded by {0}/{1}", adInfo.NetworkName, adInfo.NetworkPlacement), MegaCore.Module.mediation);
        }

        public virtual void OnBannerAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            MGLogger.LogWarning("Banner Ad loading failed!!", MegaCore.Module.mediation);
        }

        public virtual void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogInfo("Banner Ad clicked", MegaCore.Module.mediation);
        }

        public virtual void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            MGLogger.LogInfo(string.Format("Banner Ad revenue paid by {0}/{1}", adInfo.NetworkName, adInfo.NetworkPlacement), MegaCore.Module.mediation);
        }
    }

}