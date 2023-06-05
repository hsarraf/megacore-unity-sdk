using UnityEngine;

namespace MegaCore.DataModule
{

    public class MGDataManager : MonoBehaviour
    {
        public DataClass _dataObject;
        public DataClass GData
        {
            get { return _dataObject; }
            set { _dataObject = value; }
        }

        public void SyncWebAndLocalData()
        {
        }

    }

}