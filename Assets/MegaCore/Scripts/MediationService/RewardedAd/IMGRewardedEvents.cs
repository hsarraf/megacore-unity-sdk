namespace MegaCore.MediationService
{

    public interface IMGRewardedEvents
    {
        void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo);

        void OnRewardedAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo);

        void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo);

        void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo);

        void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo);

        void OnRewardedAdDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo);

        void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdkBase.Reward reward, MaxSdkBase.AdInfo adInfo);

        void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo);
    }

}
