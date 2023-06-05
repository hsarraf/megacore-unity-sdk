using System;
using Newtonsoft.Json;
using UnityEngine;

namespace MegaCore.DataModule
{
    [Serializable]
    public class DataAbstract
    {
        public bool init;

        public string username;
        public string uid;

        public int level;
        public int score;
        public int coin;

        public bool music;
        public bool sfx;

        public string Dump(bool indented = false)
        {
            try
            {
                return JsonConvert.SerializeObject(this, indented ? Formatting.Indented : Formatting.None);
            }
            catch(JsonWriterException err)
            {
                Debug.LogError(string.Format("MG: {0}", err.Message));
                return null;
            }
        }

    }

}