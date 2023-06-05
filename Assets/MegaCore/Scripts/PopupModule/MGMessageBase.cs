using UnityEngine;

namespace MegaCore.Popup
{
    public class MGMessageBase : MonoBehaviour
    {
        public enum MsgType
        {
            info, warning, error
        }

        public delegate void MessageDelegate();

        public delegate void DialogDelegate();
        public delegate void Dialog2Delegate(string text);

        public delegate void PromptDelegate(string text);

    }

}
