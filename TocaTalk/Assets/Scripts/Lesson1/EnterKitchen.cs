using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterKitchen : MonoBehaviour
{
    public DialogueList[] dialogues1, dialogues2;
    public QuestionList[] questions1, questions2;
    public GameObject cousinNPC;
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            eventManager.EnqueuePingPong(dialogues1, questions1);
            StartCoroutine(eventManager.NextEvent());
        }
        // move cake and cousin to table
        eventManager.EnqueuePingPong(dialogues2, questions2);
        StartCoroutine(eventManager.NextEvent());
        // end level??
        Destroy(this);
    }

}
