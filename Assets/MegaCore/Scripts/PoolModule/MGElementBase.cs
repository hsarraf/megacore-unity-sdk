using System.Collections;
using UnityEngine;

namespace MegaCore.PoolingModule
{

    public class MGElementBase : MonoBehaviour, IResetable
    {
        public virtual void Kill(float timeToKill = 0f)
        {
            if (timeToKill == 0f)
                ResetElement();
            else
                StartCoroutine(KillCo(timeToKill));
        }

        IEnumerator KillCo(float timeToKill)
        {
            yield return new WaitForSeconds(timeToKill);
            ResetElement();
        }

        /// <summary>
        /// override to reset all the parama befre being repooled
        /// </summary>
        public virtual void ResetElement()
        {
            MGPoolBehaviour.Instance.Repool(this as MGElement);
        }

    }

}