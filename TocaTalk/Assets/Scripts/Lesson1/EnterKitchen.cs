using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterKitchen : MonoBehaviour
{
    public DialogueList[] dialogues1, dialogues2;
    public QuestionList[] questions1, questions2;
    public GameObject cousinNPC, player;
    Events eventManager;

    public void Start()
    {
        eventManager = GameObject.FindWithTag("GameController").GetComponent<Events>();
        eventManager.OnEventsEnd += OnEventsEnd;
    }

    public void OnEventsEnd()
    {
        eventManager.OnEventsEnd -= OnEventsEnd;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            eventManager.EnqueuePingPong(dialogues1, questions1);
            StartCoroutine(eventManager.NextEvent());
            // move cake and cousin to table
            eventManager.EnqueuePingPong(dialogues2, questions2);
            eventManager.EnqueueEndLesson(); // end level
            StartCoroutine(eventManager.NextEvent());
            Destroy(this);
        }
    }

}
