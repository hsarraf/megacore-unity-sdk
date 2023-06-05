using UnityEngine;
using UnityEditor;


namespace MegaCore
{

    public static class MGStyleEditor
    {

        public static Font _megacoreLogoFont;
        public static Font _moduleLogoFont;
        public static Font _moduleLogFont;

        public static GUISkin _guiSkin;

        public static GUIStyle _toggleInfoStyle;
        public static GUIStyle _toggleWarnStyle;
        public static GUIStyle _toggleErrStyle;

        //public static GUIStyle _labelStyle;
        public static GUIStyle _textInfoStyle;
        public static GUIStyle _textWarnStyle;
        public static GUIStyle _textErrStyle;

        public static Color INFO_COLOR = new Color(0.941f, 0.941f, 0.941f);
        public static Color WARNING_COLOR = new Color(1f, 0.756f, 0.027f);
        public static Color ERROR_COLOR = new Color(1f, 0.325f, 0.29f);

        static MGStyleEditor()
        {
            _megacoreLogoFont = Resources.Load<Font>("MegaCore/Fonts/Championship");
            _moduleLogoFont = Resources.Load<Font>("MegaCore/Fonts/sportsjersey");
            _moduleLogFont = Resources.Load<Font>("MegaCore/Fonts/natlog");

            _guiSkin = Resources.Load<GUISkin>("MegaCore/GUISkin");

            _textInfoStyle = new GUIStyle(GUI.skin.textArea)
            {
                font = _moduleLogFont,
                fontSize = 20,
                wordWrap = true,
                normal = new GUIStyleState { textColor = INFO_COLOR }
            };
            _textWarnStyle = new GUIStyle(GUI.skin.textArea)
            {
                font = _moduleLogFont,
                fontSize = 20,
                wordWrap = true,
                normal = new GUIStyleState { textColor = WARNING_COLOR }
            };
            _textErrStyle = new GUIStyle(GUI.skin.textArea)
            {
                font = _moduleLogFont,
                fontSize = 20,
                wordWrap = true,
                normal = new GUIStyleState { textColor = ERROR_COLOR }
            };

            _toggleInfoStyle = new GUIStyle(GUI.skin.toggle)
            {
                contentOffset = new Vector2(5, 0),
                font = _moduleLogoFont,
                fontSize = 15,
                normal = new GUIStyleState { textColor = INFO_COLOR }
            };
            _toggleWarnStyle = new GUIStyle(GUI.skin.toggle)
            {
                contentOffset = new Vector2(5, 0),
                font = _moduleLogoFont,
                fontSize = 15,
                normal = new GUIStyleState { textColor = WARNING_COLOR }
            };
            _toggleErrStyle = new GUIStyle(GUI.skin.toggle)
            {
                contentOffset = new Vector2(5, 0),
                font = _moduleLogoFont,
                fontSize = 15,
                normal = new GUIStyleState { textColor = ERROR_COLOR }
            };
        }

        public static void DrawHeader(string moduleName, string header = "MEGACORE")
        {
            if (header != null)
            {
                EditorGUILayout.Separator();
                EditorGUILayout.Separator();
                EditorGUILayout.Separator();
                EditorGUILayout.Separator();
                EditorGUILayout.Separator();
                EditorGUILayout.Separator();

                GUILayout.BeginHorizontal();
                EditorGUILayout.Separator();
                GUILayout.Label(header, new GUIStyle { font = _megacoreLogoFont, fontSize = 50, normal = new GUIStyleState { textColor = new Color32(0xC7, 0xBE, 0xA2, 0xFF) } });
                GUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.Separator();
                EditorGUILayout.Separator();
            }

            if (moduleName != null)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.Separator();
                GUILayout.Label(moduleName, new GUIStyle { font = _moduleLogoFont, fontSize = 20, normal = new GUIStyleState { textColor = new Color32(0xC7, 0xBE, 0xA2, 0xFF) } });
                GUILayout.EndHorizontal();
            }
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            GUI.skin = _guiSkin;

        }

        public static void Separator()
        {
            GUI.skin = null;
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, GUILayout.ExpandWidth(true));
            GUI.skin = MGStyleEditor._guiSkin;
        }

    }


}