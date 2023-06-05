using UnityEngine;


namespace MegaCore.InputModule
{
    public class MGTouchBehaviour : MGAbstract
    {

        private static MGTouchBehaviour _instance;
        public static MGTouchBehaviour Instance { get { return _instance; } }
        void Awake()
        {
            if (_instance == null)
                _instance = this;
        }

        public OneTapHandler _oneTapHandler;
        public TapAndHoldHandler _tapAndHoldHandler;
        public DragHandler _dragHandler;
        public SwipeHandler _swipeHandler;
    }

}