using UnityEngine;

namespace MegaCore.InputModule
{

    public interface ITouchStarted
    {
        void OnTouchStarted(ref Vector2 touchPosition);
    }

}