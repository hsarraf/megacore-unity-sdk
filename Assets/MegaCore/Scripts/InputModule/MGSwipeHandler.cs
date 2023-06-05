using UnityEngine;
using UnityEngine.Events;

namespace MegaCore.InputModule
{

    public class SwipeHandler : DragHandler
    {
        public delegate void SwipeDelegate();

        // swipe up callbacks
        //
        public UnityEvent _onSwipeUp;
        public SwipeDelegate _SwipeUpDelegate;

        // swipe down callbacks
        //
        public UnityEvent _onSwipeDown;
        public SwipeDelegate _SwipeDownDelegate;

        // swipe left callbacks
        //
        public UnityEvent _onSwipeLeft;
        public SwipeDelegate _SwipeLeftDelegate;

        // swipe right callbacks
        //
        public UnityEvent _onSwipeRight;
        public SwipeDelegate _SwipeRightDelegate;


        protected override void OnDragLengthTriggered(ref Vector2 dragVelocity)
        {
            base.OnDragLengthTriggered(ref dragVelocity);

            float dragAngle = Mathf.Atan2(dragVelocity.x, dragVelocity.y) * Mathf.Rad2Deg;
            if (dragAngle > 55f && dragAngle < 145f)  // swipe right
            {
                _onSwipeRight?.Invoke();
                _SwipeRightDelegate?.Invoke();
            }
            else if (dragAngle >= -55f && dragAngle <= 55f)   // swipe up
            {
                _onSwipeUp?.Invoke();
                _SwipeUpDelegate?.Invoke();
            }
            else if (dragAngle > -145f && dragAngle < -55f)   // swipe left
            {
                _onSwipeLeft?.Invoke();
                _SwipeLeftDelegate?.Invoke();
            }
            else  // swipe down
            {
                _onSwipeDown?.Invoke();
                _SwipeDownDelegate?.Invoke();
            }
        }


    }

}