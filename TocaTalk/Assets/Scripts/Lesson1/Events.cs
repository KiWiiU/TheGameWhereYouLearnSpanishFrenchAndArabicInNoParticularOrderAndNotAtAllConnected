using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{

    public DialogueManager dialogueManager;
    public QuizManager quizManager;
    public DialogueList[] firstDialogue;
    public Question[] test;
    private int currentEvent;

    public DialogueList[] secondDialogue;
    private GameObject dialogueObj;

    private int playerScore;
    private int totalScore;
    public void Start()
    {
        playerScore = 0;
        totalScore = 0;
        currentEvent = 0;
        dialogueManager.OnDialogueEnd += OnDialogueEnd;
        quizManager.OnQuizEnd += OnQuizEnd;
        dialogueObj = GameObject.FindWithTag("Dialogue");
    }

    private void OnQuizEnd() {
        playerScore += quizManager.NumCorrect;
        totalScore += quizManager.NumQuestions;
        TriggerEvent();
    }

    private void OnDialogueEnd() {
        TriggerEvent();
    }

    public void TriggerEvent() {
        switch(++currentEvent) {
            case 1 : { // Trigger first dialogue
                dialogueObj.GetComponent<DialogueManager>().StartDialogue(firstDialogue);
                break;
            }
            case 2 : { // Trigger quiz
                quizManager.StartQuiz(test);
                break;
            }
            case 3 : { // Trigger dialogue after quiz, ending dialogue
                dialogueObj.GetComponent<DialogueManager>().StartDialogue(secondDialogue);
                break;
            }
            case 4 : { // End lesson
                if((double)playerScore/totalScore >= .8)
                    Holder.progress(1);
                else
                    Debug.Log("Player has failed the lesson.");
                GetComponent<SceneSwap>().SwapScene("Lessons");
                break;
            }
        }
    }

}
