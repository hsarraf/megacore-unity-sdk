using System.Collections.Generic;

using UnityEngine;

using MegaCore.MediationService;
using MegaCore.DataModule;
using MegaCore.AudioManager;
using MegaCore.PoolingModule;
using MegaCore.Popup;
using MegaCore.SceneManager;
using MegaCore.InputModule;
using MegaCore.Logger;


namespace MegaCore
{

    //[RequireComponent(typeof(MGLogger))]
    public class MegaCore : MonoBehaviour
    {
        private static MegaCore _instance;
        public static MegaCore Instance { get { return _instance; } }
        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                MakeModuleMap();
            }
        }

        public enum Module
        {
            input, data, pool, mediation, scene, audio, popup
        }

        public Dictionary<Module, MGAbstract> _moduleDict;

        public MGLogger _logger;

        void MakeModuleMap()
        {
            _moduleDict = new Dictionary<Module, MGAbstract>();
            _moduleDict[Module.mediation] = FindObjectOfType<MGMediationBehaviour>();
            _moduleDict[Module.input] = FindObjectOfType<MGTouchBehaviour>();
            _moduleDict[Module.data] = FindObjectOfType<MGDataBehaviour>();
            _moduleDict[Module.scene] = FindObjectOfType<MGSceneBehaviour>();
            _moduleDict[Module.pool] = FindObjectOfType<MGPoolBehaviour>();
            _moduleDict[Module.audio] = FindObjectOfType<MGAudioBehaviour>();
            _moduleDict[Module.popup] = FindObjectOfType<MGPopupBehaviour>();
        }

        //void Start()
        //{
        //    Debug.Log(CheckIfModuleExists("MGSceneManager"));
        //    Debug.Log(CheckIfModuleExists("MGDataManager"));
        //}

        //public List<Module> GetExistingModules()
        //{
        //    List<Module> _existingModules = new List<Module>();
        //    foreach (string moduleName in Enum.GetNames(typeof(Module)))
        //    {
        //        var type = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
        //                           from t in assembly.GetTypes()
        //                           where t.Name == moduleName
        //                           select t);
        //    }
        //}

    }

}