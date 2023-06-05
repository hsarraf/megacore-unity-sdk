namespace MegaCore.MediationService
{

    public interface IMGInterstitialEvents
    {
        void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo);

        void OnInterstitialFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo);

        void OnInterstitialFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo);

        void OnInterstitialDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo);

        void OnInterstitialRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo);
    }

}
