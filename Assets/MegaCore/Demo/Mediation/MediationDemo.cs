using UnityEngine;

using MegaCore.MediationService;

public class MediationDemo : MonoBehaviour
{
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width * 0.2f, Screen.height * 0.7f, Screen.width * 0.8f, Screen.height));
        GUILayout.BeginVertical();
        if (GUILayout.Button("Show Interstitial", GUILayout.Width(0.5f * Screen.width), GUILayout.Height(0.07f * Screen.height)))
        {
            MGMediationBehaviour.Instance.ShowInterstitial();
        }
        else if (GUILayout.Button("Show Rewarded", GUILayout.Width(0.5f * Screen.width), GUILayout.Height(0.07f * Screen.height)))
        {
            MGMediationBehaviour.Instance.ShowRewarded();
        }
        else if (GUILayout.Button("Show Banner", GUILayout.Width(0.5f * Screen.width), GUILayout.Height(0.07f * Screen.height)))
        {
            MGMediationBehaviour.Instance.ShowBanner();
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

}
