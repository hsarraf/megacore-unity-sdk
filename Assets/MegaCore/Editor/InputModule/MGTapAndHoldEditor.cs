using UnityEngine;
using UnityEditor;

namespace MegaCore.InputModule
{
    [ExecuteInEditMode]
    [CustomEditor(typeof(TapAndHoldHandler))]
    public class TapAndHandlerEditor : Editor
    {
        TapAndHoldHandler _tapAndHoldHandler;

        SerializedProperty _handScaleFactor;

        SerializedProperty _startTapcallback;
        SerializedProperty _endTapcallback;
        SerializedProperty _holdTimecallback;

        SerializedProperty _touchTimeer;

        void OnEnable()
        {
            _tapAndHoldHandler = (TapAndHoldHandler)target;

            _handScaleFactor = serializedObject.FindProperty("_handScaleFactor");

            _startTapcallback = serializedObject.FindProperty("_onTapped");
            _endTapcallback = serializedObject.FindProperty("_onUntapped");
            _holdTimecallback = serializedObject.FindProperty("_onHoldTimeTriggered");
            _touchTimeer = serializedObject.FindProperty("_touchTimer");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(Resources.Load<Texture2D>("InputHandler/HandOn"), GUILayout.Width(50), GUILayout.Height(50)))
                if (_tapAndHoldHandler._handGizmo == null)
                {
                    _tapAndHoldHandler._handGizmo = Instantiate(Resources.Load<MGInputHandGizmo>("InputHandler/HandOn"));
                    _tapAndHoldHandler._handGizmo.transform.SetParent(_tapAndHoldHandler.transform);
                    _tapAndHoldHandler._handGizmo.gameObject.SetActive(false);
                }
            if (GUILayout.Button(Resources.Load<Texture2D>("InputHandler/HandOff"), GUILayout.Width(50), GUILayout.Height(50)))
                if (_tapAndHoldHandler._handGizmo != null)
                {
                    DestroyImmediate(_tapAndHoldHandler._handGizmo.gameObject);
                    _tapAndHoldHandler._handGizmo = null;
                }

            EditorGUILayout.BeginVertical();
            EditorGUILayout.TextField(string.Format("status: {0}", _tapAndHoldHandler._callbakType.ToString()));
            EditorGUILayout.TextField(string.Format("position: {0}", _tapAndHoldHandler._touchPosition.ToString()));
            EditorGUILayout.TextField(string.Format("velocity: {0}", _tapAndHoldHandler._touchSpeedVector.ToString()));
            EditorGUILayout.TextField(string.Format("speed: {0}", _tapAndHoldHandler._touchSpeedVector.magnitude.ToString()));
            EditorGUILayout.TextField(string.Format("time: {0}", _tapAndHoldHandler._touchTimer.ToString()));
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

            serializedObject.Update();

            EditorGUILayout.PropertyField(_handScaleFactor, new GUIContent(""), GUILayout.Width(50f));

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(_startTapcallback, new GUIContent("On Tap Started"));
            EditorGUILayout.PropertyField(_endTapcallback, new GUIContent("On Tap Ended"));

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(_touchTimeer, new GUIContent("Trigger Time"));
            EditorGUILayout.PropertyField(_holdTimecallback, new GUIContent("On Hold Time Trigger"));


            //_holdTimeTrigger = serializedObject.FindProperty("_holdTriggerTime");

            serializedObject.ApplyModifiedProperties();
        }

    }

}