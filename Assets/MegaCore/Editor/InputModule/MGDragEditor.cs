using UnityEditor;
using UnityEngine;

namespace MegaCore.InputModule
{

    [ExecuteInEditMode]
    [CustomEditor(typeof(DragHandler))]
    public class DragEditor : Editor
    {
        DragHandler _dragHandler;

        SerializedProperty _handScaleFactor;

        SerializedProperty _raycastableFlag;
        SerializedProperty _raycastLayer;
        SerializedProperty _raycastCallback;

        SerializedProperty _startTapCallback;
        SerializedProperty _endTapCallback;
        SerializedProperty _holdingCallback;
        SerializedProperty _stationaryCallback;
        SerializedProperty _draggingCallback;
        SerializedProperty _dragLengthTriggeredCallback;
        SerializedProperty _dragTriggerLength;

        SerializedProperty _holdTimeTrigger;

        void OnEnable()
        {
            _dragHandler = (DragHandler)target;

            _handScaleFactor = serializedObject.FindProperty("_handScaleFactor");

            _raycastableFlag = serializedObject.FindProperty("_raycastable");
            _raycastLayer = serializedObject.FindProperty("_raycastLayer");
            _raycastCallback = serializedObject.FindProperty("_onRaycastTriggered");

            _startTapCallback = serializedObject.FindProperty("_onTapped");
            _endTapCallback = serializedObject.FindProperty("_onUntapped");
            _draggingCallback = serializedObject.FindProperty("_onDragging");
            _stationaryCallback = serializedObject.FindProperty("_onStationary");
            _holdingCallback = serializedObject.FindProperty("_onHold");
            _dragLengthTriggeredCallback = serializedObject.FindProperty("_onDragLengthTriggered");
            _dragTriggerLength = serializedObject.FindProperty("_dragLengthTrigger");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(Resources.Load<Texture2D>("InputHandler/HandOn"), GUILayout.Width(50), GUILayout.Height(50)))
                if (_dragHandler._handGizmo == null)
                {
                    _dragHandler._handGizmo = Instantiate(Resources.Load<MGInputHandGizmo>("InputHandler/HandOn"));
                    _dragHandler._handGizmo.transform.SetParent(_dragHandler.transform);
                    _dragHandler._handGizmo.gameObject.SetActive(false);
                }
            if (GUILayout.Button(Resources.Load<Texture2D>("InputHandler/HandOff"), GUILayout.Width(50), GUILayout.Height(50)))
                if (_dragHandler._handGizmo != null)
                {
                    DestroyImmediate(_dragHandler._handGizmo.gameObject);
                    _dragHandler._handGizmo = null;
                }

            EditorGUILayout.BeginVertical();
            EditorGUILayout.TextField(string.Format("status: {0}", _dragHandler._callbakType.ToString()));
            EditorGUILayout.TextField(string.Format("position: {0}", _dragHandler._touchPosition.ToString()));
            EditorGUILayout.TextField(string.Format("velocity: {0}", _dragHandler._touchSpeedVector.ToString()));
            EditorGUILayout.TextField(string.Format("speed: {0}", _dragHandler._touchSpeedVector.magnitude.ToString()));
            EditorGUILayout.TextField(string.Format("Drag Direction: {0}", _dragHandler._dragVector.ToString()));
            EditorGUILayout.TextField(string.Format("Drag Length: {0}", _dragHandler._dragLength.ToString()));
            EditorGUILayout.TextField(string.Format("Raycast Collider: {0}", _dragHandler._colliderName));
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

            serializedObject.Update();

            EditorGUILayout.PropertyField(_handScaleFactor, new GUIContent(""), GUILayout.Width(50f));

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(_startTapCallback, new GUIContent("On Tap Started"));
            EditorGUILayout.PropertyField(_endTapCallback, new GUIContent("On Tap Ended"));

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(_holdingCallback, new GUIContent("On Holding - both moving or stationary"));

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(_stationaryCallback, new GUIContent("On stationary - holding but not moving"));

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(_draggingCallback, new GUIContent("On Dragging - holding and moving"));

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(_dragTriggerLength, new GUIContent("Drag Trigget Length"));
            EditorGUILayout.PropertyField(_dragLengthTriggeredCallback, new GUIContent("On Drag Length Triggered"));

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(_raycastableFlag, new GUIContent("Raycast Active"));
            if (_dragHandler._raycastable)
            {
                EditorGUILayout.PropertyField(_raycastLayer, new GUIContent("Raycast Layer"));
                EditorGUILayout.PropertyField(_raycastCallback, new GUIContent("On Raycast"));
            }

            serializedObject.ApplyModifiedProperties();
        }

    }

}