using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

using MegaCore.Logger;


namespace MegaCore.SceneManager
{

    public class MGSceneBehaviour : MGAbstract
    {
        private static MGSceneBehaviour __instance;
        public static MGSceneBehaviour Instance { get { return __instance; } }
        void Awake()
        {
            if (__instance == null)
            {
                __instance = this;
                DontDestroyOnLoad(gameObject);

                UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else if (__instance != this)
            {
                Destroy(gameObject);
            }
        }

        public MGLoadingPanel _loadingPanel;

        public UnityEvent<Scene, LoadSceneMode> _onSceneLoaded;
        public UnityEvent _onWon;
        public UnityEvent _onLost;
        public UnityEvent _onEven;


        public void Win(float timeToTriggerCallback = 0f, Component callingComp = null, Action<Component> callback = null)
        {
            UpdateGameData();
            if (timeToTriggerCallback > 0f)
                StartCoroutine(GameOverCo(true, timeToTriggerCallback, callingComp, callback));
        }

        public void Lose(float timeToTriggerCallback = 0f, Component callingComp = null, Action<Component> callback = null)
        {
            UpdateGameData();
            if (timeToTriggerCallback > 0f)
                StartCoroutine(GameOverCo(false, timeToTriggerCallback, callingComp, callback));
        }

        public void Even(float timeToTriggerCallback = 0f, Component callingComp = null, Action<Component> callback = null)
        {
            UpdateGameData();
            if (timeToTriggerCallback > 0f)
                StartCoroutine(GameOverCo(null, timeToTriggerCallback, callingComp, callback));
        }

        IEnumerator GameOverCo(bool? hasWon, float timeToTriggerCallback, Component callingComp, Action<Component> callback)
        {
            yield return new WaitForSeconds(timeToTriggerCallback);
            if (hasWon == true)
                _onWon?.Invoke();
            else if (hasWon == false)
                _onLost?.Invoke();
            else if (hasWon == null)
                _onEven?.Invoke();

            callback?.Invoke(callingComp);
        }

        void UpdateGameData()
        {
            if (false/* Check if data manager assigned */)
            {
                /* update game data byy MGDataManager */
            }
            MGLogger.LogInfo("Data object updated", MegaCore.Module.data);
        }

        public virtual void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            _currentScene = (SceneEnum)Enum.Parse(typeof(SceneEnum), scene.name);
            _onSceneLoaded?.Invoke(scene, loadMode);
            MGLogger.LogInfo("Scene Loaded", MegaCore.Module.scene);
        }


        /// <summary>
        /// Sync scene loading
        /// </summary>
        SceneEnum _currentScene;
        SceneEnum _prevScene = SceneEnum.None;
        public void LoadScene(SceneEnum scene)
        {
            _prevScene = _currentScene;
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
        }

        public void LoadPrevScene()
        {
            if (_prevScene != SceneEnum.None)
                UnityEngine.SceneManagement.SceneManager.LoadScene(_prevScene.ToString());
            else
                MGLogger.LogError("Previous scene is none", MegaCore.Module.scene);
            _prevScene = SceneEnum.None;
        }



        /// <summary>
        /// LoadSceneAsync
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="timeToLoad"></param>
        /// <param name="panelBackground"></param>
        /// <param name="splash"></param>
        /// <param name="hint"></param>
        /// <param name="loadingText"></param>
        /// <param name="showLoadingBar"></param>
        /// <param name="showLoadingScroll"></param>
        /// <param name="showLoadingPercent"></param>
        /// <param name="callingComp"></param>
        /// <param name="callback"></param>
        public void LoadSceneAsync(SceneEnum scene, float timeToLoad = 0f,
             LoadingBackgroungPanel panelBackground = 0, LoadingSplash splash = 0, LoadingHint hint = 0, LoadingText loadingText = 0,
            bool showLoadingBar = true, bool showLoadingScroll = true, bool showLoadingPercent = true,
            Component callingComp = null, Action<Component> callback = null)
        {
            _loadingPanel.Show(panelBackground, splash, hint, loadingText, showLoadingBar, showLoadingScroll, showLoadingPercent);
            LoadSceneAsyncCo(scene, timeToLoad, callingComp, callback);
        }


        /// <summary>
        /// LoadSceneAsyncCo
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="extratimeToLoad"></param>
        /// <param name="callingComp"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        IEnumerator LoadSceneAsyncCo(SceneEnum scene, float extratimeToLoad, Component callingComp = null, Action<Component> callback = null)
        {
            yield return null;
            AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene.ToString());
            asyncOperation.allowSceneActivation = false;
            while (!asyncOperation.isDone)
            {
                _loadingPanel.UpdateLoadingBar(asyncOperation.progress);
                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }
                yield return null;
            }
            _loadingPanel.UpdateLoadingBar(1f);

            yield return new WaitForSeconds(extratimeToLoad);

            _loadingPanel.Hide();

            callback?.Invoke(callingComp);
        }
    }

}