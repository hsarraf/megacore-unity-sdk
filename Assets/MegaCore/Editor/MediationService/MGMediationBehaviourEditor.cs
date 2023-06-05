using UnityEditor;
using UnityEngine;

using AppLovinMax.Scripts.IntegrationManager.Editor;


namespace MegaCore.MediationService
{

    [ExecuteInEditMode]
    [CustomEditor(typeof(MGMediationBehaviour))]
    public class MGMediationBehaviourEditor : Editor
    {
        MGMediationBehaviour _mediationBehaviour;

        private Texture2D _bannerAdIcon = null;
        private Texture2D _interstitialAdIcon = null;
        private Texture2D _rewardedAdIcon = null;
        private Texture2D _rewardedInterstitialAdIcon = null;
        private Texture2D _MRecAdIcon = null;

        SerializedProperty _maxSdkKey;

        SerializedProperty _useConsent;
        SerializedProperty _publisherID;
        SerializedProperty _consentTestMode;
        SerializedProperty _privacyPolicyUrl;
        SerializedProperty _termsOfServiceUrl;

        SerializedProperty _adMobIOSAppId;
        SerializedProperty _adMobAndroidAppId;

        SerializedProperty _onMediaionInitializedEvent;
        SerializedProperty _onConsentDismissedEvent;

        void OnEnable()
        {
            _mediationBehaviour = target as MGMediationBehaviour;

            _mediationBehaviour.GetComponent<Transform>().hideFlags |= HideFlags.HideInInspector;

            _bannerAdIcon = Resources.Load<Texture2D>("Mediation/Icons/BannerAd");
            _interstitialAdIcon = Resources.Load<Texture2D>("Mediation/Icons/InterstitialAd");
            _rewardedAdIcon = Resources.Load<Texture2D>("Mediation/Icons/RewardedAd");
            _rewardedInterstitialAdIcon = Resources.Load<Texture2D>("Mediation/Icons/RewardedInterstitialAd");
            _MRecAdIcon = Resources.Load<Texture2D>("Mediation/Icons/MRecAd");

            _maxSdkKey = serializedObject.FindProperty("_maxSdkKey");

            _useConsent = serializedObject.FindProperty("_useConsent");
            _publisherID = serializedObject.FindProperty("_publisherID");
            _consentTestMode = serializedObject.FindProperty("_consentTestMode");
            _privacyPolicyUrl = serializedObject.FindProperty("_privacyPolicyUrl");
            _termsOfServiceUrl = serializedObject.FindProperty("_termsOfServiceUrl");

            _adMobIOSAppId = serializedObject.FindProperty("_adMobIosAppId");
            _adMobAndroidAppId = serializedObject.FindProperty("_adMobAndroidAppId");

            _onMediaionInitializedEvent = serializedObject.FindProperty("_onMediaionInitializedEvent");
            _onConsentDismissedEvent = serializedObject.FindProperty("_onConsentDismissedEvent");

            AppLovinSettings.Instance.SdkKey = _maxSdkKey.stringValue;

            AppLovinSettings.Instance.AdMobAndroidAppId = _adMobAndroidAppId.stringValue;
            AppLovinSettings.Instance.AdMobIosAppId = _adMobIOSAppId.stringValue;

            AppLovinSettings.Instance.ConsentFlowPrivacyPolicyUrl = _privacyPolicyUrl.stringValue;
            AppLovinSettings.Instance.ConsentFlowTermsOfServiceUrl = _termsOfServiceUrl.stringValue;
        }


        public override void OnInspectorGUI()
        {
            MGStyleEditor.DrawHeader("MEDIATION");

            GUILayout.BeginVertical();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent(_interstitialAdIcon, "Interstitial"), new GUIStyle(GUI.skin.button), GUILayout.Width(60), GUILayout.Height(90)))
                InterstitialAd();
            EditorGUILayout.Separator();
            if (GUILayout.Button(new GUIContent(_rewardedAdIcon, "Rewarded"), new GUIStyle(GUI.skin.button), GUILayout.Width(60), GUILayout.Height(90)))
                RewardedAd();
            EditorGUILayout.Separator();
            if (GUILayout.Button(new GUIContent(_rewardedInterstitialAdIcon, "Rewarded Interstitial"), new GUIStyle(GUI.skin.button), GUILayout.Width(60), GUILayout.Height(90)))
                RewardedInterstitialAd();
            EditorGUILayout.Separator();
            if (GUILayout.Button(new GUIContent(_bannerAdIcon, "Banner"), new GUIStyle(GUI.skin.button), GUILayout.Width(60), GUILayout.Height(90)))
                BannerAd();
            EditorGUILayout.Separator();
            if (GUILayout.Button(new GUIContent(_MRecAdIcon, "MRec"), new GUIStyle(GUI.skin.button), GUILayout.Width(60), GUILayout.Height(90)))
                MRecAd();
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            serializedObject.Update();

            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(_maxSdkKey, new GUIContent("Max SDK Key"), GUILayout.ExpandWidth(true));
            if (GUILayout.Button("Find", new GUIStyle(GUI.skin.button), GUILayout.Width(50)))
                Application.OpenURL("https://dash.applovin.com/o/account#keys");
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            serializedObject.Update();

            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(_onMediaionInitializedEvent, new GUIContent("Initialized Callback"));
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(_onConsentDismissedEvent, new GUIContent("Consent Dismissed Callback"));
            GUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            Separator();

            EditorGUILayout.PropertyField(_useConsent, new GUIContent("Use Consent"), GUILayout.ExpandWidth(true));
            EditorGUILayout.Separator();
            if (_mediationBehaviour._useConsent)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(_publisherID, new GUIContent("Android Publisher ID"), GUILayout.ExpandWidth(true));
                if (GUILayout.Button("Find", GUILayout.Width(50)))
                    Application.OpenURL("https://apps.admob.com/v2/settings/account-info");
                GUILayout.EndHorizontal();
                EditorGUILayout.Separator();
                EditorGUILayout.Separator();
                EditorGUILayout.PropertyField(_privacyPolicyUrl, new GUIContent("Privacy Policy URL"), GUILayout.ExpandWidth(true));
                EditorGUILayout.PropertyField(_termsOfServiceUrl, new GUIContent("Terms of Service URL"), GUILayout.ExpandWidth(true));
                EditorGUILayout.Separator();
                EditorGUILayout.Separator();
                EditorGUILayout.PropertyField(_consentTestMode, new GUIContent("Consent Test Mode"), GUILayout.ExpandWidth(true));
                EditorGUILayout.Separator();
                Separator();
            }

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(_adMobAndroidAppId, new GUIContent("Google AdMob App ID (Android)"), GUILayout.ExpandWidth(true));
            EditorGUILayout.PropertyField(_adMobIOSAppId, new GUIContent("Google AdMob App ID (iOS)"), GUILayout.ExpandWidth(true));

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            Separator();

            GUILayout.BeginHorizontal();
            EditorGUILayout.Separator();
            if (GUILayout.Button("Open Integration Manager", GUILayout.Width(250), GUILayout.Height(40)))
            {
                AppLovinIntegrationManagerWindow.ShowManager();
                AppLovinSettings.Instance.SdkKey = _maxSdkKey.stringValue;
                AppLovinSettings.Instance.AdMobIosAppId = _adMobIOSAppId.stringValue;
                AppLovinSettings.Instance.AdMobAndroidAppId = _adMobAndroidAppId.stringValue;
                AppLovinSettings.Instance.ConsentFlowPrivacyPolicyUrl = _privacyPolicyUrl.stringValue;
                AppLovinSettings.Instance.ConsentFlowTermsOfServiceUrl = _termsOfServiceUrl.stringValue;
            }
            EditorGUILayout.Separator();
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

        }

        void Separator()
        {
            GUI.skin = null;
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, GUILayout.ExpandWidth(true));
            GUI.skin = MGStyleEditor._guiSkin;
        }


        /// <summary>
        /// Menu to handle Mediation Service
        /// </summary>
        /// 
        public static MGMediationBehaviour GetMediationBehaviour()
        {
            MGMediationBehaviour mediationBehaviour = FindObjectOfType<MGMediationBehaviour>();
            if (mediationBehaviour == null)
            {
                MGHelper.LogInfo("Mediation service created");
                mediationBehaviour = new GameObject("MG: Mediation").AddComponent<MGMediationBehaviour>();
            }
            Selection.activeGameObject = mediationBehaviour.gameObject;
            return mediationBehaviour;
        }


        [MenuItem("MegaCore/Mediation/Interstitial Ad")]
        static void InterstitialAd()
        {
            MGMediationBehaviour mediationBehaviour = GetMediationBehaviour();
            MGInterstitialAdDelegate interstitialAd = mediationBehaviour.gameObject.GetComponent<MGInterstitialAdDelegate>();
            if (interstitialAd == null)
            {
                interstitialAd = mediationBehaviour.gameObject.AddComponent<MGInterstitialAdDelegate>();
                MGHelper.LogInfo("Intersitial ad component created");
            }
            else
            {
                MGHelper.LogWarnong("Intersitial ad component already exists");
            }
            mediationBehaviour.InterstitialAd = interstitialAd;
        }


        [MenuItem("MegaCore/Mediation/Rewarded Ad")]
        static void RewardedAd()
        {
            MGMediationBehaviour mediationBehaviour = GetMediationBehaviour();
            MGRewardedAdDelegate rewardedAd = mediationBehaviour.gameObject.GetComponent<MGRewardedAdDelegate>();
            if (rewardedAd == null)
            {
                rewardedAd = mediationBehaviour.gameObject.AddComponent<MGRewardedAdDelegate>();
                MGHelper.LogInfo("Rewarded ad component created");
            }
            else
            {
                MGHelper.LogWarnong("Rewarded ad component already exists");
            }
            mediationBehaviour.RewardedAd = rewardedAd;
        }


        [MenuItem("MegaCore/Mediation/Rewarded Interstitial Ad")]
        static void RewardedInterstitialAd()
        {
            MGMediationBehaviour mediationBehaviour = GetMediationBehaviour();
            MGRewardedInterstitialAdDelegate rewardedInterstitialAd = mediationBehaviour.gameObject.GetComponent<MGRewardedInterstitialAdDelegate>();
            if (rewardedInterstitialAd == null)
            {
                rewardedInterstitialAd = mediationBehaviour.gameObject.AddComponent<MGRewardedInterstitialAdDelegate>();
                MGHelper.LogInfo("Rewarded Intersitial ad component created");
            }
            else
            {
                MGHelper.LogWarnong("Rewarded intersitial ad component already exists");
            }
            mediationBehaviour.RewardedInterstitialAd = rewardedInterstitialAd;
        }


        [MenuItem("MegaCore/Mediation/Banner Ad")]
        static void BannerAd()
        {
            MGMediationBehaviour mediationBehaviour = GetMediationBehaviour();
            MGBannerAdDelegate bannerAd = mediationBehaviour.gameObject.GetComponent<MGBannerAdDelegate>();
            if (bannerAd == null)
            {
                bannerAd = mediationBehaviour.gameObject.AddComponent<MGBannerAdDelegate>();
                MGHelper.LogInfo("Banner ad component created");
            }
            else
            {
                MGHelper.LogWarnong("Banner ad component already exists");
            }
            mediationBehaviour.BannerAd = bannerAd;
        }


        [MenuItem("MegaCore/Mediation/MRec Ad")]
        static void MRecAd()
        {
            MGMediationBehaviour mediationBehaviour = GetMediationBehaviour();
            MGMRecAdDelegate mRecAd = mediationBehaviour.gameObject.GetComponent<MGMRecAdDelegate>();
            if (mRecAd == null)
            {
                mRecAd = mediationBehaviour.gameObject.AddComponent<MGMRecAdDelegate>();
                MGHelper.LogInfo("MGMRec ad component created");
            }
            else
            {
                MGHelper.LogWarnong("MGMRec ad component already exists");
            }
            mediationBehaviour.MRecAd = mRecAd;
        }

    }

}