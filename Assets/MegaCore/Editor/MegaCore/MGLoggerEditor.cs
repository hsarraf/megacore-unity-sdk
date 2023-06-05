using UnityEditor;
using UnityEngine;


namespace MegaCore.Logger
{

    [ExecuteInEditMode]
    [CustomEditor(typeof(MGLogger))]
    public class MGLoggerEditor : Editor
    {
        MGLogger _logger;

        Vector2 _infoScrollPos;
        Vector2 _warningScrollPos;
        Vector2 _errorScrollPos;

        Texture2D _clearIcon;

        void OnEnable()
        {
            _logger = (MGLogger)target;

            _clearIcon = Resources.Load<Texture2D>("MegaCore/ClearIcon");
        }

        public override void OnInspectorGUI()
        {
            MGStyleEditor.DrawHeader("DEBUGGER", null);

            GUILayout.BeginHorizontal();
            if (_logger._input)
                GUILayout.Label("INPUT", GUI.skin.box, GUILayout.MaxWidth(50));
            if (_logger._data)
                GUILayout.Label("DATA", GUI.skin.box, GUILayout.MaxWidth(50));
            if (_logger._scene)
                GUILayout.Label("SCENE", GUI.skin.box, GUILayout.MaxWidth(50));
            if (_logger._pool)
                GUILayout.Label("POOL", GUI.skin.box, GUILayout.MaxWidth(50));
            if (_logger._audio)
                GUILayout.Label("AUDIO", GUI.skin.box, GUILayout.MaxWidth(50));
            if (_logger._mediation)
                GUILayout.Label("MEDIATION", GUI.skin.box, GUILayout.MaxWidth(80));
            if (_logger._popup)
                GUILayout.Label("POPUP", GUI.skin.box, GUILayout.MaxWidth(50));
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            _logger._logInfo = GUILayout.Toggle(_logger._logInfo, "INFOS", MGStyleEditor._toggleInfoStyle);
            if (_logger._logInfo)
            {
                GUILayout.BeginHorizontal();
                _infoScrollPos = EditorGUILayout.BeginScrollView(_infoScrollPos, GUILayout.ExpandWidth(true), GUILayout.Height(150));
                MGLogger._infoLogString = EditorGUILayout.TextArea(MGLogger._infoLogString, MGStyleEditor._textInfoStyle, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
                EditorGUILayout.EndScrollView();
                if (GUILayout.Button(new GUIContent(_clearIcon, "Clear infos"), GUILayout.Width(30), GUILayout.Height(30)))
                    MGLogger._infoLogString = string.Empty;
                GUILayout.EndHorizontal();
            }

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            _logger._logWarning = GUILayout.Toggle(_logger._logWarning, "WARNINGS", MGStyleEditor._toggleWarnStyle);
            if (_logger._logWarning)
            {
                GUILayout.BeginHorizontal();
                _warningScrollPos = EditorGUILayout.BeginScrollView(_warningScrollPos, GUILayout.ExpandWidth(true), GUILayout.Height(150));
                MGLogger._warningLogString = EditorGUILayout.TextArea(MGLogger._warningLogString, MGStyleEditor._textWarnStyle, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
                EditorGUILayout.EndScrollView();
                if (GUILayout.Button(new GUIContent(_clearIcon, "Clear warnings"), GUILayout.Width(30), GUILayout.Height(30)))
                    MGLogger._warningLogString = string.Empty;
                GUILayout.EndHorizontal();
            }

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            _logger._logError = GUILayout.Toggle(_logger._logError, "ERRORS", MGStyleEditor._toggleErrStyle);
            if (_logger._logError)
            {
                GUILayout.BeginHorizontal();
                _errorScrollPos = EditorGUILayout.BeginScrollView(_errorScrollPos, GUILayout.ExpandWidth(true), GUILayout.Height(150));
                MGLogger._errorLogString = EditorGUILayout.TextArea(MGLogger._errorLogString, MGStyleEditor._textErrStyle, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
                EditorGUILayout.EndScrollView();
                if (GUILayout.Button(new GUIContent(_clearIcon, "Clear errors"), GUILayout.Width(30), GUILayout.Height(30)))
                    MGLogger._errorLogString = string.Empty;
                GUILayout.EndHorizontal();
            }

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            MGStyleEditor.Separator();

            _logger._logStayTime = EditorGUILayout.FloatField("Log Stay Time", _logger._logStayTime);

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            if (GUILayout.Button("Deactivate Debugger", GUILayout.Width(150), GUILayout.Height(40)))
            {
                _logger._logInfo = _logger._logWarning = _logger._logError = false;
                _logger._input = _logger._data = _logger._scene = _logger._pool = _logger._mediation = _logger._popup = _logger._audio = false;
            }

        }

        /// <summary>
        /// Menu to handle MGLogger
        /// </summary>
        /// 
        static MGLogger GetLogger()
        {
            MGLogger logger = FindObjectOfType<MGLogger>();
            if (logger == null)
            {
                MegaCoreEditor.CreateMegaCore();
                MGHelper.LogInfo("Logger module created");
                //logger = new GameObject("MG: Logger").AddComponent<MGLogger>();
            }
            else
            {
                MGHelper.LogWarnong("Logger module already exists");
            }
            return logger;
        }


        [MenuItem("MegaCore/Logger")]
        static void CreateLogger()
        {
            GetLogger();
        }

    }

}
