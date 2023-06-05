using MegaCore.Logger;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MegaCore.SceneManager
{

    public class MGSplashScene : MonoBehaviour
    {
        public float _extraTimeToHideSplash;

        public bool _loginFlow; /* register / login / update web game data */

        bool _registerDone = false;
        bool _loginDone = false;
        bool _sceneLoaded = false;

        void Awake()
        {
            ShowSplash();
            DeactivateAudio();

            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnFirstSceneLoaded;
        }

        private void OnFirstSceneLoaded(Scene scene, LoadSceneMode mdoe)
        {
            _sceneLoaded = true;
        }

        IEnumerator Start()
        {
            StartCoroutine(Logup());
            yield return new WaitUntil(() => _registerDone);

            StartCoroutine(Login());
            yield return new WaitUntil(() => _loginDone);

            LoadGameData();

            ActivateAudio();

            yield return new WaitUntil(() => _sceneLoaded);
            yield return new WaitForSeconds(_extraTimeToHideSplash);

            HideSplash();
        }

        void ShowSplash(LoadingBackgroungPanel panelBackground = 0, LoadingSplash splash = 0, LoadingHint hint = 0, LoadingText loadingText = 0,
            bool showLoadingBar = true, bool showLoadingScroll = true, bool showLoadingPercent = true)
        {
            MGSceneBehaviour.Instance._loadingPanel.Show(panelBackground, splash, hint, loadingText, showLoadingBar, showLoadingScroll, showLoadingPercent);
        }

        void HideSplash()
        {
            MGSceneBehaviour.Instance._loadingPanel.Hide();
        }

        IEnumerator Logup()
        {
            yield return null;
            _registerDone = true;
            MGLogger.LogInfo("Player logged up", MegaCore.Module.scene);
        }

        IEnumerator Login()
        {
            yield return null;
            _loginDone = true;
            MGLogger.LogInfo("Player logged in", MegaCore.Module.scene);
        }

        void LoadGameData()
        {
            MGLogger.LogInfo("Game data loaded", MegaCore.Module.scene);
        }

        void ActivateAudio()
        {
            MGLogger.LogInfo("Audio activated", MegaCore.Module.scene);
        }

        void DeactivateAudio()
        {
            MGLogger.LogInfo("Audio deactivated", MegaCore.Module.scene);
        }



    }

}
