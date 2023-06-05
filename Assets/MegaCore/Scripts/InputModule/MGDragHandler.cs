using UnityEngine;
using UnityEngine.Events;


namespace MegaCore.InputModule
{

    public class DragHandler : TapAndHoldHandler, ITouchStationary, ITouchMoving
    {
        // stationary callbacks
        //
        public UnityEvent<Vector2> _onStationary;
        public delegate void StationaryDelegate(ref Vector2 touchPosition);
        public StationaryDelegate _stationaryDelegate;
        public float _stationaryTimer = 0f;

        // drag callbacks
        //
        public UnityEvent<Vector2, Vector2, Vector2, float> _onDragging;
        public delegate void DragDelegate(ref Vector2 touchPosition, ref Vector2 dragDirection, ref Vector2 dragVelocity, float dragLength);
        public DragDelegate _dragDelegate;

        //  drag trigger
        //
        public UnityEvent<Vector2, Vector2, Vector2, float> _onDragLengthTriggered;
        public DragDelegate _dragLengthTriggerDelegate;

        private Vector2 _touchInitPos;
        public float _dragLengthTrigger;
        bool _dragLengthTriggered = false;

        public Vector2 _dragVector;
        public float _dragLength;


        public override void OnTouchStarted(ref Vector2 touchPosition)
        {
            _touchInitPos = _touchPosition;
            base.OnTouchStarted(ref touchPosition);
        }

        public override void OnTouchMoving(ref Vector2 touchPosition, ref Vector2 dragSpeed)
        {
            _dragVector = _touchPosition - _touchInitPos;
            _dragLength = _dragVector.magnitude;

            _onDragging?.Invoke(touchPosition, _dragVector, dragSpeed, _dragLength);
            _dragDelegate?.Invoke(ref touchPosition, ref _dragVector, ref dragSpeed, _dragLength);

            if (_dragLength > _dragLengthTrigger)
            {
                if (!_dragLengthTriggered)
                {
                    _onDragLengthTriggered?.Invoke(touchPosition, _dragVector, dragSpeed, _dragLength);
                    _dragDelegate?.Invoke(ref touchPosition, ref _dragVector, ref dragSpeed, _dragLength);
                    OnDragLengthTriggered(ref _dragVector);
                    _dragLengthTriggered = true;
                }
            }
            base.OnTouchMoving(ref touchPosition, ref dragSpeed);
        }

        public override void OnTouchEnded(ref Vector2 touchPosition, bool hasCanceled)
        {
            _dragVector = Vector2.zero;
            _dragLength = 0f;
            _dragLengthTriggered = false;


            base.OnTouchEnded(ref touchPosition, hasCanceled);
        }

        protected virtual void OnDragLengthTriggered(ref Vector2 dragVector)
        {
        }

        public override void OnTouchStationary(ref Vector2 touchPosition)
        {
            _onStationary?.Invoke(_touchPosition);
            _stationaryDelegate?.Invoke(ref _touchPosition);

            base.OnTouchStationary(ref touchPosition);
        }
    }

}
