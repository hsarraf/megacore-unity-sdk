using UnityEngine;

namespace MegaCore.DataModule
{
    public class MGWebDataManager : MGDataManager
    {
        private static MGWebDataManager _instance;
        public static MGWebDataManager Instance { get { return _instance; } }
        void Awake()
        {
            if (_instance == null)
                _instance = this;
        }

    }

}
