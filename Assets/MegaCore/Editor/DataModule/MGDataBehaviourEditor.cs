using Newtonsoft.Json;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace MegaCore.DataModule
{
    [ExecuteInEditMode]
    [CustomEditor(typeof(MGDataBehaviour))]
    public class MGDataBehaviourEditor : Editor
    {
        static MGDataBehaviour _dataBehaviour;
        private Texture2D _logo = null;

        string _webDataJsonStr = null;

        static string GameDataDir
        {
            get { return string.Format(MGDataConfig.RELATIVE_GAME_DATA_DIR, Application.persistentDataPath); }
        }
        static string GameDataPath
        {
            get { return string.Format(MGDataConfig.RELATIVE_GAME_DATA_PATH, Application.persistentDataPath); }
        }

        GUISkin _guiSkin;

        void OnEnable()
        {
            _dataBehaviour = (MGDataBehaviour)target;

            if (!Directory.Exists(GameDataDir))
                Directory.CreateDirectory(GameDataDir);

            _dataBehaviour.GetComponent<Transform>().hideFlags |= HideFlags.HideInInspector;

            _logo = Resources.Load<Texture2D>("DataManager/Banner");

            _guiSkin = Resources.Load<GUISkin>("DataManager/GUISkin");

            //_dataBehaviour._localDataManager.Load();
        }

        Vector2 _localJsonScrollPos;
        Vector2 _webJsonScrollPos;
        public override void OnInspectorGUI()
        {

            GUI.skin = _guiSkin;

            GUILayout.Label(_logo, new GUIStyle { alignment = TextAnchor.LowerCenter });

            EditorGUILayout.Separator();

            GUILayout.BeginHorizontal();


                GUILayout.BeginVertical();

                    if (GUILayout.Button("Create Local Module", GUILayout.ExpandWidth(true), GUILayout.Height(40f)))
                        CreateLocalDataModule();
                    GUILayout.BeginHorizontal();
                        if (GUILayout.Button("Generate Data", GUILayout.ExpandWidth(true), GUILayout.Height(30f)))
                            GenerateLocalData();
                        else if (GUILayout.Button("Delete", GUILayout.ExpandWidth(true), GUILayout.Height(30f)))
                            DeleteLocalDataModule();
                    GUILayout.EndHorizontal();

                    _localJsonScrollPos = EditorGUILayout.BeginScrollView(_localJsonScrollPos, GUILayout.Height(250));
                    EditorGUILayout.TextArea(MGDataBehaviour.__GAME_DATA_JSON, new GUIStyle { normal = new GUIStyleState { textColor = Color.grey } }, GUILayout.ExpandHeight(true), GUILayout.Width(220));
                    EditorGUILayout.EndScrollView();

                    if (GUILayout.Button("Open Data Json", GUILayout.ExpandWidth(true)))
                        OpenDataJson();
                    else if (GUILayout.Button("Open Data Calss"))
                        OpenDataClass();

                GUILayout.EndVertical();


                EditorGUILayout.Separator();


                GUILayout.BeginVertical();

                    if (GUILayout.Button("Create Web Module", GUILayout.ExpandWidth(true), GUILayout.Height(40f)))
                        CreateWebDataModule();
                    GUILayout.BeginHorizontal();
                        if (GUILayout.Button("Generate Data", GUILayout.ExpandWidth(true), GUILayout.Height(30f)))
                            GenerateWebData();
                        else if (GUILayout.Button("Delete", GUILayout.ExpandWidth(true), GUILayout.Height(30f)))
                            DeleteWebDataModule();
                    GUILayout.EndHorizontal();

                    _webJsonScrollPos = EditorGUILayout.BeginScrollView(_webJsonScrollPos, GUILayout.Height(250));
                    EditorGUILayout.TextArea(_webDataJsonStr, new GUIStyle { normal = new GUIStyleState { textColor = Color.grey } }, GUILayout.ExpandHeight(true), GUILayout.Width(220f));
                    EditorGUILayout.EndScrollView();

                GUILayout.EndVertical();


            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
        }

        private void OpenDataJson()
        {
            Application.OpenURL(string.Format(MGDataConfig.RELATIVE_GAME_DATA_PATH, Application.persistentDataPath));
        }

        public static MGDataBehaviour GetDataBehaviour()
        {
            MGDataBehaviour dataBehaviour = FindObjectOfType<MGDataBehaviour>();
            if (dataBehaviour == null)
            {
                MGHelper.LogInfo("Data manager created");
                dataBehaviour = new GameObject("MG: DataManager").AddComponent<MGDataBehaviour>();
            }
            Selection.activeGameObject = dataBehaviour.gameObject;
            return dataBehaviour;
        }

        private void DeleteLocalDataModule()
        {
            // remove local data component
            //
            MGLocalDataManager localDataManager = _dataBehaviour.GetComponent<MGLocalDataManager>();
            if (localDataManager != null)
                DestroyImmediate(localDataManager);

            MGDataBehaviour.__GAME_DATA_JSON = null;

            // delete game data file
            //
            File.Delete(GameDataPath);
        }

        private void DeleteWebDataModule()
        {
            // remove web data component
            //
            MGWebDataManager webDataManager = _dataBehaviour.GetComponent<MGWebDataManager>();
            if (webDataManager != null)
                DestroyImmediate(webDataManager);
        }

        static void ShowLocalDataJson()
        {
            try
            {
                using (StreamReader stream = new StreamReader(GameDataPath))
                    MGDataBehaviour.__GAME_DATA_JSON = stream.ReadToEnd();
            }
            catch (JsonReaderException err)
            {
                Debug.LogError(string.Format("MG: {0}", err.Message));
            }
        }


        string WriteWebDataJson(DataClass data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }


        /// <summary>
        /// Local Data Module
        /// </summary>
        [MenuItem("MegaCore/Data Manager/Local/Create Module")]
        static void CreateLocalDataModule()
        {
            MGDataBehaviour dataBehaviour = GetDataBehaviour();
            MGLocalDataManager localDataManager = dataBehaviour.GetComponent<MGLocalDataManager>();
            if (localDataManager == null)
            {
                localDataManager = dataBehaviour.gameObject.AddComponent<MGLocalDataManager>();
                MGHelper.LogInfo("Local data manager created");
            }
            else
            {
                MGHelper.LogWarnong("Local data manager already exists");
            }
            dataBehaviour._localDataManager = localDataManager;
        }

        [MenuItem("MegaCore/Data Manager/Local/Generate Data")]
        static void GenerateLocalData()
        {
            DataClass dataObject = null;
            if (_dataBehaviour.GetComponent<MGLocalDataManager>() != null)
            {
                if (!File.Exists(string.Format(GameDataPath)))
                    dataObject = new DataClass();
                else
                {
                    MGHelper.LogWarnong("Data already generated!!");
                    return;
                }
            }
            else
            {
                MGHelper.LogWarnong("No local data module assigned!!");
                return;
            }
            MGDataBehaviour.__GAME_DATA_JSON = dataObject.Dump(true);
            if (MGDataBehaviour.__GAME_DATA_JSON != null)
            {
                using (FileStream file = File.Open(GameDataPath, FileMode.OpenOrCreate))
                    using (StreamWriter stream = new StreamWriter(file))
                        stream.Write(MGDataBehaviour.__GAME_DATA_JSON);
            }
        }


        /// <summary>
        /// Web Data Module
        /// </summary>
        [MenuItem("MegaCore/Data Manager/Web/Create Module")]
        static void CreateWebDataModule()
        {
            MGDataBehaviour dataBehaviour = GetDataBehaviour();
            MGWebDataManager webDataManager = dataBehaviour.gameObject.GetComponent<MGWebDataManager>();
            if (webDataManager == null)
            {
                webDataManager = dataBehaviour.gameObject.AddComponent<MGWebDataManager>();
                MGHelper.LogInfo("Local data manager created");
            }
            else
            {
                MGHelper.LogWarnong("Web data manager already exists");
            }
            dataBehaviour._webDataManager = webDataManager;
        }

        [MenuItem("MegaCore/Data Manager/Web/Generate Data")]
        static void GenerateWebData()
        {
        }


        /// <summary>
        /// Open Data Class
        /// </summary>
        [MenuItem("MegaCore/Data Manager/Open Data Class")]
        static void OpenDataClass()
        {
            Application.OpenURL(string.Format("file://{0}/MegaCore/Scripts/DataModule/MGDataClass.cs", (Application.dataPath)));
        }

    }

}