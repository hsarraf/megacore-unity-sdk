using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MegaCore.Popup;

public class MGMessagingTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            MGPopupBehaviour.Instance.ShowMessageBox("First Message", () => Debug.Log("message box tapped"));
        }
    }

}
