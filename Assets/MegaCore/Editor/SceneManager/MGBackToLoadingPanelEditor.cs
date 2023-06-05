using UnityEngine;
using UnityEditor;


namespace MegaCore.SceneManager
{

    [ExecuteInEditMode]
    [CustomEditor(typeof(MGBackToLoadingPanel))]
    public class MGBackToLoadingPanelEditor : Editor
    {
        Texture2D _backIcon;

        void OnEnable()
        {
            _backIcon = Resources.Load<Texture2D>("SceneManager/BackIcon");
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button(new GUIContent(_backIcon, "Back to loading panel"), GUILayout.Width(30), GUILayout.Height(30)))
                Selection.activeObject = FindObjectOfType<MGSceneBehaviour>().gameObject;
        }

    }

}