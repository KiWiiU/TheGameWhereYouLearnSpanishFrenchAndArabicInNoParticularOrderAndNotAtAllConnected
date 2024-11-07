using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{

    public DialogueManager dialogueManager;
    public QuizManager quizManager;
    public Question question;
    void Start()
    {
        dialogueManager.OnDialogueEnd += OnDialogueEnd;
    }

    private void OnDialogueEnd() {
        quizManager.StartQuiz(question);
    }
}
