using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{

    public DialogueManager dm;
    public QuizManager quizManager;
    public DialogueList[] dialogue;
    public Question[] test;
    // private int currentEvent;
    // private GameObject dialogueObj;
    // private int currentEvent;
    private int playerScore;
    private int totalScore;

    private int q, d;
    public void Start()
    {
        q = 0;
        d = 0;
        playerScore = 0;
        totalScore = 0;
        // currentEvent = 0;
        dm.OnDialogueEnd += OnDialogueEnd;
        quizManager.OnQuizEnd += OnQuizEnd;
        // dialogueObj = GameObject.FindWithTag("Dialogue");
    }

    private void OnQuizEnd()
    {
        playerScore += quizManager.NumCorrect;
        totalScore += quizManager.NumQuestions;
        NextDialogue();
    }

    public void NextDialogue()
    {
        if (d >= dialogue.Length)
        {
            EndLesson();
            return;
        }
        dm.StartDialogue(dialogue[d..(d + 1)]);
        d++;
    }

    public void NextQuiz()
    {
        if (q >= test.Length)
        {
            EndLesson();
            return;
        }
        quizManager.StartQuiz(test[q..(q + 1)]);
        q++;
    }
    private void OnDialogueEnd()
    {
        NextQuiz();
    }

    private void EndLesson()
    {
        if((double)playerScore/totalScore >= .8)
            Holder.progress(1);
        else
            Debug.Log("Player has failed the lesson.");
            GetComponent<SceneSwap>().SwapScene("Lessons");
    }


    // public void doThing(bool mode)
    // {
    //     int q = 0, d = 0;
    //     if (mode)
    //         while (true)
    //         {
    //             if (q >= test.Length && d >= firstDialogue.Length)
    //                 break;
    //             if (d < firstDialogue.Length)
    //                 dm.StartDialogue(firstDialogue[d..(d + 1)]);
    //             if (q < test.Length)
    //                 quizManager.StartQuiz(test[q..(q + 1)]);
    //             playerScore += quizManager.NumCorrect;
    //             totalScore += quizManager.NumQuestions;
    //             q++;
    //             d++;
    //         }
    //     else
    //     {
    //         while (true)
    //         {
    //             if (q >= test.Length && d >= firstDialogue.Length)
    //                 break;
    //             if (q < test.Length)
    //                 quizManager.StartQuiz(test[q..(q + 1)]);
    //             if (d < firstDialogue.Length)
    //                 dm.StartDialogue(firstDialogue[d..(d + 1)]);
    //             playerScore += quizManager.NumCorrect;
    //             totalScore += quizManager.NumQuestions;
    //             q++;
    //             d++;
    //         }
    //     }
    // }

    /*

    

    
    */

}
