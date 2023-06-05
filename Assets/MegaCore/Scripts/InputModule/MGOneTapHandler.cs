using UnityEngine;
using UnityEngine.Events;

namespace MegaCore.InputModule
{

    public class OneTapHandler : InputTouch, ITouchStarted, ITouchEnded
    {
        // start tap event and delegate
        //
        public UnityEvent<Vector2> _onTapped;
        public delegate void TapDelegate(ref Vector2 touchPosition);
        public TapDelegate _tapDelegate;

        // end tap event and delegate
        //
        public UnityEvent<Vector2, bool> _onUntapped;
        public delegate void UntapDelegate(ref Vector2 touchPosition, bool hasCanceled);
        public UntapDelegate _untapDelegate;

        public override void OnTouchStarted(ref Vector2 touchPosition)
        {
            _onTapped?.Invoke(touchPosition);
            _tapDelegate?.Invoke(ref touchPosition);
            base.OnTouchStarted(ref touchPosition);
        }

        public override void OnTouchEnded(ref Vector2 touchPosition, bool hasCanceled)
        {
            _onUntapped?.Invoke(touchPosition, hasCanceled);
            _untapDelegate?.Invoke(ref touchPosition, hasCanceled);
            base.OnTouchEnded(ref touchPosition, hasCanceled);
        }

    }

}
