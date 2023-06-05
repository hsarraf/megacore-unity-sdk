using System;


namespace MegaCore.Popup
{

    public class MGPopupBehaviour : MGAbstract
    {

        private static MGPopupBehaviour __instance;
        public static MGPopupBehaviour Instance { get { return __instance; } }
        void Awake()
        {
            if (__instance == null)
            {
                __instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (__instance != this)
            {
                Destroy(gameObject);
            }
        }

        public MGMessageBox _messageBox;
        public MGDialogBox _dialogBox;
        public MGPromptBox _promptBox;
        public MGGDRPConsent _GDRPConsent;


        public void ShowMessageBox(string message, Action tapAction = null)
        {
            _messageBox.Show(message, tapAction);
        }

        public void ShowDialogBox(string message, Action okAction)
        {
            //_dialogBox.Show(message, tapAction);
        }

        public void ShowPromptBox(string message, Action okAction)
        {
            //_promptBox.Show(message, tapAction);
        }

        public void ShowGDRPConsent(string message, Action okAction)
        {
            //_promptBox.Show(message, okAction);
        }

    }

}
