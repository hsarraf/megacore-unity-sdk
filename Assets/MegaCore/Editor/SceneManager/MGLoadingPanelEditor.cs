using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


namespace MegaCore.SceneManager
{

    [ExecuteInEditMode]
    [CustomEditor(typeof(MGLoadingPanel))]
    public class MGLoadingPanelEditor : Editor
    {
        static MGLoadingPanel _loadingPanel;

        private Texture2D _editorPanelBackground = null;
        private Texture2D _editorPanelBanner = null;
        private Texture2D _editorLoadingScroll = null;
        private Texture2D _editorLoadingBar = null;

        Texture2D _removeIcon;

        public enum ItemType
        {
            label, text
        }

        string _newBackgroundName = "Back panel name";
        string _newBannerName = "Banner name";
        string _newHintName = "Hint name";
        string _newLoadingTextName = "Loading text name";

        void OnEnable()
        {
            _loadingPanel = (MGLoadingPanel)target;

            _loadingPanel.GetComponent<RectTransform>().hideFlags |= HideFlags.HideInInspector;

            _removeIcon = Resources.Load<Texture2D>("SceneManager/RemoveIcon");

            _editorPanelBackground = Resources.Load<Texture2D>("SceneManager/EditorLoadingScheme/LoadingBackground");
            _editorPanelBanner = Resources.Load<Texture2D>("SceneManager/EditorLoadingScheme/LoadingBanner");
            _editorLoadingScroll = Resources.Load<Texture2D>("SceneManager/EditorLoadingScheme/LoadingScroll");
            _editorLoadingBar = Resources.Load<Texture2D>("SceneManager/EditorLoadingScheme/LoadingBar");

            if (!Application.isPlaying)
            {
                _loadingPanel._loadingBackgroundGrp = GameObject.Find("MG: LoadingBackgroundGrp").transform;
                _loadingPanel._loadingBannerGrp = GameObject.Find("MG: LoadingBannerGrp").transform;
                _loadingPanel._loadingScroll = GameObject.Find("MG: LoadingScroll");
                _loadingPanel._loadingHintGrp = GameObject.Find("MG: LoadingHintGrp").transform;
                _loadingPanel._loadingBar = GameObject.Find("MG: LoadingBar").GetComponent<MGLoadingBar>();
                _loadingPanel._loadingTextGrp = GameObject.Find("MG: LoadingTextGrp").transform;
            }

        }

        public override void OnInspectorGUI()
        {
            MGStyleEditor.DrawHeader("LOADING PANEL", null);


            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical();

            GUILayout.Label(new GUIContent(_editorPanelBackground, "Background panel"));
            //if (GUILayout.Button(new GUIContent(_editorPanelBackground, "Background panel"), GUI.skin.label))
            //    Selection.activeObject = _loadingPanel._loadingBackgroundGrp.GetChild(0).gameObject;

            GUILayout.BeginArea(new Rect(55f, 130f, 230, 150));
            //_loadingBanner = EditorGUILayout.Toggle(_loadingBanner);
            if (GUILayout.Button(new GUIContent(_editorPanelBanner, "Banner expressing the level being loaded"), GUI.skin.label))
                Selection.activeObject = _loadingPanel._loadingBannerGrp.GetChild(0).gameObject;
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(155f, 300f, 40f, 40f));
            //_loadingScroll = EditorGUILayout.Toggle(_loadingScroll);
            if (GUILayout.Button(new GUIContent(_editorLoadingScroll, "Loading scroll"), GUI.skin.label))
                Selection.activeObject = _loadingPanel._loadingScroll.gameObject;
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(60f, 370f, 225f, 130f));
            //GUILayout.Button(new GUIContent(_editorLoadingHint, ""), GUI.skin.label);
            //_loadingHint = EditorGUILayout.Toggle(_loadingHint);
            if (GUILayout.Button(new GUIContent("\nStrong fighters need someone that can \n find traps, heal or cast spells"),
                new GUIStyle { alignment = TextAnchor.MiddleCenter, normal = new GUIStyleState { textColor = Color.white } }))
                Selection.activeObject = _loadingPanel._loadingHintGrp.gameObject;
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(105f, 450f, 140f, 50f));
            //_loadingBar = EditorGUILayout.Toggle(_loadingBar);
            if (GUILayout.Button(new GUIContent(_editorLoadingBar, "Loading bar"), GUI.skin.label))
                Selection.activeObject = _loadingPanel._loadingBar.gameObject;
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(120f, 490f, 110f, 50f));
            //_loadingText = EditorGUILayout.Toggle(_loadingText);
            if (GUILayout.Button(new GUIContent("Loading...", "Loading text"), new GUIStyle { alignment = TextAnchor.MiddleCenter, normal = new GUIStyleState { textColor = Color.white } }))
                Selection.activeObject = _loadingPanel._loadingTextGrp.gameObject;
            GUILayout.EndArea();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            if (GUILayout.Button("Apply Enums", GUILayout.Height(30f)))
            {
                ApplyEnums();
            }

            GUILayout.EndVertical();

            GUILayout.BeginVertical();

            AddNewEditorItem(_loadingPanel._loadingBackgroundGrp, ref _newBackgroundName, 45, 80, "Add Panel", ItemType.label);
            AddNewEditorItem(_loadingPanel._loadingBannerGrp, ref _newBannerName, 100, 50, "Add Banner", ItemType.label);
            AddNewEditorItem(_loadingPanel._loadingHintGrp, ref _newHintName, 130, 50, "Add Hint", ItemType.text);
            AddNewEditorItem(_loadingPanel._loadingTextGrp, ref _newLoadingTextName, 130, 25, "Add Text", ItemType.text);

            GUILayout.EndVertical();


            GUILayout.EndHorizontal();

        }


        void AddNewEditorItem(Transform group, ref string itemName, int width, int height, string buttonName, ItemType itemType = ItemType.label)
        {
            //GUI.skin = null;
            foreach (Transform child in group)
            {
                GUILayout.BeginHorizontal();
                if (itemType == ItemType.label)
                {
                    if (GUILayout.Button(child.GetComponent<Image>().sprite.texture,
                        new GUIStyle(GUI.skin.box),
                        GUILayout.Width(width), GUILayout.Height(height)))
                        Selection.activeObject = child.gameObject;
                }
                else if (itemType == ItemType.text)
                {
                    if (GUILayout.Button(child.GetComponent<Text>().text,
                        new GUIStyle(GUI.skin.box) { normal = new GUIStyleState { textColor = Color.white }, wordWrap = true }, GUILayout.Width(width),
                        GUILayout.Height(height)))
                        Selection.activeObject = child.gameObject;
                }
                GUILayout.BeginVertical();
                GUILayout.Label(child.name);
                if (GUILayout.Button(_removeIcon, GUILayout.Width(20f), GUILayout.Height(20f)))
                {
                    if (group.childCount > 1)
                        DestroyImmediate(child.gameObject);
                    else
                        MGHelper.LogWarnong("There should be at least one item left");
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }
            EditorGUILayout.Separator();
            GUILayout.BeginHorizontal();
            itemName = GUILayout.TextField(itemName, GUILayout.Width(130));
            if (GUILayout.Button(buttonName, GUILayout.Width(90)))
                AddNewItem(group, ref itemName);
            GUILayout.EndHorizontal();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, GUILayout.Width(230));

            EditorGUILayout.Separator();
        }

        void AddNewItem(Transform group, ref string itemName)
        {
            if (string.IsNullOrEmpty(itemName))
            {
                MGHelper.LogError(string.Format("Please enter a name", itemName));
                return;
            }
            foreach (Transform child in group)
            {
                if (child.name == itemName)
                {
                    MGHelper.LogError(string.Format("The neme, {0} already exists", itemName));
                    return;
                }
            }

            GameObject splash = group.GetChild(group.childCount - 1).gameObject;
            GameObject newSplash = Instantiate(splash, splash.transform.position, Quaternion.identity);
            newSplash.transform.SetParent(group);
            newSplash.name = itemName;
        }

        public static string LoadingElementEnumScriptPath
        {
            get { return string.Format("{0}/MegaCore/Scripts/SceneManager/Enums/MGLoadingElementEnum.cs", Application.dataPath); }
        }

        static void ApplyEnums()
        {
            using (StreamWriter stream = new StreamWriter(LoadingElementEnumScriptPath))
            {
                stream.WriteLine("namespace MegaCore.SceneManager \n{\n");

                stream.WriteLine("\tpublic enum LoadingBackPanelEnum \n\t{");
                foreach (Transform item in _loadingPanel._loadingBackgroundGrp)
                    stream.WriteLine(string.Format("\t\t{0},", item.name));
                stream.WriteLine("\t}\n");

                stream.WriteLine("\tpublic enum LoadingBannerEnum \n\t{");
                foreach (Transform item in _loadingPanel._loadingBannerGrp)
                    stream.WriteLine(string.Format("\t\t{0},", item.name));
                stream.WriteLine("\t}\n");

                stream.WriteLine("\tpublic enum LoadingHintEnum \n\t{");
                foreach (Transform item in _loadingPanel._loadingHintGrp)
                    stream.WriteLine(string.Format("\t\t{0},", item.name));
                stream.WriteLine("\t}\n");

                stream.WriteLine("\tpublic enum LoadingTextEnum \n\t{");
                foreach (Transform item in _loadingPanel._loadingTextGrp)
                    stream.WriteLine(string.Format("\t\t{0},", item.name));
                stream.WriteLine("\t}\n");

                stream.WriteLine("}");
            }
            Application.OpenURL(LoadingElementEnumScriptPath);
        }

    }

}
