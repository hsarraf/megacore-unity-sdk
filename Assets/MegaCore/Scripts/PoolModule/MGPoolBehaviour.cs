using System.Collections.Generic;
using UnityEngine;


namespace MegaCore.PoolingModule
{
    public class MGPoolBehaviour : MGAbstract
    {

        public int _preStartCount = 0;

        public MGElement[] _elementPrefabList;
        Dictionary<MGElement.Type, MGElement> _prefabMap;

        public Dictionary<MGElement.Type, List<MGElement>> _poolMap;
        List<MGElement> _activeElements;

        // refrences of zero position and rotation to make less copies in spawm
        //
        Vector3 _zeroPos = Vector3.zero;
        Quaternion _ZeroRot = Quaternion.identity;


        private static MGPoolBehaviour _instance;
        public static MGPoolBehaviour Instance { get { return _instance; } }
        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;

                // crate pool map object
                //
                _activeElements = new List<MGElement>();
                _poolMap = new Dictionary<MGElement.Type, List<MGElement>>();

                // initialize pool map and prefab map
                //
                _prefabMap = new Dictionary<MGElement.Type, MGElement>();
                foreach (MGElement element in _elementPrefabList)
                {
                    _prefabMap[element._type] = element;
                    _poolMap[element._type] = new List<MGElement>();
                }

                // instantiate pre-start elements 
                //
                foreach (MGElement element in _elementPrefabList)
                {
                    for (int i = 0; i < _preStartCount; i++)
                    {
                        _poolMap[element._type].Add(element);
                        element.gameObject.SetActive(false);
                    }
                }
            }
        }

        public MGElement Spawn(MGElement.Type type, ref Vector3 position, ref Quaternion rotation)
        {
            MGElement element;
            if (_poolMap[type].Count > 0)
            {
                element = _poolMap[type][0];
                _poolMap[type].RemoveAt(0);
            }
            else
            {
                element = Instantiate(_prefabMap[type]);
            }
            _activeElements.Add(element);
            element.transform.SetPositionAndRotation(position, rotation);
            element.gameObject.SetActive(true);
            return element;
        }

        public MGElement Spawn(MGElement.Type type)
        {
            return Spawn(type, ref _zeroPos, ref _ZeroRot);
        }


        public void Repool(MGElement element)
        {
            _poolMap[element._type].Add(element);
            _activeElements.Remove(element);
            element.gameObject.SetActive(false);
        }

        public void ResetElement()
        {
            foreach(MGElement element in _activeElements)
            {
                element.gameObject.SetActive(false);
                _poolMap[element._type].Add(element);
            }
            _activeElements.Clear();
            _activeElements = null;
            _activeElements = new List<MGElement>();
        }

    }

}