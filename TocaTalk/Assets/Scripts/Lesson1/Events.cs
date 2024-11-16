using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{

    public DialogueManager dialogueManager;
    public QuizManager quizManager;
    public Question[] questions;
    private bool startQuiz;
    void Start()
    {
        startQuiz = false;
        dialogueManager.OnDialogueEnd += OnDialogueEnd;
        quizManager.OnQuizEnd += OnQuizEnd;
    }

    private void OnQuizEnd() {
        Holder.progress(1);
        gameObject.GetComponent<SceneSwap>().SwapScene("Lessons");
    }

    private void OnDialogueEnd() {
        if(!startQuiz) {
            startQuiz = true;
            quizManager.StartQuiz(questions);
        }
    }
}
