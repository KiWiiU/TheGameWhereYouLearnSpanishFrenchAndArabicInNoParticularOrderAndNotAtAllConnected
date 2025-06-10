using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public DialogueList[] theGoodDialogues, dialogues2, dialogues3;
    public QuestionList[] theFirstQuestions, questions2, questions3;
    public GameObject luisNPC, cousinNPC, auntNPC, uncleNPC;

    public Transform luisDestination, auntDestination, uncleDestination;
    private bool triggered = false;
    Events eventManager;

    public BoxCollider2D kitchenEvent;
    private int currentEvent = 0;
    // When player walks into trigger, it triggers the events of the level

    public void Start()
    {
        eventManager = GameObject.FindWithTag("GameController").GetComponent<Events>();
    }

    public void OnEventsEnd()
    {
        if (currentEvent == 0) {
            StartCoroutine(MoveLuis());
            currentEvent++;
        } else if (currentEvent == 1) {
            StartCoroutine(MoveTios());
            currentEvent++;
            eventManager.OnEventsEnd -= OnEventsEnd;
        }
        
    }
    public IEnumerator MoveTios() {
        StartCoroutine(auntNPC.GetComponent<NPC>().MoveTo(auntDestination.position, 5f));
        yield return StartCoroutine(uncleNPC.GetComponent<NPC>().MoveTo(uncleDestination.position, 5f));
        eventManager.EnqueuePingPong(questions3, dialogues3);
        StartCoroutine(eventManager.NextEvent());
        kitchenEvent.enabled = true;
        Destroy(this);
    }
    public IEnumerator MoveLuis() {
        Holder.canPlayerMove = false;
        yield return StartCoroutine(cousinNPC.GetComponent<NPC>().MoveTo(luisDestination.position, 5f));
        Holder.canPlayerMove = true;
        eventManager.EnqueuePingPong(dialogues2, questions2);
        StartCoroutine(eventManager.NextEvent());
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!triggered)
            {
                triggered = true;
                StartCoroutine(HandleTrigger());
            }
        }
    }

    public IEnumerator HandleTrigger()
    {
        luisNPC.SetActive(true);
        cousinNPC.SetActive(true);
        eventManager.EnqueuePingPong(theFirstQuestions, theGoodDialogues); // questions first because its put in first
        yield return new WaitForSeconds(2f);
        StartCoroutine(eventManager.NextEvent());
        eventManager.OnEventsEnd += OnEventsEnd;
    }
}
