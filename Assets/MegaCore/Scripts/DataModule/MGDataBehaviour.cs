using System.IO;

using UnityEngine;

namespace MegaCore.DataModule
{

    public class MGDataBehaviour : MGAbstract
    {
        private static MGDataBehaviour __instance;
        public static MGDataBehaviour Instance { get { return __instance; } }

        public static string __GAME_DATA_PATH
        {
            get { return string.Format(MGDataConfig.RELATIVE_GAME_DATA_PATH, Application.persistentDataPath); }
        }
        public static string __GAME_DATA_JSON;

        public MGLocalDataManager _localDataManager;
        public MGWebDataManager _webDataManager;

        void Awake()
        {
            if (__instance == null)
            {
                __instance = this;
                DontDestroyOnLoad(gameObject);

                if (_localDataManager != null)
                    LoadLocalData();
                else
                    Debug.LogWarning("MG: Local data module is not assigned!!");
            }
            else if (__instance != this)
            {
                Destroy(gameObject);
            }
        }

        public DataClass GData
        {
            get
            {
                if (_localDataManager != null)
                    return _localDataManager._dataObject;
                else
                {
                    Debug.LogError("MG: Local data manager is not assigned!!");
                    return null;
                }
            }
        }

        private void LoadLocalData()
        {
            if (File.Exists(__GAME_DATA_PATH))
            {
                _localDataManager.Load();
                Debug.Log("MG: local data loaded successfully");
            }
            else
                Debug.LogWarning("MG: game data file does not exit!!");
        }

        private void SaveLocalData()
        {
            if (File.Exists(__GAME_DATA_PATH))
            {
                _localDataManager.Save();
                Debug.Log("MG: local data saved successfully");
            }
            else
                Debug.LogWarning("MG: game data file does not exit!!");
        }

        public void Save()
        {
            SaveLocalData();
        }


        private void LoadWebData()
        {
            //
        }

    }

}