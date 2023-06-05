using System;
using UnityEngine;

namespace MegaCore.InputModule
{
    public class InputTouch : MonoBehaviour, IRaycastable
    {

        public enum InputType
        {
            Tap, Swipe, Joystick, doubleTap, multitouch
        }
        public enum TouchState
        {
            touching, noTouching, inactive
        }

        public enum CallbackType
        {
            tap, untap, stationary, drag, na
        }

        protected bool _active = true;
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        protected bool _overrideInputEvents = false;
        public bool OverrideHUDEvents
        {
            get { return _overrideInputEvents; }
            set { _overrideInputEvents = value; }
        }

        public InputType _inputType;
        public TouchState _touchState = TouchState.noTouching;
        public CallbackType _callbakType = CallbackType.na;

        protected Touch _touch;
        public Vector2 _touchPosition;
        protected Vector2 _lastTouchPosition;

        public Vector2 _touchSpeedVector;
        public float _touchSpeed;
        public float _movingThreshold;

        public MGInputHandGizmo _handGizmo;
        public float _handScaleFactor = 1f;

        public bool _debugMode = true;

        Camera _camera;
        RaycastHit _hitInfo;
        public string _colliderName;
        public bool _raycastable = false;
        public LayerMask _raycastLayer;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            Application.targetFrameRate = 60;
        }

        public virtual void OnTouchStarted(ref Vector2 touchPosition)
        {
            _touchState = TouchState.touching;
            _callbakType = CallbackType.tap;
            ActivateHand();
            MoveHand(ref touchPosition);
        }

        public virtual void OnTouchMoving(ref Vector2 touchPosition, ref Vector2 dragSpeed)
        {
            _callbakType = CallbackType.drag;
        }

        public virtual void OnTouchStationary(ref Vector2 touchPosition)
        {
            _callbakType = CallbackType.stationary;
        }

        public virtual void OnTouchEnded(ref Vector2 touchPosition, bool hasCanceled)
        {
            _touchState = TouchState.noTouching;
            _callbakType = CallbackType.untap;
            _touchSpeedVector = Vector2.zero;
            _touchSpeed = 0f;
            DeactivateHand();
        }

        public virtual void OnNoTouching()
        {
            _touchState = TouchState.noTouching;
            _callbakType = CallbackType.na;
            _touchSpeedVector = Vector2.zero;
            _touchSpeed = 0f;
        }

        void Update()
        {
            if (!Active)
            {
                _touchState = TouchState.inactive;
                return;
            }

            if (_handGizmo != null && _handGizmo.isActiveAndEnabled)
            {
                _handGizmo.SetScaleFactor(_handScaleFactor);
            }

#if UNITY_EDITOR || UNITY_STANDALONE

            _touchPosition = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                OnTouchStarted(ref _touchPosition);
            }
            else if (Input.GetMouseButton(0))
            {
                MoveHand(ref _touchPosition);
                _touchSpeedVector = _touchPosition - _lastTouchPosition;
                _touchSpeed = _touchSpeedVector.magnitude;

                if (_touchSpeed > _movingThreshold)
                {
                    OnTouchMoving(ref _touchPosition,ref  _touchSpeedVector);
                }
                else
                {
                    OnTouchStationary(ref _touchPosition);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {

                OnTouchEnded(ref _touchPosition, false);
            }
            else
            {
                OnNoTouching();
            }

#elif UNITY_ANDROID || UNITY_IOS

            if (Input.touchCount > 0)
            {
                _touch = Input.GetTouch(0);
                _touchPosition = _touch.position;
                if (_touch.phase == TouchPhase.Began)
                {
                    OnTouchStarted(ref _touchPosition);
                }
                else if (_touch.phase == TouchPhase.Moved)
                {
                    _touchSpeedVector = _touchPosition - _lastTouchPosition;
                    _touchSpeed = _touchSpeedVector.magnitude;
                    OnTouchMoving(ref _touchPosition, ref  _touchSpeedVector, _touchTimer);
                }
                else if (_touch.phase == TouchPhase.Stationary)
                {
                    OnTouchStationary(ref _touchPosition, _touchTimer);
                }
                else if (_touch.phase == TouchPhase.Ended)
                {
                    OnTouchEnded(ref _touchPosition, _touchTimer, false);
                }
                else if(_touch.phase == TouchPhase.Canceled)
                {
                    OnTouchEnded(ref _touchPosition, _touchTimer, true);
                }
            }
            else
            {
                OnNoTouching();
            }
#endif
            // set last pos to calculate touch speed
            //
            _lastTouchPosition = _touchPosition;

        }

        protected void Raycast()
        {
            if (_raycastable && Physics.Raycast(_camera.ScreenPointToRay(_touchPosition), out _hitInfo, 100f, _raycastLayer))
            {
                OnRaycastTriggered(ref _hitInfo);
                _colliderName = _hitInfo.collider.name;
            }
            else
            {
                _colliderName = "";
            }
        }

        public virtual void OnRaycastTriggered(ref RaycastHit hitInfo)
        {
        }

        protected void ActivateHand()
        {
            if (_debugMode && _handGizmo != null)
                _handGizmo.gameObject.SetActive(true);
        }

        protected void DeactivateHand()
        {
            if (_debugMode && _handGizmo != null)
                _handGizmo.gameObject.SetActive(false);
        }

        protected void MoveHand(ref Vector2 handPosition)
        {
            if (_debugMode && _handGizmo != null)
                _handGizmo._transform.position = handPosition;
        }

    }

}