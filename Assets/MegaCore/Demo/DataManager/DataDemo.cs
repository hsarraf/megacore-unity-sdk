using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MegaCore.DataModule;

public class DataDemo : MonoBehaviour
{
    void Start()
    {
        MGDataBehaviour.Instance.GData.gems++;
        MGDataBehaviour.Instance.Save();
    }
}
