using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using UnityEditor;
using UnityEngine;


namespace MegaCore.AudioManager
{

    [ExecuteInEditMode]
    [CustomEditor(typeof(MGAudioBehaviour))]
    public class MGAudioBehaviourEditor : Editor
    {
        static MGAudioBehaviour _audioBehaviour;
        private Texture2D _logo = null;
        private Texture2D _playBttnIcon = null;

        static string _audioClipsDir;
        static string _audioClipEnumsScriptPath;

        SerializedProperty _longAudioClips;

        static string[] _exts = new[] { "mp3", "wav", "ogg" };

        public class AudioClipClass
        {
            public string _path;
            public string _name;
            public string _abbrName;
            public string _sanitName;
        }

        static int _maxClipNameChar = 20;

        public static List<AudioClipClass> _longClipObjects;
        public static List<AudioClipClass> _mediumClipObjects;
        public static List<AudioClipClass> _shortClipObjects;

        Vector2 _longClipsScroller;
        Vector2 _mediumClipsScroller;
        Vector2 _shortClipsScroller;

        GUISkin _logGuiSkin;


        void OnEnable()
        {
            _audioBehaviour = (MGAudioBehaviour)target;

            _longAudioClips = serializedObject.FindProperty("_longAudioClips");

            _audioBehaviour.GetComponent<Transform>().hideFlags |= HideFlags.HideInInspector;
            _audioBehaviour._longAudioSource.hideFlags |= HideFlags.HideInInspector;
            _audioBehaviour._mediumAudioSource.hideFlags |= HideFlags.HideInInspector;
            _audioBehaviour._shortAudioSource.hideFlags |= HideFlags.HideInInspector;

            _logo = Resources.Load<Texture2D>("AudioManager/banner");
            _playBttnIcon = Resources.Load<Texture2D>("AudioManager/PlayBttnIcon");

            _logGuiSkin = Resources.Load<GUISkin>("AudioManager/GUISkin");

            _audioClipsDir = string.Format("{0}/MegaCore/Resources/AudioManager/AudioClips/", Application.dataPath);
            _audioClipEnumsScriptPath = string.Format("{0}/MegaCore/Scripts/AudioManager/MGAudioClipEnums.cs", Application.dataPath);

            DisplayAudioClips();
        }

        public override void OnInspectorGUI()
        {
            GUI.skin = _logGuiSkin;

            GUILayout.Label(_logo, new GUIStyle { alignment = TextAnchor.LowerCenter });

            if (Application.isPlaying)
            {
                EditorGUILayout.Separator();
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label(new GUIContent("MegaCore Audio Manger is working..."), new GUIStyle { fontStyle = FontStyle.Bold, normal = new GUIStyleState { textColor = Color.yellow } });
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                EditorGUILayout.Separator();
                return;
            }

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical();
            GUILayout.Label(new GUIContent("Long Clips"), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(23));
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, GUILayout.Width(150), GUILayout.Height(1));
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            _longClipsScroller = EditorGUILayout.BeginScrollView(_longClipsScroller, GUILayout.Height(250));
            foreach (AudioClipClass clipObject in _longClipObjects)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(new GUIContent(clipObject._abbrName, clipObject._name), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(25));
                if (GUILayout.Button(new GUIContent { image = _playBttnIcon }, new GUIStyle { }, GUILayout.Width(18), GUILayout.Height(18)))
                    Application.OpenURL(clipObject._path);
                GUILayout.EndHorizontal();
                EditorGUILayout.Separator();
            }
            EditorGUILayout.EndScrollView();
            GUILayout.EndVertical();

            EditorGUILayout.Separator();

            GUILayout.BeginVertical();
            GUILayout.Label(new GUIContent("Medium Clips"), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(23));
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, GUILayout.Width(150), GUILayout.Height(1));
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            _mediumClipsScroller = EditorGUILayout.BeginScrollView(_mediumClipsScroller, GUILayout.Height(250));
            foreach (AudioClipClass clipObject in _mediumClipObjects)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(new GUIContent(clipObject._abbrName, clipObject._name), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(25));
                if (GUILayout.Button(new GUIContent { image = _playBttnIcon }, new GUIStyle { }, GUILayout.Width(18), GUILayout.Height(18)))
                    Application.OpenURL(clipObject._path);
                GUILayout.EndHorizontal();
                EditorGUILayout.Separator();
            }
            EditorGUILayout.EndScrollView();
            GUILayout.EndVertical();

            EditorGUILayout.Separator();

            GUILayout.BeginVertical();
            GUILayout.Label(new GUIContent("Short Clips"), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(23));
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, GUILayout.Width(150), GUILayout.Height(1));
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            _shortClipsScroller = EditorGUILayout.BeginScrollView(_shortClipsScroller, GUILayout.Height(250));
            foreach (AudioClipClass clipObject in _shortClipObjects)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(new GUIContent(clipObject._abbrName, clipObject._name), GUI.skin.box, GUILayout.Width(150), GUILayout.Height(25));
                if (GUILayout.Button(new GUIContent { image = _playBttnIcon }, new GUIStyle { }, GUILayout.Width(18), GUILayout.Height(18)))
                    Application.OpenURL(clipObject._path);
                GUILayout.EndHorizontal();
                EditorGUILayout.Separator();
            }
            EditorGUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Open", GUILayout.Width(100), GUILayout.Height(35)))
                Application.OpenURL(_audioClipsDir);

            EditorGUILayout.Separator();

            if (GUILayout.Button("Generate Enumeration Map", GUILayout.Width(400), GUILayout.Height(35)))
                ApplyAudioClips();

            GUILayout.EndHorizontal();


            EditorGUILayout.Separator();
        }

        static void DisplayAudioClips()
        {
            if (Application.isPlaying)
                return;

            _longClipObjects = new List<AudioClipClass>();
            _mediumClipObjects = new List<AudioClipClass>();
            _shortClipObjects = new List<AudioClipClass>();
            foreach (string path in GetLongAudioClipsPath())
                _longClipObjects.Add(new AudioClipClass { _path = path, _name = ClipName(path), _abbrName = AbbriviateClipName(path), _sanitName = ClipName(path) });
            foreach (string path in GetMediumAudioClipsPath())
                _mediumClipObjects.Add(new AudioClipClass { _path = path, _name = ClipName(path), _abbrName = AbbriviateClipName(path), _sanitName = ClipName(path) });
            foreach (string path in GetShortAudioClipsPath())
                _shortClipObjects.Add(new AudioClipClass { _path = path, _name = ClipName(path), _abbrName = AbbriviateClipName(path), _sanitName = ClipName(path) });
        }


        static void ApplyAudioClips()
        {
            DisplayAudioClips();
            using (StreamWriter stream = new StreamWriter(_audioClipEnumsScriptPath))
            {
                stream.WriteLine("namespace MegaCore.AudioManager \n{\n");

                stream.WriteLine("\tpublic enum LongClip \n\t{");
                foreach (AudioClipClass clip in _longClipObjects)
                    stream.WriteLine(string.Format("\t\t{0},", clip._sanitName));
                stream.WriteLine("\t}\n");

                stream.WriteLine("\tpublic enum MediumClip \n\t{");
                foreach (AudioClipClass clip in _mediumClipObjects)
                    stream.WriteLine(string.Format("\t\t{0},", clip._sanitName));
                stream.WriteLine("\t}\n");

                stream.WriteLine("\tpublic enum ShortClip \n\t{");
                foreach (AudioClipClass clip in _shortClipObjects)
                    stream.WriteLine(string.Format("\t\t{0},", clip._sanitName));
                stream.WriteLine("\t}\n");

                stream.WriteLine("}");
            }
        }

        //static void GenerateEnumClipMapJson()
        //{
        //    Dictionary<LongClip, string> enumClipMap = new Dictionary<LongClip, string>();
        //    foreach (AudioClipClass clip in _longClipObjects)
        //    {
        //        AudioClip ac;
        //        ac.name = 
        //        _audioBehaviour._longAudioClips.Add(new AudioClip())
        //        enumClipMap[(LongClip)Enum.Parse(typeof(LongClip), clip._sanitName)] = new AudioClip;
        //    }
        //}

        static string[] GetLongAudioClipsPath()
        {
            string[] clipPathList = Directory.EnumerateFiles(string.Format("{0}/Long", _audioClipsDir), "*.*").
                            Where(file => _exts.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase))).ToArray();
            foreach (string clipPath in clipPathList)
                if (!IsSanitized(ClipName(clipPath)))
                    File.Move(clipPath, string.Format("{0}{1}", Path.Combine(Path.GetDirectoryName(clipPath), SanitizeClipName(ClipName(clipPath))), Path.GetExtension(clipPath)));
            return Directory.EnumerateFiles(string.Format("{0}/Long", _audioClipsDir), "*.*").
                            Where(file => _exts.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase))).ToArray();
        }

        static string[] GetMediumAudioClipsPath()
        {
            string[] clipPathList = Directory.EnumerateFiles(string.Format("{0}/Medium", _audioClipsDir), "*.*").
                            Where(file => _exts.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase))).ToArray();
            foreach (string clipPath in clipPathList)
                if (!IsSanitized(ClipName(clipPath)))
                    File.Move(clipPath, string.Format("{0}{1}", Path.Combine(Path.GetDirectoryName(clipPath), SanitizeClipName(ClipName(clipPath))), Path.GetExtension(clipPath)));
            return Directory.EnumerateFiles(string.Format("{0}/Medium", _audioClipsDir), "*.*").
                            Where(file => _exts.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase))).ToArray();
        }

        static string[] GetShortAudioClipsPath()
        {
            string[] clipPathList = Directory.EnumerateFiles(string.Format("{0}/Short", _audioClipsDir), "*.*").
                            Where(file => _exts.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase))).ToArray();
            foreach (string clipPath in clipPathList)
                if (!IsSanitized(ClipName(clipPath)))
                {
                    File.Move(clipPath, string.Format("{0}{1}", Path.Combine(Path.GetDirectoryName(clipPath), SanitizeClipName(ClipName(clipPath))), Path.GetExtension(clipPath)));
                    Debug.Log(Path.GetExtension(clipPath));
                }
            return Directory.EnumerateFiles(string.Format("{0}/Short", _audioClipsDir), "*.*").
                            Where(file => _exts.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase))).ToArray();
        }


        static string ClipName(string clipPath)
        {
            return Path.GetFileNameWithoutExtension(clipPath);
        }

        static bool IsSanitized(string name)
        {
            return Regex.IsMatch(name, "^[a-z_]\\w*$");
        }

        static string SanitizeClipName(string clipPath)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in ClipName(clipPath))
            {
                if (char.IsLetter(c) || char.IsNumber(c))
                    sb.Append(char.ToLower(c));
                else
                    sb.Append('_');
            }
            return string.Format("ac_{0}_{1}", sb.ToString(), UnityEngine.Random.Range(0, 10000));
        }

        static string AbbriviateClipName(string clipPath)
        {
            char[] abbrClipName = new char[_maxClipNameChar];
            string clipName = ClipName(clipPath);
            if (clipName.Length > _maxClipNameChar - 3)
            {
                for (int i = 0; i < _maxClipNameChar - 3; i++)
                    abbrClipName[i] = clipName[i];
                abbrClipName[_maxClipNameChar - 3] = abbrClipName[_maxClipNameChar - 2] = abbrClipName[_maxClipNameChar - 1] = '.';
                return new string(abbrClipName);
            }
            return clipName;
        }


        public static MGAudioBehaviour GetAudioBehaviour()
        {
            MGAudioBehaviour audioBehaviour = FindObjectOfType<MGAudioBehaviour>();
            if (audioBehaviour == null)
            {
                //MGHelper.LogInfo("Mediation service created");
                audioBehaviour = new GameObject("MG: AudioBehaviour").AddComponent<MGAudioBehaviour>();
                audioBehaviour._longAudioSource = audioBehaviour.gameObject.AddComponent<AudioSource>();
                audioBehaviour._mediumAudioSource = audioBehaviour.gameObject.AddComponent<AudioSource>();
                audioBehaviour._shortAudioSource = audioBehaviour.gameObject.AddComponent<AudioSource>();
            }
            Selection.activeGameObject = audioBehaviour.gameObject;
            return audioBehaviour;
        }


        [MenuItem("MegaCore/Audio Manager/Audio Behaviour")]
        static void AudioBehaviour()
        {
            MGAudioBehaviour mediationBehaviour = GetAudioBehaviour();
        }

    }

}