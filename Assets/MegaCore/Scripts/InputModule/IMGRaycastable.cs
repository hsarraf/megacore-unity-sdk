using UnityEngine;

namespace MegaCore.InputModule
{

    public interface IRaycastable
    {
        void OnRaycastTriggered(ref RaycastHit hitInfo);
    }

}