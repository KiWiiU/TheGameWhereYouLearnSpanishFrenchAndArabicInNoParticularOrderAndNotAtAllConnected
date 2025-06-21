using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomTrigger : MonoBehaviour
{
    Events eventManager;
    public DialogueList[] dialogues;
    public QuestionList[] questions;

    public DialogueList afterClosed;
    public GroceryList groceryList;
    public GameObject marketplaceTrigger;
    bool triggered;
    void Start()
    {
        triggered = false;
        eventManager = GameObject.FindWithTag("GameController").GetComponent<Events>();
        groceryList.OnClosed += OnGroceryClosed;
    }

    private void OnEventsEnd()
    {
        Holder.groceryList = true; // player can now open the grocery list
        groceryList.OpenGroceryList();
        eventManager.OnEventsEnd -= OnEventsEnd;
    }

    private void OnGroceryClosed()
    {
        eventManager.EnqueueDialogue(afterClosed);
        StartCoroutine(eventManager.NextEvent());
        marketplaceTrigger.SetActive(true);
        groceryList.OnClosed -= OnGroceryClosed;
        Destroy(this);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!triggered)
            {
                eventManager.OnEventsEnd += OnEventsEnd;
                triggered = true;
                StartCoroutine(HandleTrigger());
            }
        }
    }

    IEnumerator HandleTrigger()
    {
        yield return null;
        eventManager.EnqueuePingPong(questions, dialogues);
        StartCoroutine(eventManager.NextEvent());
    }
}
