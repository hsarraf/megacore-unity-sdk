using UnityEngine;

namespace MegaCore.InputModule
{

    public interface ITouchHolding
    {
        void OnTouchHolding(ref Vector2 touchPositionm, float duration);
    }

}