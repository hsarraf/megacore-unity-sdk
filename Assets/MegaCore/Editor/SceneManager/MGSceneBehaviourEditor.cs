using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace MegaCore.SceneManager
{
    [ExecuteInEditMode]
    [CustomEditor(typeof(MGSceneBehaviour))]
    public class MGSceneBehaviourEditor : Editor
    {

        static MGSceneBehaviour _sceneBehaviour;

        SerializedProperty _onWonCallback;
        SerializedProperty _onLostCallback;
        SerializedProperty _onEvenCallback;

        SerializedProperty _onSceneLoadedCallback;


        void OnEnable()
        {
            _sceneBehaviour = (MGSceneBehaviour)target;

            _sceneBehaviour.GetComponent<RectTransform>().hideFlags |= HideFlags.HideInInspector;
            _sceneBehaviour.GetComponent<Canvas>().hideFlags |= HideFlags.HideInInspector;
            _sceneBehaviour.GetComponent<CanvasScaler>().hideFlags |= HideFlags.HideInInspector;
            _sceneBehaviour.GetComponent<GraphicRaycaster>().hideFlags |= HideFlags.HideInInspector;

            _onWonCallback = serializedObject.FindProperty("_onWon");
            _onLostCallback = serializedObject.FindProperty("_onLost");
            _onEvenCallback = serializedObject.FindProperty("_onEven");
            _onSceneLoadedCallback = serializedObject.FindProperty("_onSceneLoaded");
        }

        public override void OnInspectorGUI()
        {
            MGStyleEditor.DrawHeader("SCENE MANAGER");

            GUILayout.BeginHorizontal();
            serializedObject.Update();
            EditorGUILayout.PropertyField(_onWonCallback, new GUIContent("OnWon"));
            EditorGUILayout.PropertyField(_onLostCallback, new GUIContent("OnLost"));
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(_onEvenCallback, new GUIContent("OnEven"));

            EditorGUILayout.Separator();
            MGStyleEditor.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(_onSceneLoadedCallback, new GUIContent("OnSceneLoaded"));

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Setup", GUILayout.Width(130f), GUILayout.Height(40f)))
                Setup();
            GUILayout.FlexibleSpace();
            EditorGUILayout.Separator();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Goto Loading Panel", GUILayout.Width(130f), GUILayout.Height(40f)))
                Selection.activeObject = _sceneBehaviour.GetComponentInChildren<MGLoadingPanel>().gameObject;
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
        }

        private void Setup()
        {
        }

        public static MGSceneBehaviour GetSceneBehaviour()
        {
            MGSceneBehaviour sceneBehaviour = FindObjectOfType<MGSceneBehaviour>();
            if (sceneBehaviour == null)
            {
                MGHelper.LogInfo("Scene manager created");
                sceneBehaviour = Instantiate(Resources.Load<MGSceneBehaviour>("SceneManager/MGSceneBehaviour"));
                sceneBehaviour.gameObject.name = "MG: SceneManager";
            }
            Selection.activeGameObject = sceneBehaviour.gameObject;
            return sceneBehaviour;
        }


        [MenuItem("MegaCore/Scene Manager/Create Scene Manager")]
        static void CreatSceneManager()
        {
            GetSceneBehaviour();
        }

    }

}