using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{

    public DialogueManager dm;
    public QuizManager qm;

    private Queue<object> eventQueue = new();
    private int playerScore;
    private int totalScore;
    private bool currentlyOpen;
    public void Start()
    {
        currentlyOpen = false;
        playerScore = 0;
        totalScore = 0;
        dm.OnDialogueEnd += OnDialogueEnd;
        qm.OnQuizEnd += OnQuizEnd;
    }

    public void EnqueuePingPong(DialogueList[] dialogue, QuestionList[] quiz) // dialogue first
    {
        int d = 0;
        int q = 0;
        while (d < dialogue.Length || q < quiz.Length)
        {
            if (d < dialogue.Length)
            {
                EnqueueDialogue(dialogue[d++]);
            }
            if (q < quiz.Length)
            {
                EnqueueQuiz(quiz[q++]);
            }
        }
    }

    public void EnqueuePingPong(QuestionList[] quiz, DialogueList[] dialogue) // quiz first
    {
        int d = 0;
        int q = 0;
        while (d < dialogue.Length || q < quiz.Length)
        {
            if (q < quiz.Length)
            {
                EnqueueQuiz(quiz[q++]);
            }
            if (d < dialogue.Length)
            {
                EnqueueDialogue(dialogue[d++]);
            }
        }
    }

    public void EnqueueDialogue(DialogueList dialogue)
    {
        eventQueue.Enqueue(dialogue);
    }

    public void EnqueueQuiz(QuestionList quiz)
    {
        eventQueue.Enqueue(quiz);
    }

    public void EnqueueEndLesson()
    {
        eventQueue.Enqueue(new EndLesson());
    }

    public IEnumerator NextEvent()
    {
        if (currentlyOpen)
        {
            yield return new WaitUntil(() => !currentlyOpen); // wait until its not currently open
            yield return new WaitForSeconds(1f); // wait a little bit after ready to make it seem less rigid
        }
        if (eventQueue.Count == 0)
        {
            yield break;
        }

        currentlyOpen = true;
        object newEvent = eventQueue.Dequeue();
        if (newEvent is DialogueList dialogue)
        {
            dm.StartDialogue(dialogue);
        }
        else if (newEvent is QuestionList quiz)
        {
            qm.StartQuiz(quiz);
        }
        else if (newEvent is EndLesson)
        {
            StartCoroutine(EndLesson());
        }
        currentlyOpen = false;
    }


    public void Destroy()
    {
        dm.OnDialogueEnd -= OnDialogueEnd;
        qm.OnQuizEnd -= OnQuizEnd;
    }

    private void OnQuizEnd()
    {
        playerScore += qm.NumCorrect;
        totalScore += qm.NumQuestions;
        StartCoroutine(NextEvent());
    }

    private void OnDialogueEnd()
    {
        StartCoroutine(NextEvent());
    }

    private IEnumerator EndLesson()
    {
        yield return new WaitForSeconds(2f);
        if ((double)playerScore / totalScore >= .8)
            Holder.progress(1);
        else
            Debug.Log("Player has failed the lesson.");
        GetComponent<SceneSwap>().SwapScene("Lessons");
    }

}

public class EndLesson
{
    // literally just a marker lmao
}
