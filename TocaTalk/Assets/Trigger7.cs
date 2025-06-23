using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger7 : MonoBehaviour
{
    Events eventManager;
    public DialogueList[] d1, d2;
    public DialogueList d3, d4;
    public QuestionList[] q1, q2;
    public GameObject bounds, bg, jesus, player, eBound;
    public AudioSource a;
    private int eventNum = 0;

    void Start()
    {
        eventManager = GameObject.FindWithTag("GameController").GetComponent<Events>();
        eventManager.OnEventsEnd += OnEventsEnd;
        a = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            print("hi");
            StartCoroutine(First());
        }
    }

    public void OnEventsEnd()
    {
        eventNum++;
        switch (eventNum) {
            case 1:
                StartCoroutine(Second());
                break;
            case 2:
                StartCoroutine(Third()); 
                break;
            case 3:
                StartCoroutine(Fourth());
                break;
            case 4:
                Fifth();
                break;
            default:
                break;
        }
    }

    IEnumerator First()
    {
        bg.SetActive(false);
        jesus.SetActive(true);
        bounds.SetActive(false);
        player.transform.localScale = new Vector3(16.8357658f, 16.8357658f, 16.8357658f);
        player.transform.position = new Vector3(-3.1500001f, -7.46000004f, 0f);
        eBound.SetActive(true);
        yield return new WaitForSeconds(2);
        eventManager.EnqueuePingPong(q1, d1);
        StartCoroutine(eventManager.NextEvent());
    }

    IEnumerator Second()
    {
        yield return new WaitForSeconds(3);
        eventManager.EnqueuePingPong(q2, d2);
        StartCoroutine(eventManager.NextEvent());
    }

    IEnumerator Third()
    {
        yield return new WaitForSeconds(1);
        eventManager.EnqueueDialogue(d3);
        StartCoroutine(eventManager.NextEvent());
    }

    IEnumerator Fourth()
    {
        a.Play();
        yield return new WaitForSeconds(1);
        eventManager.EnqueueDialogue(d4);
        StartCoroutine(eventManager.NextEvent());
    }

    void Fifth()
    {
        eventManager.EnqueueEndLesson();
        StartCoroutine(eventManager.NextEvent());
    }
}
