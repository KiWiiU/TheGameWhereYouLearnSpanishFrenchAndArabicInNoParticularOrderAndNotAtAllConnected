using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    public DialogueList[] dialogues;
    Events eventManager;
    public void Start()
    {
        eventManager = GameObject.FindWithTag("GameController").GetComponent<Events>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            double pct = (double)eventManager.playerScore / eventManager.totalScore;
            if (pct <= .8)
            {
                eventManager.EnqueueDialogue(dialogues[0]);
            }
            else if (pct <= 1)
            {
                eventManager.EnqueueDialogue(dialogues[1]);
            }
            else
            {
                eventManager.EnqueueDialogue(dialogues[2]);
            }
        }
        eventManager.EnqueueEndLesson();
        StartCoroutine(eventManager.NextEvent());
    }
}
