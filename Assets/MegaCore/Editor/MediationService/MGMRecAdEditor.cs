using UnityEditor;
using UnityEngine;


namespace MegaCore.MediationService
{

    [ExecuteInEditMode]
    [CustomEditor(typeof(MGMRecAdDelegate))]
    public class MGMRecAdEditor : Editor
    {
        MGMRecAdDelegate _bannerAd;

        SerializedProperty _adUnitIdAndroid;
        SerializedProperty _adUnitIdIOS;

        SerializedProperty _bannerPosition;

        GUISkin _logGuiSkin;

        void OnEnable()
        {
            _bannerAd = (MGMRecAdDelegate)target;

            _adUnitIdAndroid = serializedObject.FindProperty("_adUnitIdAndroid");
            _adUnitIdIOS = serializedObject.FindProperty("_adUnitIdIOS");

            _bannerPosition = serializedObject.FindProperty("_mrecPosition");

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

            EditorGUILayout.PropertyField(_bannerPosition, new GUIContent("Banner Position"));

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Separator();

            if (GUILayout.Button("Open Delegate", GUILayout.Width(140), GUILayout.Height(40)))
            {
                Application.OpenURL(string.Format("file://{0}/MegaCore/Scripts/MediationService/MRecAd/MRecAdDelegate.cs", Application.dataPath));
            }

        }

    }

}