using UnityEngine;

namespace MegaCore.InputModule
{
    public class MGInputHandGizmo : MonoBehaviour
    {

        internal RectTransform _transform;
        internal Canvas _canvas;

        private void Awake()
        {
            _transform = transform.GetChild(0).GetComponent<RectTransform>();
            _canvas = GetComponent<Canvas>();
        }

        internal void SetScaleFactor(float scaleFactor)
        {
            _canvas.scaleFactor = scaleFactor;
        }

    }

}