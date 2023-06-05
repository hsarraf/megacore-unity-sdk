using System;
using System.Collections.Generic;
using UnityEngine;


namespace MegaCore.AudioManager
{

    public class MGAudioBehaviour : MGAbstract
    {
        private static MGAudioBehaviour __instance;
        public static MGAudioBehaviour Instance { get { return __instance; } }
        void Awake()
        {
            if (__instance == null)
            {
                __instance = this;
                DontDestroyOnLoad(gameObject);

                CreateEnumClipMap();
            }
            else if (__instance != this)
            {
                Destroy(gameObject);
            }
        }

        public AudioSource _longAudioSource;
        public AudioSource _mediumAudioSource;
        public AudioSource _shortAudioSource;

        public List<AudioClip> _longAudioClips;
        public List<AudioClip> _mediumAudioClips;
        public List<AudioClip> _shortAudioClips;

        Dictionary<LongClip, AudioClip> _longEnumClipMap;
        Dictionary<MediumClip, AudioClip> _mediumEnumClipMap;
        Dictionary<ShortClip , AudioClip> _shortEnumClipMap;


        private void CreateEnumClipMap()
        {
            _longEnumClipMap = new Dictionary<LongClip, AudioClip>();
            _mediumEnumClipMap = new Dictionary<MediumClip, AudioClip>();
            _shortEnumClipMap = new Dictionary<ShortClip, AudioClip>();
            foreach (LongClip clipName in Enum.GetValues(typeof(LongClip)))
                _longEnumClipMap[clipName] = Resources.Load<AudioClip>(string.Format("AudioManager/AudioClips/Long/{0}", clipName.ToString()));
            foreach (MediumClip clipName in Enum.GetValues(typeof(MediumClip)))
                _mediumEnumClipMap[clipName] = Resources.Load<AudioClip>(string.Format("AudioManager/AudioClips/Medium/{0}", clipName.ToString()));
            foreach (ShortClip clipName in Enum.GetValues(typeof(ShortClip)))
                _shortEnumClipMap[clipName] = Resources.Load<AudioClip>(string.Format("AudioManager/AudioClips/Short/{0}", clipName.ToString()));
        }

        /// <summary>
        /// Long Audio Handler
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        /// <param name="pitch"></param>
        public void Play(LongClip clip, float volume = 1f, float pitch = 1)
        {
            _longAudioSource.volume = volume;
            _longAudioSource.pitch = pitch;
            _longAudioSource.clip = _longEnumClipMap[clip];
            _longAudioSource.Play();
        }
        public void StopLong()
        {
            _longAudioSource.Stop();
        }
        public void PauseLong()
        {
            _longAudioSource.Pause();
        }

        /// <summary>
        /// Medium Audio Handler
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        /// <param name="pitch"></param>
        public void Play(MediumClip clip, float volume = 1f, float pitch = 1)
        {
            _mediumAudioSource.volume = volume;
            _mediumAudioSource.pitch = pitch;
            _mediumAudioSource.clip = _mediumEnumClipMap[clip];
            _mediumAudioSource.Play();
        }
        public void StopMedium()
        {
            _longAudioSource.Stop();
        }
        public void PauseMedium()
        {
            _longAudioSource.Pause();
        }

        /// <summary>
        /// Short Audio Handler
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        /// <param name="pitch"></param>
        public void Play(ShortClip clip, float volume = 1f, float pitch = 1)
        {
            _shortAudioSource.volume = volume;
            _shortAudioSource.pitch = pitch;
            _shortAudioSource.PlayOneShot(_shortEnumClipMap[clip]);
        }
        //
        // no needto stop or pause

    }

}