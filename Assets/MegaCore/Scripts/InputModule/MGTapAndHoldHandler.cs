using UnityEngine;
using UnityEngine.Events;

namespace MegaCore.InputModule
{

    public class TapAndHoldHandler : OneTapHandler, ITouchHolding
    {
        // raycast event and delegate
        //
        public UnityEvent<RaycastHit> _onRaycastTriggered;
        public delegate void RaycastDelegate(ref RaycastHit hitInfo);
        public RaycastDelegate _raycastDelegate;

        // hold event and delegate
        //
        public UnityEvent<Vector2, float> _onHold;
        public delegate void HoldDelegate(ref Vector2 touchPosition, float duration);
        public HoldDelegate _holdDelegate;

        // hold timeout trigger event and delegate
        //
        public UnityEvent<Vector2> _onHoldTimeTriggered;
        public delegate void HoldTimeoutDelegate(ref Vector2 touchPosition);
        public HoldTimeoutDelegate _holdTimeDelegate;

        public float _touchTimer = 0f;
        public float _touchTriggerTimer = 0f;
        float _holdTriggerTime;

        private void LateUpdate()
        {
            if (_touchState == TouchState.touching)
            {
                _touchTimer += Time.deltaTime;
                _touchTriggerTimer += Time.deltaTime;
                OnTouchHolding(ref _touchPosition, _touchTimer);
                if (_touchTriggerTimer > _holdTriggerTime)
                {
                    _onHoldTimeTriggered?.Invoke(_touchPosition);
                    _holdTimeDelegate?.Invoke(ref _touchPosition);
                    _touchTriggerTimer = 0f;
                }
                // raycast handler
                //
                Raycast();
            }
            else
            {
                _touchTimer = 0f;
            }
        }

        public override void OnRaycastTriggered(ref RaycastHit hitInfo)
        {
            _onRaycastTriggered?.Invoke(hitInfo);
            _raycastDelegate?.Invoke(ref hitInfo);

            base.OnRaycastTriggered(ref hitInfo);
        }

        public virtual void OnTouchHolding(ref Vector2 touchPosition, float duration)
        {
            _onHold?.Invoke(touchPosition, _touchTimer);
            _holdDelegate?.Invoke(ref touchPosition, _touchTimer);

            base.OnTouchStationary(ref touchPosition);
        }

    }

}
