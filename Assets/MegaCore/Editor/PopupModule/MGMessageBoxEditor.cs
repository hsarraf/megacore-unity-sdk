using UnityEditor;
using UnityEngine;


namespace MegaCore.Popup
{
    [ExecuteInEditMode]
    [CustomEditor(typeof(MGMessageBox))]
    public class MGMessageBoxEditor : Editor
    {
        MGMessageBox _messageBox;

        SerializedProperty _showAnimCurve;
        SerializedProperty _hideAnimCurve;

        GUISkin _logGuiSkin;

        void OnEnable()
        {
            _messageBox = (MGMessageBox)target;

            _showAnimCurve = serializedObject.FindProperty("_showAnimCurve");
            _hideAnimCurve = serializedObject.FindProperty("_hideAnimCurve");

            _logGuiSkin = Resources.Load<GUISkin>("MGGUISkin");
        }

        public override void OnInspectorGUI()
        {
            GUI.skin = _logGuiSkin;

            EditorGUILayout.Separator();

            serializedObject.Update();

            EditorGUILayout.PropertyField(_showAnimCurve, new GUIContent("Show Motion Curve"), GUILayout.ExpandWidth(true), GUILayout.Height(30));

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(_hideAnimCurve, new GUIContent("Hide Motion Curve"), GUILayout.ExpandWidth(true), GUILayout.Height(30));

            _messageBox._topAnchor = EditorGUILayout.FloatField("Top Anchor", _messageBox._topAnchor);
            _messageBox._bottomAnchor = EditorGUILayout.FloatField("Bottom Anchor", _messageBox._bottomAnchor);
            _messageBox._speed = EditorGUILayout.FloatField("Speed", _messageBox._speed);
            _messageBox._stayDuration = EditorGUILayout.FloatField("Stay Duration", _messageBox._stayDuration);

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Separator();

            if (GUILayout.Button("Select Panel", GUILayout.Width(130), GUILayout.Height(45)))
                Selection.activeGameObject = _messageBox._panel;

            EditorGUILayout.Separator();

            if (GUILayout.Button("Select Text", GUILayout.Width(130), GUILayout.Height(45)))
                Selection.activeGameObject = _messageBox._panel.transform.GetChild(0).gameObject;

        }

    }

}
