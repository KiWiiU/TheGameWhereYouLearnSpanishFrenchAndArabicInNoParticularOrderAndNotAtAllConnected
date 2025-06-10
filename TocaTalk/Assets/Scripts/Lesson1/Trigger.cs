using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public DialogueList[] theGoodDialogues, dialogues2, dialogues3;
    public QuestionList[] theFirstQuestions, questions2, questions3;
    public GameObject luisNPC, cousinNPC, kitchenEvent;
    Events eventManager;
    // When player walks into trigger, it triggers the events of the level

    public void Start()
    {
        kitchenEvent.SetActive(false);
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
        eventManager.EnqueuePingPong(theFirstQuestions, theGoodDialogues); // questions first because its put in first
        yield return new WaitForSeconds(2f);
        StartCoroutine(eventManager.NextEvent());
        // Juan moves
        eventManager.EnqueuePingPong(dialogues2, questions2);
        StartCoroutine(eventManager.NextEvent());
        // aunts and uncles appear
        eventManager.EnqueuePingPong(questions3, dialogues3);
        StartCoroutine(eventManager.NextEvent());
        kitchenEvent.SetActive(true);
        Destroy(this);
    }
}
