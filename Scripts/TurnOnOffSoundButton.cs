using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnOffSoundButton : MonoBehaviour
{
    [SerializeField] private GameObject musicOff;

    private bool turned = true;

    private float soundVolume = 1;

    private void Start()
    {
        soundVolume = PlayerPrefs.GetFloat("Sound", soundVolume);
        ChekIsSoundTurned();
    }

    private void ChekIsSoundTurned()
    {
        if(soundVolume == 0)
        {
            musicOff.SetActive(true);
            AudioListener.volume = 0;
            turned = false;
        }
        else
        {
            musicOff.SetActive(false);
            AudioListener.volume = 1;
            turned = true;
        }
    }

    public void TurnOnOffSound()
    {
        if (turned)
        {
            AudioListener.volume = 0;
            soundVolume = 0;
            turned = !turned;
        }
        else
        {
            AudioListener.volume = 1;
            soundVolume = 1;
            turned = !turned;
        }
        ChekIsSoundTurned();
        PlayerPrefs.SetFloat("Sound", soundVolume);
        Debug.Log(soundVolume);

    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("Sound", soundVolume);
    }
}
