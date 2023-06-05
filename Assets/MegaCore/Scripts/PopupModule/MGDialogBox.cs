using System;

namespace MegaCore.Popup
{

    public class MGDialogBox : MGMessageBase
    {

        public event Action _okAction;

        public DialogDelegate _okDelegate;

        private void OnEnable()
        {
            _okDelegate = new DialogDelegate(_okAction);
        }

        public void Show(string message, MsgType msgType, bool lockBackground, Action okAction)
        {
            _okAction = okAction;
        }

        public void Hide()
        {
        }

    }

}
