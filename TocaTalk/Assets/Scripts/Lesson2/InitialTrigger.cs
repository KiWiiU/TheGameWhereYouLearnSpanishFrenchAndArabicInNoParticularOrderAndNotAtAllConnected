using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialTrigger : MonoBehaviour
{
    Events eventManager;
    public GameObject doraNPC, player, newBG, bounds, miguelNPC, antoniaNPC, mayaNPC, diegoNPC;
    public GameObject MirrorDora, MirrorPlayer, MirrorMiguel, MirrorAntonia, MirrorMaya, MirrorDiego;
    public DialogueList[] dialogue1, dialogue2, dialogue3, dialogue4;
    public DialogueList WinDialogue, LoseDialogue;
    public QuestionList[] question1, question2, question3, question4;
    public Transform doraDestination;
    private int currentEvent = 0;

    public void Start()
    {
        eventManager = GameObject.FindWithTag("GameController").GetComponent<Events>();
        eventManager.OnEventsEnd += OnEventsEnd;
        newBG.SetActive(false);
        bounds.SetActive(true);
    }

    public void OnEventsEnd()
    {
        // eventManager.OnEventsEnd -= OnEventsEnd;
        currentEvent++;
        if (currentEvent == 1)
        {
            StartCoroutine(MoveDora());
        }
        if (currentEvent == 2)
        {
            // transition to the gameshow
            newBG.SetActive(true);
            Holder.canPlayerMove = false;
            bounds.SetActive(false);
            player.transform.position = MirrorPlayer.transform.position;
            doraNPC.transform.position = MirrorDora.transform.position;
            miguelNPC.transform.position = MirrorMiguel.transform.position;
            antoniaNPC.transform.position = MirrorAntonia.transform.position;
            mayaNPC.transform.position = MirrorMaya.transform.position;
            diegoNPC.transform.position = MirrorDiego.transform.position;
        
            eventManager.EnqueuePingPong(dialogue3, question3);
            StartCoroutine(eventManager.NextEvent());
        }
        if (currentEvent == 3)
        {
            // lighting changes for later on
            eventManager.EnqueuePingPong(question4, dialogue4);
            StartCoroutine(eventManager.NextEvent());
        }
        if (currentEvent == 4)
        {
            if (eventManager.returnScore() >= 0.8)
            {
                eventManager.EnqueueDialogue(WinDialogue);
                StartCoroutine(eventManager.NextEvent());
            } else
            {
                eventManager.EnqueueDialogue(LoseDialogue);
                StartCoroutine(eventManager.NextEvent());
            }
        }
        if (currentEvent == 5)
        {
            eventManager.EnqueueEndLesson();
            StartCoroutine(eventManager.NextEvent());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            Destroy(GetComponent<BoxCollider2D>());
            eventManager.EnqueuePingPong(dialogue1, question1);
            StartCoroutine(eventManager.NextEvent());
            // Dora walks up to the characters
        }
    }

    public IEnumerator MoveDora()
    {
        Holder.canPlayerMove = false;
        yield return StartCoroutine(doraNPC.GetComponent<NPC>().MoveTo(doraDestination.position, 5f));
        Holder.canPlayerMove = true;
        eventManager.EnqueuePingPong(question2, dialogue2);
        StartCoroutine(eventManager.NextEvent());
    }
}
