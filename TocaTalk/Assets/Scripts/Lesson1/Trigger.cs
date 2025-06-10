using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public DialogueList[] dialogues;
    public QuestionList[] questions;
    public GameObject luisNPC;
    public GameObject cousinNPC;
    Events eventManager;
    // When player walks into trigger, it triggers the events of the level

    public void Start()
    {
        eventManager = GameObject.FindWithTag("GameController").GetComponent<Events>();
        eventManager.OnEventsEnd += OnEventsEnd;
    }

    public void OnEventsEnd()
    {
        eventManager.OnEventsEnd -= OnEventsEnd;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(HandleTrigger());
        }
    }

    public IEnumerator HandleTrigger()
    {
        luisNPC.SetActive(true);
        cousinNPC.SetActive(true);
        eventManager.EnqueuePingPong(questions, dialogues); // questions first because its put in first
        yield return new WaitForSeconds(2f);
        StartCoroutine(eventManager.NextEvent());
        Destroy(this);
    }
}
