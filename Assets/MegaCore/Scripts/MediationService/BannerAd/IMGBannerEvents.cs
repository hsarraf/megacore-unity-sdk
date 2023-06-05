namespace MegaCore.MediationService
{

    public interface IMGBannerEvents
    {
        void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo);

        void OnBannerAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo);

        void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo);

        void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo);
    }

}
