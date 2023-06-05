using UnityEngine;
using UnityEngine.Events;

using MegaCore.Logger;


namespace MegaCore.MediationService
{

    public class MGMediationBehaviour : MGAbstract
    {
        private static MGMediationBehaviour __instance;
        public static MGMediationBehaviour Instance { get { return __instance; } }
        void Awake()
        {
            if (__instance == null)
            {
                __instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (__instance != this)
            {
                Destroy(gameObject);
            }
        }

        public bool _consentDone = false;

        private void Start()
        {

            MGLogger.LogInfo("MAX SDK is Initializing..", MegaCore.Module.mediation);
            MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
            {
                MGLogger.LogInfo("MAX SDK Initialized", MegaCore.Module.mediation);
                _onMediaionInitializedEvent?.Invoke(sdkConfiguration);
            };

            bool consentAvailable = _useConsent && Application.internetReachability != NetworkReachability.NotReachable;
            if (!consentAvailable)
            {
                OnConsentStatusReceived("not applicable");
            }

#if UNITY_ANDROID
            if (consentAvailable)
            {
                AndroidConsent();
            }
#elif UNITY_IOS
            MaxSdkCallbacks.OnSdkConsentDialogDismissedEvent += () =>
            {
                OnConsentStatusReceived("ios");
            };
#endif
            MaxSdk.SetSdkKey(_maxSdkKey);
            MaxSdk.InitializeSdk();
        }


#if UNITY_ANDROID

        void AndroidConsent()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaClass consentClass = new AndroidJavaClass("com.megacore.consent.ConsentIntent");

            consentClass.SetStatic("publisherID", _publisherID);
            consentClass.SetStatic("privacyPolicyURL", _privacyPolicyUrl);
            consentClass.SetStatic("testMode", _consentTestMode);
            consentClass.CallStatic("getConsentStatus", unityActivity);
        }

#endif

        void OnConsentStatusReceived(string status)
        {
            MGLogger.LogInfo(string.Format("MAX consent dismissed with status {0}", status), MegaCore.Module.mediation);
            _consentDone = true;
            _onConsentDismissedEvent?.Invoke();
        }


        // sdk key
        //
        public string _maxSdkKey = null;

        public bool _useConsent = true;
        public string _publisherID = null;
        public bool _consentTestMode = false;
        public string _privacyPolicyUrl = null;
        public string _termsOfServiceUrl = null;

        public string _adMobIosAppId = null;
        public string _adMobAndroidAppId = null;

        // ad type modules
        //
        public MGInterstitialAd InterstitialAd;

        public MGRewardedAd RewardedAd;

        public MGRewardedInterstitialAd RewardedInterstitialAd;

        public MGBannerAd BannerAd;

        public MGMRecAd MRecAd;

        // initilaization callback
        //
        public UnityEvent<MaxSdkBase.SdkConfiguration> _onMediaionInitializedEvent;
        public UnityEvent _onConsentDismissedEvent;


        public void Initialize()
        {

        }


        public void ShowInterstitial()
        {
            InterstitialAd.Show();
        }


        public void ShowRewarded()
        {
            RewardedAd.Show();
        }

        public void ShowRewardedInterstitial()
        {
            RewardedInterstitialAd.Show();
        }

        public void ShowBanner()
        {
            BannerAd.Show();
        }

        public void ShpwMRec()
        {
            MRecAd.Show();
        }

    }

}