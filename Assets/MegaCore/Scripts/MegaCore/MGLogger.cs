using System.Collections.Generic;
using UnityEngine;


namespace MegaCore.Logger
{
    public class MGLogger : MonoBehaviour
    {
        private static MGLogger __instance;
        public static MGLogger Instance { get { return __instance; } }
        void Awake()
        {
            if (__instance == null)
            {
                __instance = this;
                DontDestroyOnLoad(gameObject);

                _logList = new List<LogObject>();
                _infoList = new List<LogObject>();
                _warningList = new List<LogObject>();
                _errorList = new List<LogObject>();

                _COLOR_MAP[LogType.info] = new Color(0.941f, 0.941f, 0.941f);
                _COLOR_MAP[LogType.warn] = new Color(1f, 0.756f, 0.027f);
                _COLOR_MAP[LogType.err] = new Color(1f, 0.325f, 0.29f);

                UpdateModuleLogMap();

                RemoveFirstLogLoop();
            }
            else if (__instance != this)
            {
                Destroy(gameObject);
            }
        }

        public static Dictionary<MegaCore.Module, bool> _moduleLogDict;
        public bool _input = true;
        public bool _data = true;
        public bool _pool = true;
        public bool _mediation = true;
        public bool _scene = true;
        public bool _audio = true;
        public bool _popup = true;

        public float _logStayTime = 4f;

        public bool _logInfo;
        public bool _logWarning;
        public bool _logError;

        public static List<LogObject> _logList;

        public static List<LogObject> _infoList;
        public static string _infoLogString;

        public static List<LogObject> _warningList;
        public static string _warningLogString;

        public static List<LogObject> _errorList;
        public static string _errorLogString;


        public enum LogType
        {
            info, warn, err
        }
        static Dictionary<LogType, Color> _COLOR_MAP = new Dictionary<LogType, Color>();

        public class LogObject
        {
            public MegaCore.Module _module;
            public LogType _logType;
            public string _logMsg;
            public string ToJson()
            {
                return JsonUtility.ToJson(this);
            }
        }

        void OnGUI()
        {
            GUILayout.BeginVertical();
            for (int i = 0; i < _logList.Count; i++)
            {
                if (_logInfo && _logList[i]._logType == LogType.info)
                    GUILayout.TextArea(string.Format("{0}: {1}", _logList[i]._module, _logList[i]._logMsg), MGStyleEditor._textInfoStyle);
                else if (_logWarning && _logList[i]._logType == LogType.warn)
                    GUILayout.TextArea(string.Format("{0}: {1}", _logList[i]._module, _logList[i]._logMsg), MGStyleEditor._textWarnStyle);
                else if (_logError && _logList[i]._logType == LogType.err)
                    GUILayout.TextArea(string.Format("{0}: {1}", _logList[i]._module, _logList[i]._logMsg), MGStyleEditor._textErrStyle);
            }
            GUILayout.EndVertical();
        }

        private void UpdateModuleLogMap()
        {
            _moduleLogDict = new Dictionary<MegaCore.Module, bool>();

            _moduleLogDict[MegaCore.Module.input] = _input;
            _moduleLogDict[MegaCore.Module.data] = _data;
            _moduleLogDict[MegaCore.Module.pool] = _pool;
            _moduleLogDict[MegaCore.Module.mediation] = _mediation;
            _moduleLogDict[MegaCore.Module.scene] = _scene;
            _moduleLogDict[MegaCore.Module.audio] = _audio;
            _moduleLogDict[MegaCore.Module.popup] = _popup;
        }

        public static void LogInfo(string log, MegaCore.Module module)
        {
            if (_moduleLogDict[module])
            {
                LogObject logObj = new LogObject { _logMsg = log, _module = module, _logType = LogType.info };
                _logList.Add(logObj);
                _infoList.Add(logObj);
                _infoLogString += string.Format("{0}: {1}\n", module, log);
            }
        }

        public static void LogWarning(string log, MegaCore.Module module)
        {
            if (_moduleLogDict[module])
            {
                LogObject logObj = new LogObject { _logMsg = log, _module = module, _logType = LogType.warn };
                _logList.Add(logObj);
                _warningList.Add(logObj);
                _warningLogString += string.Format("{0}: {1}\n", module, log);
            }

        }

        public static void LogError(string log, MegaCore.Module module)
        {
            if (_moduleLogDict[module])
            {
                LogObject logObj = new LogObject { _logMsg = log, _module = module, _logType = LogType.err };
                _logList.Add(logObj);
                _errorList.Add(logObj);
                _errorLogString += string.Format("{0}: {1}\n", module, log);
            }
        }

        void RemoveFirstLogLoop()
        {
            if (_logList.Count > 0)
                _logList.RemoveAt(0);
            Invoke("RemoveFirstLogLoop", _logStayTime);
        }

    }

}
