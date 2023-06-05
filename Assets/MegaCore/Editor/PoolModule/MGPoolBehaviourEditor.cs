using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace MegaCore.PoolingModule
{
    [ExecuteInEditMode]
    [CustomEditor(typeof(MGPoolBehaviour))]
    public class MGPoolBehaviourEditor : Editor
    {
        MGPoolBehaviour _poolBehaviour;
        private Texture2D _logo = null;

        SerializedProperty _elementPrefabList;

        //GUISkin _logGuiSkin;

        void OnEnable()
        {
            _poolBehaviour = (MGPoolBehaviour)target;

            _poolBehaviour.GetComponent<Transform>().hideFlags |= HideFlags.HideInInspector;

            _logo = Resources.Load<Texture2D>("PoolModule/Banner");

            _elementPrefabList = serializedObject.FindProperty("_elementPrefabList");

            //_logGuiSkin = Resources.Load<GUISkin>("PoolModule/GUISkin");
        }

        public override void OnInspectorGUI()
        {
            //GUI.skin = _logGuiSkin;

            GUILayout.Label(_logo, new GUIStyle { alignment = TextAnchor.LowerCenter });

            EditorGUILayout.Separator();

            serializedObject.Update();
            EditorGUILayout.PropertyField(_elementPrefabList, new GUIContent("Element Prefab List"));
            serializedObject.ApplyModifiedProperties();

            if (_poolBehaviour._poolMap != null)
            {
                GUILayout.BeginHorizontal();
                    GUILayout.TextField("Element", new GUIStyle { normal = new GUIStyleState { textColor = Color.grey }, fontStyle = FontStyle.Bold, alignment = TextAnchor.LowerLeft });
                    GUILayout.TextField("In Pool", new GUIStyle { normal = new GUIStyleState { textColor = Color.grey }, fontStyle = FontStyle.Bold, alignment = TextAnchor.LowerLeft });
                GUILayout.EndHorizontal();

                EditorGUILayout.Separator();

                GUILayout.BeginHorizontal();
                    GUILayout.TextField("----------", new GUIStyle { normal = new GUIStyleState { textColor = Color.grey }, fontStyle = FontStyle.Bold, alignment = TextAnchor.LowerLeft });
                    GUILayout.TextField("----------", new GUIStyle { normal = new GUIStyleState { textColor = Color.grey }, fontStyle = FontStyle.Bold, alignment = TextAnchor.LowerLeft });
                GUILayout.EndHorizontal();

                foreach (KeyValuePair<MGElement.Type, List<MGElement>> poolElement in _poolBehaviour._poolMap)
                {
                    EditorGUILayout.Separator();
                    GUILayout.BeginHorizontal();
                    GUILayout.TextField(poolElement.Key.ToString(), new GUIStyle { normal = new GUIStyleState { textColor = Color.grey }, alignment = TextAnchor.LowerLeft });
                    GUILayout.TextField(poolElement.Value.Count.ToString(), new GUIStyle { normal = new GUIStyleState { textColor = Color.grey }, alignment = TextAnchor.LowerLeft });
                    GUILayout.EndHorizontal();
                }
                Repaint();
            }
        }

        public static MGPoolBehaviour GetPoolBehaviour()
        {
            MGPoolBehaviour poolBehaviour = FindObjectOfType<MGPoolBehaviour>();
            if (poolBehaviour == null)
            {
                poolBehaviour = new GameObject("MG: PoolManager").AddComponent<MGPoolBehaviour>();
                MGHelper.LogInfo("Pool manager created");
            }
            Selection.activeGameObject = poolBehaviour.gameObject;
            return poolBehaviour;
        }


        [MenuItem("MegaCore/Pooling/Create Pool")]
        static void LocalDataModule()
        {
            GetPoolBehaviour();
        }

    }
}