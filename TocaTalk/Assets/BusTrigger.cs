using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BusTrigger : MonoBehaviour
{
    Events eventManager;
    public DialogueList d1;
    public DialogueList[] d2;
    public QuestionList[] q;
    public GameObject player, cortez, outBounds, bus, outside;
    private int eventNum = 0;

    void Start()
    {
        eventManager = GameObject.FindWithTag("GameController").GetComponent<Events>();
        eventManager.OnEventsEnd += OnEventsEnd;
    }

    public void OnEventsEnd()
    {
        eventNum++;
        if (eventNum == 1)
        {
            Holder.canPlayerMove = false;
            while (player.transform.position != new Vector3(5.55999994f, -3.4000001f, 0f))
            {
                player.transform.position = Vector2.MoveTowards(player.transform.position, new Vector2(5.55999994f, -3.4000001f), 5f * Time.deltaTime);
            }
            eventManager.EnqueuePingPong(d2, q);
            StartCoroutine(eventManager.NextEvent());
        }
        if (eventNum == 2)
        {
            eventManager.EnqueueEndLesson();
            StartCoroutine(eventManager.NextEvent());
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            bus.SetActive(false);
            outBounds.SetActive(false);
            cortez.SetActive(true);
            outside.SetActive(false);
            player.transform.localScale = new Vector3(14.124505f, 14.124505f, 14.124505f);
            player.transform.position = new Vector3(0.949999988f, -3.11999989f, 0f);
            eventManager.EnqueueDialogue(d1);
            StartCoroutine(eventManager.NextEvent());
        }
    }
}
