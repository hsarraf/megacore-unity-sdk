using UnityEditor;
using UnityEngine;


namespace MegaCore.MediationService
{

    [ExecuteInEditMode]
    [CustomEditor(typeof(MGInterstitialAdDelegate))]
    public class MGInterstitialAdEditor : Editor
    {
        MGInterstitialAdDelegate _interstitialAd;

        SerializedProperty _adUnitIdAndroid;
        SerializedProperty _adUnitIdIOS;

        GUISkin _logGuiSkin;

        void OnEnable()
        {
            _interstitialAd = (MGInterstitialAdDelegate)target;

            _adUnitIdAndroid = serializedObject.FindProperty("_adUnitIdAndroid");
            _adUnitIdIOS = serializedObject.FindProperty("_adUnitIdIOS");

            _logGuiSkin = Resources.Load<GUISkin>("Mediation/GUISkin");
        }

        public override void OnInspectorGUI()
        {
            GUI.skin = _logGuiSkin;

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            serializedObject.Update();
            EditorGUILayout.PropertyField(_adUnitIdAndroid, new GUIContent("Android Ad Unit Id"));
            EditorGUILayout.PropertyField(_adUnitIdIOS, new GUIContent("iOS Ad Unit Id"));
            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Separator();

            if (GUILayout.Button("Open Delegate", GUILayout.Width(140), GUILayout.Height(40)))
            {
                Application.OpenURL(string.Format("file://{0}/MegaCore/Scripts/MediationService/InterstitialAd/MGInterstitialAdDelegate.cs", Application.dataPath));
            }

        }

    }

}