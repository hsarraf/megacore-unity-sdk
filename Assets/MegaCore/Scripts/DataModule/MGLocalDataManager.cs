using System.IO;

using Newtonsoft.Json;

using UnityEngine;


namespace MegaCore.DataModule
{
    public class MGLocalDataManager : MGDataManager
    {
        private static MGLocalDataManager _instance;
        public static MGLocalDataManager Instance { get { return _instance; } }
        void Awake()
        {
            if (_instance == null)
                _instance = this;
        }

        public void Load()
        {
            try
            {
                string json;
                using (StreamReader stream = new StreamReader(MGDataBehaviour.__GAME_DATA_PATH)) {
                    json = stream.ReadToEnd();
                    _dataObject = JsonConvert.DeserializeObject<DataClass>(json);
                }
                MGDataBehaviour.__GAME_DATA_JSON = json;
            }
            catch (JsonReaderException err)
            {
                Debug.LogError(string.Format("MG: {0}", err.Message));
                _dataObject = null;
            }
        }

        public void Save()
        {
            using (StreamWriter stream = new StreamWriter(MGDataBehaviour.__GAME_DATA_PATH))
            {
                string json = MGDataBehaviour.__GAME_DATA_JSON = _dataObject.Dump(true);
                if (json != null)
                    stream.Write(json);
            }
        }

    }

}
