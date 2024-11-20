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
        Trigger();
    }

    private void OnDialogueEnd() {
        Trigger();
    }

    public void Trigger() {
        switch(++currentEvent) {
            case 1 : {
                dialogueObj.GetComponent<DialogueTrigger>().TriggerDialogue();
                break;
            }
            case 2 : {
                quizManager.StartQuiz(questions);
                break;
            }
            case 3 : {
                dialogueObj.GetComponent<DialogueManager>().StartDialogue(secondDialogue);
                break;
            }
            case 4 : {
                Holder.progress(1);
                GetComponent<SceneSwap>().SwapScene("Lessons");
                break;
            }
        }
    }
}
