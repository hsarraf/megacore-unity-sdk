using UnityEditor;
using UnityEngine;

using MegaCore.MediationService;
using MegaCore.DataModule;
using MegaCore.AudioManager;
using MegaCore.PoolingModule;
using MegaCore.Popup;
using MegaCore.SceneManager;
using MegaCore.InputModule;
using MegaCore.Logger;


namespace MegaCore
{

    [ExecuteInEditMode]
    [CustomEditor(typeof(MegaCore))]
    public class MegaCoreEditor : Editor
    {

        MegaCore _megaCore;

        Texture2D _inputIcon;
        Texture2D _audioIcon;
        Texture2D _dataIcon;
        Texture2D _mediationIcon;
        Texture2D _poolIcon;
        Texture2D _sceneIcon;
        Texture2D _popupIcon;

        Texture2D _remIcon;
        Texture2D _logIcon;

        void OnEnable()
        {
            _megaCore = (MegaCore)target;

            _megaCore.GetComponent<Transform>().hideFlags |= HideFlags.HideInInspector;

            _inputIcon = Resources.Load<Texture2D>("MegaCore/ModuelIcons/InputIcon");
            _dataIcon = Resources.Load<Texture2D>("MegaCore/ModuelIcons/DataIcon");
            _sceneIcon = Resources.Load<Texture2D>("MegaCore/ModuelIcons/SceneIcon");
            _poolIcon = Resources.Load<Texture2D>("MegaCore/ModuelIcons/PoolIcon");
            _audioIcon = Resources.Load<Texture2D>("MegaCore/ModuelIcons/AudioIcon");
            _mediationIcon = Resources.Load<Texture2D>("MegaCore/ModuelIcons/MediationIcon");
            _popupIcon = Resources.Load<Texture2D>("MegaCore/ModuelIcons/PopupIcon");

            _remIcon = Resources.Load<Texture2D>("MegaCore/RemIcon");
            _logIcon = Resources.Load<Texture2D>("MegaCore/DebugIcon");
        }

        public override void OnInspectorGUI()
        {
            MGStyleEditor.DrawHeader("MEGARAMA");

            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical();
            if (GUILayout.Button(new GUIContent(_inputIcon, "Input Handler"),
                FindObjectOfType<MGTouchBehaviour>() == null ? new GUIStyle(MGStyleEditor._guiSkin.button) : new GUIStyle(),
                GUILayout.MaxWidth(50), GUILayout.MaxHeight(50)))
            {
                MGTouchBehaviourEditor.GetTouchBehaviour();
            }
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(_remIcon, GUILayout.Width(20), GUILayout.Height(20)))
                Destroy(FindObjectOfType<MGTouchBehaviour>().gameObject);
            _megaCore._logger._input = GUILayout.Toggle(_megaCore._logger._input, _logIcon, GUILayout.Width(30), GUILayout.Height(30));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            EditorGUILayout.Separator();

            GUILayout.BeginVertical();
            if (GUILayout.Button(new GUIContent(_dataIcon, "Data Manager"),
                FindObjectOfType<MGDataBehaviour>() == null ? new GUIStyle(MGStyleEditor._guiSkin.button) : new GUIStyle(),
                GUILayout.MaxWidth(50), GUILayout.MaxHeight(50)))
                MGDataBehaviourEditor.GetDataBehaviour();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(_remIcon, GUILayout.Width(20), GUILayout.Height(20)))
                Debug.Log("");
            _megaCore._logger._data = GUILayout.Toggle(_megaCore._logger._data, _logIcon, GUILayout.Width(30), GUILayout.Height(30));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            EditorGUILayout.Separator();

            GUILayout.BeginVertical();
            if (GUILayout.Button(new GUIContent(_sceneIcon, "Scene Manager"),
                FindObjectOfType<MGSceneBehaviour>() == null ? new GUIStyle(MGStyleEditor._guiSkin.button) : new GUIStyle(),
                GUILayout.MaxWidth(50), GUILayout.MaxHeight(50)))
                MGSceneBehaviourEditor.GetSceneBehaviour();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(_remIcon, GUILayout.Width(20), GUILayout.Height(20)))
                Debug.Log("");
            _megaCore._logger._scene = GUILayout.Toggle(_megaCore._logger._scene, _logIcon, GUILayout.Width(30), GUILayout.Height(30));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            EditorGUILayout.Separator();

            GUILayout.BeginVertical();
            if (GUILayout.Button(new GUIContent(_poolIcon, "Pooling System"),
                FindObjectOfType<MGPoolBehaviour>() == null ? new GUIStyle(MGStyleEditor._guiSkin.button) : new GUIStyle(),
                GUILayout.MaxWidth(50), GUILayout.MaxHeight(50)))
                MGPoolBehaviourEditor.GetPoolBehaviour();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(_remIcon, GUILayout.Width(20), GUILayout.Height(20)))
                Debug.Log("");
            _megaCore._logger._pool = GUILayout.Toggle(_megaCore._logger._pool, _logIcon, GUILayout.Width(30), GUILayout.Height(30));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            EditorGUILayout.Separator();

            GUILayout.BeginVertical();
            if (GUILayout.Button(new GUIContent(_audioIcon, "Audio Manager"),
                FindObjectOfType<MGAudioBehaviour>() == null ? new GUIStyle(MGStyleEditor._guiSkin.button) : new GUIStyle(),
                GUILayout.MaxWidth(50), GUILayout.MaxHeight(50)))
                MGAudioBehaviourEditor.GetAudioBehaviour();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(_remIcon, GUILayout.Width(20), GUILayout.Height(20)))
                Debug.Log("");
            _megaCore._logger._audio = GUILayout.Toggle(_megaCore._logger._audio, _logIcon, GUILayout.Width(30), GUILayout.Height(30));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            EditorGUILayout.Separator();

            GUILayout.BeginVertical();
            if (GUILayout.Button(new GUIContent(_mediationIcon, "Mediation"),
                FindObjectOfType<MGMediationBehaviour>() == null ? new GUIStyle(MGStyleEditor._guiSkin.button) : new GUIStyle(),
                GUILayout.MaxWidth(50), GUILayout.MaxHeight(50)))
                MGMediationBehaviourEditor.GetMediationBehaviour();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(_remIcon, GUILayout.Width(20), GUILayout.Height(20)))
                Debug.Log("");
            _megaCore._logger._mediation = GUILayout.Toggle(_megaCore._logger._mediation, _logIcon, GUILayout.Width(30), GUILayout.Height(30));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            EditorGUILayout.Separator();

            GUILayout.BeginVertical();
            if (GUILayout.Button(new GUIContent(_popupIcon, "PopUp Handler"),
                FindObjectOfType<MGPopupBehaviour>() == null ? new GUIStyle(MGStyleEditor._guiSkin.button) : new GUIStyle(),
                GUILayout.MaxWidth(50), GUILayout.MaxHeight(50)))
                MGPopupBehaviourEditor.GetMessaginghBehaviour();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(_remIcon, GUILayout.Width(20), GUILayout.Height(20)))
                Debug.Log("");
            _megaCore._logger._popup = GUILayout.Toggle(_megaCore._logger._popup, _logIcon, GUILayout.Width(30), GUILayout.Height(30));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();


            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
        }

        /// <summary>
        /// MegaCore Menu bar
        /// </summary>
        /// 
        static MegaCore GetMegaCore()
        {
            MegaCore megaCore = FindObjectOfType<MegaCore>();
            if (megaCore == null)
            {
                MGHelper.LogInfo("MegaCore created");
                megaCore = new GameObject("MEGACORE").AddComponent<MegaCore>();
            }
            Selection.activeGameObject = megaCore.gameObject;
            return megaCore;
        }


        [MenuItem("MegaCore/MegaCore")]
        public static void CreateMegaCore()
        {
            MegaCore megaCore = GetMegaCore();
            MGLogger logger = megaCore.gameObject.GetComponent<MGLogger>();
            if (logger == null)
            {
                logger = megaCore.gameObject.AddComponent<MGLogger>();
                MGHelper.LogInfo("Logger component created");
            }
            else
            {
                MGHelper.LogWarnong("Logger component already exists");
            }
            megaCore._logger = logger;
        }

    }

}