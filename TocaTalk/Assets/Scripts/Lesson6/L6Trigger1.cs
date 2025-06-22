using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L6Trigger1 : MonoBehaviour
{
    Events eventManager;
    private int eventNum = 0;
    public DialogueList[] d1, d2;
    public QuestionList[] q1, q2;
    public GameObject bedBounds, houseBounds, mom, dad, player, bedroom, house, bus, outBounds, busTrig;
    public GameObject mom1, dad1;

    public void Start()
    {
        bedBounds.SetActive(true);
        eventManager = GameObject.FindWithTag("GameController").GetComponent<Events>();
        eventManager.OnEventsEnd += OnEventsEnd;
        eventManager.EnqueuePingPong(d1, q1);
        StartCoroutine(eventManager.NextEvent());
    }

    public void OnEventsEnd()
    {
        eventNum++;
        if (eventNum == 1)
        {
            bedroom.SetActive(false);
            bedBounds.SetActive(false);
            houseBounds.SetActive(true);
            mom.SetActive(false);
            dad.SetActive(false);
            mom.transform.position = mom1.transform.position;
            dad.transform.position = dad1.transform.position;
            mom.SetActive(true);
            dad.SetActive(true);
            player.transform.position = new Vector3(9.34000015f, -0.100000001f, 0f);
            eventManager.EnqueuePingPong(q2, d2);
            StartCoroutine(eventManager.NextEvent());
        }
        if (eventNum == 2)
        {
            houseBounds.SetActive(false);
            bus.SetActive(true);
            house.SetActive(false);
            outBounds.SetActive(true);
            player.transform.position = new Vector3(7.88999987f, -4.67999983f, 0f);
            busTrig.SetActive(true);
            mom.SetActive(false);
            dad.SetActive(false);
        }
    }
}
