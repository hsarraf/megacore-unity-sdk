using UnityEngine;

namespace MegaCore.InputModule
{

    public interface ITouchEnded
    {
        void OnTouchEnded(ref Vector2 touchPosition, bool hasEnded);
    }

}