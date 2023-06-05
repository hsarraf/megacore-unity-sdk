using UnityEditor;
using UnityEngine;

namespace MegaCore.DataModule
{
    [ExecuteInEditMode]
    [CustomEditor(typeof(MGLocalDataManager))]
    public class LocalDataManagerEditor : Editor
    {
        MGLocalDataManager _dataManager;

        GUISkin _guiSkin;

        void OnEnable()
        {
            _dataManager = (MGLocalDataManager)target;

            _guiSkin = Resources.Load<GUISkin>("DataManager/GUISkin");
        }

        public override void OnInspectorGUI()
        {
            GUI.skin = _guiSkin;

            EditorGUILayout.Separator();

            GUILayout.TextField(string.Format(MGDataConfig.RELATIVE_GAME_DATA_PATH, Application.persistentDataPath), new GUIStyle { normal = new GUIStyleState { textColor = Color.grey } });
            EditorGUILayout.Separator();
            //if (GUILayout.Button("Open Folder", GUILayout.Width(150f), GUILayout.Height(40f)))
            //{
            //    Application.OpenURL("");
            //}
        }

    }

}