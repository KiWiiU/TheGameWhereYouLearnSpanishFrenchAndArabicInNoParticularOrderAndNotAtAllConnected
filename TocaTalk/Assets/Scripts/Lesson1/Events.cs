using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{

    public DialogueManager dialogueManager;
    public QuizManager quizManager;
    public Question[] questions;
    private int currentEvent;

    public CharacterDialogue[] secondDialogue;
    private GameObject dialogueObj;
    public void Start()
    {
        currentEvent = 0;
        dialogueManager.OnDialogueEnd += OnDialogueEnd;
        quizManager.OnQuizEnd += OnQuizEnd;
        dialogueObj = GameObject.FindWithTag("Dialogue");
    }

    private void OnQuizEnd() {
        Debug.Log("Player has finished quiz. ");
        TriggerEvent();
    }

    private void OnDialogueEnd() {
        TriggerEvent();
    }

    public void TriggerEvent() {
        switch(++currentEvent) {
            case 1 : { // Trigger first dialogue
                dialogueObj.GetComponent<DialogueTrigger>().TriggerDialogue();
                break;
            }
            case 2 : { // Trigger quiz
                quizManager.StartQuiz(questions);
                break;
            }
            case 3 : { // Trigger dialogue after quiz, ending dialogue
                dialogueObj.GetComponent<DialogueManager>().StartDialogue(secondDialogue);
                break;
            }
            case 4 : { // End lesson
                Holder.progress(1);
                GetComponent<SceneSwap>().SwapScene("Lessons");
                break;
            }
        }
    }
}
