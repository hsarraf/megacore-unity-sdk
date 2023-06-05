using UnityEngine;

namespace MegaCore.InputModule
{

    public interface ITouchMoving
    {
        void OnTouchMoving(ref Vector2 touchPosition, ref Vector2 dragSpeed);
    }

}