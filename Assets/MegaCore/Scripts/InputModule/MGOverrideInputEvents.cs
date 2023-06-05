using UnityEngine;
using UnityEngine.EventSystems;

namespace MegaCore.InputModule
{

    public class OverrideInputEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        InputTouch[] _inputHandlerList;

        private void Start()
        {
            _inputHandlerList = MGTouchBehaviour.Instance.gameObject.GetComponents<InputTouch>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            foreach (InputTouch inputHandler in _inputHandlerList)
                inputHandler.OverrideHUDEvents = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            foreach (InputTouch inputHandler in _inputHandlerList)
                inputHandler.OverrideHUDEvents = false;
        }

        //IEnumerator OnPointerUpCo()
        //{
        //    foreach (MGInputbase inputHandler in _inputHandlerList)
        //        inputHandler.OverrideInputEvents = false;
        //}

    }

}