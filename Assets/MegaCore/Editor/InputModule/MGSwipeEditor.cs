using UnityEngine;
using UnityEditor;

namespace MegaCore.InputModule
{
    [ExecuteInEditMode]
    [CustomEditor(typeof(SwipeHandler))]
    public class SwipeEditor : Editor
    {
        OneTapHandler _tapHandler;

        SerializedProperty _handScaleFactor;

        SerializedProperty _startTapcallback;
        SerializedProperty _endTapcallback;

        SerializedProperty _swipeLeftCallback;
        SerializedProperty _swipeRightCallback;
        SerializedProperty _swipeUpCallback;
        SerializedProperty _swipeDownCallback;
        SerializedProperty _swipeTriggerLength;

        void OnEnable()
        {
            _tapHandler = (OneTapHandler)target;

            _handScaleFactor = serializedObject.FindProperty("_handScaleFactor");

            _startTapcallback = serializedObject.FindProperty("_onTapped");
            _endTapcallback = serializedObject.FindProperty("_onUntapped");

            _swipeTriggerLength = serializedObject.FindProperty("_dragLengthTrigger");

            _swipeLeftCallback = serializedObject.FindProperty("_onSwipeLeft");
            _swipeRightCallback = serializedObject.FindProperty("_onSwipeRight");
            _swipeUpCallback = serializedObject.FindProperty("_onSwipeUp");
            _swipeDownCallback = serializedObject.FindProperty("_onSwipeDown");
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

            EditorGUILayout.Separator();

            serializedObject.Update();

            EditorGUILayout.PropertyField(_startTapcallback, new GUIContent("On Tap Started"));
            EditorGUILayout.PropertyField(_endTapcallback, new GUIContent("On Tapp Ended"));

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(_swipeTriggerLength, new GUIContent("Swipe Trigget Length"));

            EditorGUILayout.PropertyField(_swipeLeftCallback, new GUIContent("On Swipe Left"));
            EditorGUILayout.PropertyField(_swipeRightCallback, new GUIContent("On Swipe Right"));
            EditorGUILayout.PropertyField(_swipeUpCallback, new GUIContent("On Swipe Up"));
            EditorGUILayout.PropertyField(_swipeDownCallback, new GUIContent("On Swipe Down"));

            serializedObject.ApplyModifiedProperties();
        }

    }

}