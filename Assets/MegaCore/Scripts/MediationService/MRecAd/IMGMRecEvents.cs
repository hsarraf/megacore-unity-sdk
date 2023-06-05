namespace MegaCore.MediationService
{

    public interface IMGMRecEvents
    {
        void OnMRecAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo);

        void OnMRecAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo);

        void OnMRecAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo);

        void OnMRecAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo);
    }

}