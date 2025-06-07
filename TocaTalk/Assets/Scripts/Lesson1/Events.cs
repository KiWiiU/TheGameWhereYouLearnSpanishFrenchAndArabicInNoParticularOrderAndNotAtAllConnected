using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{

    public DialogueManager dm;
    public QuizManager quizManager;
    public DialogueList[] dialogue;
    public QuestionList[] test;
    private int playerScore;
    private int totalScore;

    private int q, d;
    public void Start()
    {
        q = 0;
        d = 0;
        playerScore = 0;
        totalScore = 0;
        dm.OnDialogueEnd += OnDialogueEnd;
        quizManager.OnQuizEnd += OnQuizEnd;
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

}
