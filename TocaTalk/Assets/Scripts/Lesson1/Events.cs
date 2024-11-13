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
    }

    private void OnDialogueEnd() {
        if(!startQuiz) {
            startQuiz = true;
            quizManager.StartQuiz(questions);
        }
    }
}
