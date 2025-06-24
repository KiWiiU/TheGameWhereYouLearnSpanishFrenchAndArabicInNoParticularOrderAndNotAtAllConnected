using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music instance = null;
    public static Music Instance { get { return instance; } }

    public AudioClip audioClip;

    void Awake()
    {
        gameObject.GetComponent<AudioSource>().volume = Holder.volume;
        if (instance == null)
        {
            playClip();
        }
        if(instance != null && instance != this) {
            if(audioClip != instance.audioClip) {
                Destroy(instance.gameObject);
                playClip();
            }
            else {
                Destroy(this.gameObject);
                return;
            }
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        gameObject.GetComponent<AudioSource>().volume = Holder.volume;
    }
    public void playClip() {
        instance = this;
        GetComponent<AudioSource>().clip = audioClip;
        GetComponent<AudioSource>().Play();
    }
}
