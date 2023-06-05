using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MegaCore.Popup
{
    [ExecuteInEditMode]
    [CustomEditor(typeof(MGPopupBehaviour))]
    public class MGPopupBehaviourEditor : Editor
    {
        static MGPopupBehaviour _messaging;
        private Texture2D _logo = null;

        CanvasScaler _canvasScaler;

        GUISkin _logGuiSkin;


        void OnEnable()
        {
            _messaging = (MGPopupBehaviour)target;
            _canvasScaler = _messaging.GetComponent<CanvasScaler>();

            _logo = Resources.Load<Texture2D>("MegaCoreLogo_Messaging");

            _messaging.GetComponent<RectTransform>().hideFlags |= HideFlags.HideInInspector;
            _messaging.GetComponent<Canvas>().hideFlags |= HideFlags.HideInInspector;
            _messaging.GetComponent<CanvasScaler>().hideFlags |= HideFlags.HideInInspector;
            _messaging.GetComponent<GraphicRaycaster>().hideFlags |= HideFlags.HideInInspector;

            _logGuiSkin = Resources.Load<GUISkin>("MGGUISkin");
        }

        public override void OnInspectorGUI()
        {
            GUI.skin = _logGuiSkin;

            GUILayout.Label(_logo, new GUIStyle { alignment = TextAnchor.LowerCenter });

            EditorGUILayout.Separator();

            GUILayout.BeginHorizontal();


            GUILayout.BeginVertical();
            if (GUILayout.Button("Message Box", GUILayout.Width(100), GUILayout.Height(50)))
                MessageBox();
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Remove", GUILayout.Width(100), GUILayout.Height(25)))
            {
                if (_messaging.GetComponent<MGMessageBox>() != null)
                {
                    DestroyImmediate(_messaging.GetComponent<MGMessageBox>()._panel.gameObject);
                    DestroyImmediate(_messaging.GetComponent<MGMessageBox>());
                    _messaging._messageBox = null;
                }
            }
            GUILayout.EndVertical();

            GUI.backgroundColor = Color.white;

            EditorGUILayout.Separator();

            GUILayout.BeginVertical();
            if (GUILayout.Button("Dialog Box", GUILayout.Width(100), GUILayout.Height(50)))
                DialogBox();
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Remove", GUILayout.Width(100), GUILayout.Height(25)))
            {
            }
            GUILayout.EndVertical();

            GUI.backgroundColor = Color.white;

            EditorGUILayout.Separator();

            GUILayout.BeginVertical();
            if (GUILayout.Button("Prompt Box", GUILayout.Width(100), GUILayout.Height(50)))
                PromptBox();
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Remove", GUILayout.Width(100), GUILayout.Height(25)))
            {
            }
            GUILayout.EndVertical();

            EditorGUILayout.Separator();

            GUILayout.BeginVertical();
            if (GUILayout.Button("GDRP Consent", GUILayout.Width(100), GUILayout.Height(50)))
                PromptBox();
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Remove", GUILayout.Width(100), GUILayout.Height(25)))
            {
                MessageBox();
            }
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUI.backgroundColor = Color.white;

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            _canvasScaler.scaleFactor = EditorGUILayout.Slider("Scale Factor", _canvasScaler.scaleFactor, 1f, 12f);
            EditorUtility.SetDirty(_canvasScaler);

        }


        static void CreateEventSystem()
        {
            if (FindObjectOfType<EventSystem>() == null)
            {
                GameObject eventSystem = new GameObject("EventSystem");
                eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();
                MGHelper.LogInfo("Event System created");
            }
        }

        public static MGPopupBehaviour GetMessaginghBehaviour()
        {
            MGPopupBehaviour messagingBehaviour = FindObjectOfType<MGPopupBehaviour>();
            if (messagingBehaviour == null)
            {
                MGHelper.LogInfo("Messaging module created");
                messagingBehaviour = Instantiate(Resources.Load<MGPopupBehaviour>("Messaging/MG_Messaging"));
                messagingBehaviour.name = "MG: Messaging";
            }
            CreateEventSystem();
            Selection.activeGameObject = messagingBehaviour.gameObject;
            return messagingBehaviour;
        }

        [MenuItem("MegaCore/Messaging/Message Box")]
        static void MessageBox()
        {
            MGPopupBehaviour messagingBehaviour = GetMessaginghBehaviour();
            MGMessageBox messageBox = messagingBehaviour.gameObject.GetComponent<MGMessageBox>();
            if (messageBox == null)
            {
                messageBox = messagingBehaviour.gameObject.AddComponent<MGMessageBox>();
                GameObject messageBoxAsset = Instantiate(Resources.Load<GameObject>("Messaging/MG_MessageBox"));
                messageBoxAsset.name = "MG: MessageBox";
                messageBoxAsset.transform.SetParent(messagingBehaviour.transform);
                messageBoxAsset.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                //_messaging.GetComponent<CanvasScaler>().scaleFactor = Screen.width * 0.05f;
                messageBox._panel = messageBoxAsset;
                MGHelper.LogInfo("Message box created");
            }
            else
            {
                MGHelper.LogWarnong("Message box already exists");
            }
            messagingBehaviour._messageBox = messageBox;
        }


        [MenuItem("MegaCore/Messaging/Dialog Box")]
        static void DialogBox()
        {
            MGPopupBehaviour messagingBehaviour = GetMessaginghBehaviour();
            MGDialogBox dialogBox = messagingBehaviour.gameObject.GetComponent<MGDialogBox>();
            if (dialogBox == null)
            {
                messagingBehaviour.gameObject.AddComponent<MGDialogBox>();
                MGHelper.LogInfo("Dialog box created");
            }
            else
            {
                MGHelper.LogWarnong("Dialog box already exists");
            }
            messagingBehaviour._dialogBox = dialogBox;
        }

        [MenuItem("MegaCore/Messaging/Prompt Box")]
        static void PromptBox()
        {
            MGPopupBehaviour messagingBehaviour = GetMessaginghBehaviour();
            MGPromptBox promptBox = messagingBehaviour.gameObject.GetComponent<MGPromptBox>();
            if (promptBox == null)
            {
                messagingBehaviour.gameObject.AddComponent<MGPromptBox>();
                MGHelper.LogInfo("Prompt box created");
            }
            else
            {
                MGHelper.LogWarnong("Prompt box already exists");
            }
            messagingBehaviour._promptBox = promptBox;
        }


    }

}
