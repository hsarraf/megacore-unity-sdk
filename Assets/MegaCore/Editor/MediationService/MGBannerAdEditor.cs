using UnityEditor;
using UnityEngine;


namespace MegaCore.MediationService
{

    [ExecuteInEditMode]
    [CustomEditor(typeof(MGBannerAdDelegate))]
    public class MGBannerAdEditor : Editor
    {
        MGBannerAdDelegate _bannerAd;

        SerializedProperty _adUnitIdAndroid;
        SerializedProperty _adUnitIdIOS;

        SerializedProperty _backgroundColor;
        SerializedProperty _bannerPosition;

        GUISkin _logGuiSkin;

        void OnEnable()
        {
            _bannerAd = (MGBannerAdDelegate)target;

            _adUnitIdAndroid = serializedObject.FindProperty("_adUnitIdAndroid");
            _adUnitIdIOS = serializedObject.FindProperty("_adUnitIdIOS");

            _backgroundColor = serializedObject.FindProperty("_backgroundColor");
            _bannerPosition = serializedObject.FindProperty("_bannerPosition");

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

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(_backgroundColor, new GUIContent("Background Color"));

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(_bannerPosition, new GUIContent("Banner Position"));

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Separator();

            if (GUILayout.Button("Open Delegate", GUILayout.Width(140), GUILayout.Height(40)))
            {
                Application.OpenURL(string.Format("file://{0}/MegaCore/Scripts/MediationService/BannerAd/MGBannerAdDelegate.cs", Application.dataPath));
            }
        }

    }

}