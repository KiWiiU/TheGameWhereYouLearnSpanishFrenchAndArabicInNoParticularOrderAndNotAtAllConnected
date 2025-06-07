using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public DialogueList[] dialogues;
    public QuestionList[] questions;
    public GameObject npc;
    Events eventManager;
    // When player walks into trigger, it triggers the events of the level

    public void Start()
    {
        eventManager = GameObject.FindWithTag("GameController").GetComponent<Events>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            npc.SetActive(true);
            eventManager.EnqueuePingPong(dialogues, questions); // dialogue first because its put in first
            eventManager.EnqueueEndLesson();
            StartCoroutine(eventManager.NextEvent());
            Destroy(this);
        }
    }
}
