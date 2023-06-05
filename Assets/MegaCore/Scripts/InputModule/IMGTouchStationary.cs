using UnityEngine;

namespace MegaCore.InputModule
{

    public interface ITouchStationary
    {
        void OnTouchStationary(ref Vector2 touchPosition);
    }

}