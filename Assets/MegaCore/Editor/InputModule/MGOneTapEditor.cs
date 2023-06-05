using UnityEngine;
using UnityEditor;

namespace MegaCore.InputModule
{
    [ExecuteInEditMode]
    [CustomEditor(typeof(OneTapHandler))]
    public class TapHandlerEditor : Editor
    {
        OneTapHandler _tapHandler;

        SerializedProperty _handScaleFactor;

        SerializedProperty _startTapcallback;
        SerializedProperty _endTapcallback;

        void OnEnable()
        {
            _tapHandler = (OneTapHandler)target;

            _handScaleFactor = serializedObject.FindProperty("_handScaleFactor");

            _startTapcallback = serializedObject.FindProperty("_onTapped");
            _endTapcallback = serializedObject.FindProperty("_onUntapped");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(Resources.Load<Texture2D>("InputHandler/HandOn"), GUILayout.Width(50), GUILayout.Height(50)))
                if (_tapHandler._handGizmo == null)
                {
                    _tapHandler._handGizmo = Instantiate(Resources.Load<MGInputHandGizmo>("InputHandler/HandOn"));
                    _tapHandler._handGizmo.transform.SetParent(_tapHandler.transform);
                    _tapHandler._handGizmo.gameObject.SetActive(false);
                }
            if (GUILayout.Button(Resources.Load<Texture2D>("InputHandler/HandOff"), GUILayout.Width(50), GUILayout.Height(50)))
                if (_tapHandler._handGizmo != null)
                {
                    DestroyImmediate(_tapHandler._handGizmo.gameObject);
                    _tapHandler._handGizmo = null;
                }
            EditorGUILayout.BeginVertical();
            EditorGUILayout.TextField(string.Format("status: {0}", _tapHandler._callbakType.ToString()));
            EditorGUILayout.TextField(string.Format("position: {0}", _tapHandler._touchPosition.ToString()));
            EditorGUILayout.TextField(string.Format("velocity: {0}", _tapHandler._touchSpeedVector.ToString()));
            EditorGUILayout.TextField(string.Format("speed: {0}", _tapHandler._touchSpeedVector.magnitude.ToString()));
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            serializedObject.Update();

            EditorGUILayout.PropertyField(_startTapcallback, new GUIContent("On Tap Started"));
            EditorGUILayout.PropertyField(_endTapcallback, new GUIContent("On Tapp Ended"));

            serializedObject.ApplyModifiedProperties();
        }

    }

}