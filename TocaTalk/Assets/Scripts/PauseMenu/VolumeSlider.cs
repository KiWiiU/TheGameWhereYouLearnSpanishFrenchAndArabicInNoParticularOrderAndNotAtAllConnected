using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    public void changeVolume(float volume)
    {
        Holder.volume = volume;
        foreach (AudioSource a in FindObjectsOfType<AudioSource>())
        {
            a.volume = volume;
        }
    }
}
