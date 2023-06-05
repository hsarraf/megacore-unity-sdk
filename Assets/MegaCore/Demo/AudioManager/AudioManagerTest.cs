using UnityEngine;

using MegaCore.AudioManager;

public class AudioManagerTest : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            MGAudioBehaviour.Instance.Play(LongClip.ac_music1_4502);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            MGAudioBehaviour.Instance.Play(MediumClip.ac_phone_ring_2_5505);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MGAudioBehaviour.Instance.Play(ShortClip.ac_coin_1_3_1739);
        }
    }

}
