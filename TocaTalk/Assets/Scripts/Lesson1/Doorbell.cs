using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorbell : MonoBehaviour
{
    public DialogueList dialogue;
    public BoxCollider2D trigger;

    private AudioSource sound;
    private Events eventManager;
    public void Start()
    {
        eventManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<Events>();
        sound = GetComponent<AudioSource>();
        sound.volume = Holder.volume;
        StartCoroutine(Ring());
    }
    public IEnumerator Ring()
    {
        yield return new WaitForSeconds(10f);
        AudioSource music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        music.Pause();
        sound.Play();
        eventManager.EnqueueDialogue(dialogue);
        StartCoroutine(eventManager.NextEvent());
        yield return new WaitForSeconds(sound.clip.length + 0.5f);
        music.Play();
        trigger.enabled = true;
        Destroy(this);
    }
}
