using UnityEngine;
using UnityEditor;

namespace MegaCore.InputModule
{
    [ExecuteInEditMode]
    [CustomEditor(typeof(MGTouchBehaviour))]
    public class MGTouchBehaviourEditor : Editor
    {
        MGTouchBehaviour _touchBehaviour;
        private Texture2D _logo = null;

        void OnEnable()
        {
            _touchBehaviour = (MGTouchBehaviour)target;

            _touchBehaviour.GetComponent<Transform>().hideFlags |= HideFlags.HideInInspector;

            _logo = Resources.Load<Texture2D>("InputHandler/Splash");
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginVertical();

            GUILayout.Label(_logo, new GUIStyle { alignment = TextAnchor.LowerCenter } );

            EditorGUILayout.Separator();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(Resources.Load<Texture2D>("InputHandler/OneTapIcon"), new GUIStyle { }, GUILayout.Width(50), GUILayout.Height(50)))
            {
                OneTapModule();
            }
            EditorGUILayout.Separator();
            if (GUILayout.Button(Resources.Load<Texture2D>("InputHandler/TapAndHoldIcon"), new GUIStyle { }, GUILayout.Width(50), GUILayout.Height(50)))
            {
                TapAndHoldModule();
            }
            EditorGUILayout.Separator();
            if (GUILayout.Button(Resources.Load<Texture2D>("InputHandler/DragIcon"), new GUIStyle { }, GUILayout.Width(50), GUILayout.Height(50)))
            {
                DragModule();
            }
            EditorGUILayout.Separator();
            if (GUILayout.Button(Resources.Load<Texture2D>("InputHandler/SwipeIcon"), new GUIStyle { }, GUILayout.Width(50), GUILayout.Height(50)))
            {
                SwipeModule();
            }
            EditorGUILayout.Separator();
            if (GUILayout.Button(Resources.Load<Texture2D>("InputHandler/JoystickIcon"), new GUIStyle { }, GUILayout.Width(50), GUILayout.Height(50)))
            {
                MGHelper.LogInfo("Joystick module is not implemented yet:)");
            }
            EditorGUILayout.Separator();
            if (GUILayout.Button(Resources.Load<Texture2D>("InputHandler/DoubleFingerIcon"), new GUIStyle { }, GUILayout.Width(50), GUILayout.Height(50)))
            {
                MGHelper.LogInfo("DoubleFinger module is not implemented yet:)");
            }
            EditorGUILayout.Separator();
            if (GUILayout.Button(Resources.Load<Texture2D>("InputHandler/CameraScrollIcon"), new GUIStyle { }, GUILayout.Width(50), GUILayout.Height(50)))
            {
                MGHelper.LogInfo("CameraScroll module is not implemented yet:)");
            }

            GUILayout.EndHorizontal();

            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Menu to handle Input Handlers
        /// </summary>
        /// 
        public static MGTouchBehaviour GetTouchBehaviour()
        {
            MGTouchBehaviour touchBehaviour = FindObjectOfType<MGTouchBehaviour>();
            if (touchBehaviour == null)
            {
                MGHelper.LogInfo("Input handler created");
                touchBehaviour = new GameObject("MG: InputHandler").AddComponent<MGTouchBehaviour>();
            }
            Selection.activeGameObject = touchBehaviour.gameObject;
            return touchBehaviour;
        }

        [MenuItem("MegaCore/Input Handler/Tap Handler")]
        static void OneTapModule()
        {
            MGTouchBehaviour touchBehaviour = GetTouchBehaviour();
            OneTapHandler tapHandler = touchBehaviour.gameObject.GetComponent<OneTapHandler>();
            if (tapHandler == null)
            {
                touchBehaviour.gameObject.AddComponent<OneTapHandler>();
                MGHelper.LogInfo("Tap handler created");
            }
            else
            {
                MGHelper.LogWarnong("Tap handler already exists");
            }
            touchBehaviour._oneTapHandler = tapHandler;
        }

        [MenuItem("MegaCore/Input Handler/Tap and Hold Handler")]
        static void TapAndHoldModule()
        {
            MGTouchBehaviour touchBehaviour = GetTouchBehaviour();
            TapAndHoldHandler tapAndHoldHandler = touchBehaviour.gameObject.GetComponent<TapAndHoldHandler>();
            if (tapAndHoldHandler == null)
            {
                touchBehaviour.gameObject.AddComponent<TapAndHoldHandler>();
                MGHelper.LogInfo("Tap and hold handler created");
            }
            else
            {
                MGHelper.LogWarnong("Tap and hold handler already exists");
            }
            touchBehaviour._tapAndHoldHandler = tapAndHoldHandler;
        }

        [MenuItem("MegaCore/Input Handler/Drag Handler")]
        static void DragModule()
        {
            MGTouchBehaviour touchBehaviour = GetTouchBehaviour();
            DragHandler dragHandler = touchBehaviour.gameObject.GetComponent<DragHandler>();
            if (dragHandler == null)
            {
                touchBehaviour.gameObject.AddComponent<DragHandler>();
                MGHelper.LogInfo("Drag handler created");
            }
            else
            {
                MGHelper.LogWarnong("Drag handler already exists");
            }
            touchBehaviour._dragHandler = dragHandler;
        }

        [MenuItem("MegaCore/Input Handler/Swipe Handler")]
        static void SwipeModule()
        {
            MGTouchBehaviour touchBehaviour = GetTouchBehaviour();
            SwipeHandler swipeHandler = touchBehaviour.gameObject.GetComponent<SwipeHandler>();
            if (swipeHandler == null)
            {
                touchBehaviour.gameObject.AddComponent<SwipeHandler>();
                MGHelper.LogInfo("Swipe handler created");
            }
            else
            {
                MGHelper.LogWarnong("Swipe handler already exists");
            }
            touchBehaviour._swipeHandler = swipeHandler;
        }

    }

}