using System.Collections;
using UnityEngine;

using MegaCore.PoolingModule;


public class Spawner : MonoBehaviour
{

    public int _count = 30;

    IEnumerator Start()
    {
        for (int i = 0; i < _count; i++)
        {
            MGPoolBehaviour.Instance.Spawn(MGElement.Type.Element1);
            yield return new WaitForSeconds(0.1f);
            MGPoolBehaviour.Instance.Spawn(MGElement.Type.Element2);
            yield return new WaitForSeconds(0.1f);
        }
    }

}
