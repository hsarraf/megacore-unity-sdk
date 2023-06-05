using UnityEditor;
using UnityEngine;

namespace MegaCore.DataModule
{
    [ExecuteInEditMode]
    [CustomEditor(typeof(MGWebDataManager))]
    public class WebDataManagerEditor : Editor
    {
        MGWebDataManager _dataManager;

        GUISkin _guiSkin;

        void OnEnable()
        {
            _dataManager = (MGWebDataManager)target;

            _guiSkin = Resources.Load<GUISkin>("DataManager/GUISkin");
        }

        public override void OnInspectorGUI()
        {
            GUI.skin = _guiSkin;

            EditorGUILayout.Separator();

        }

    }

}