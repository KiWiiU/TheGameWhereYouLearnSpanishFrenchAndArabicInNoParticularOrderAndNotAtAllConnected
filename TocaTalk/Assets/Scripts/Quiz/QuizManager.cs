using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public GameObject obj; // Quiz parent game object
    private Queue<Question> questions;
    private bool isOpen;
    public Animator animator;
    private int numCorrect; // number of questions the player got correct
    private int numQuestions;
    public event Action OnQuizEnd; // event to trigger when quiz ends
    private Question currentQuestion;

        // Same structure as the DialogueManager

    public bool IsOpen {get {return isOpen;}}
    public int NumCorrect {get {return numCorrect;}}
    public int NumQuestions {get {return numQuestions;}}
    public void Start() {
        questions = new();
        isOpen = false;
    }
    public void StartQuiz(Question[] questions) {
        numCorrect = 0;
        isOpen = true;
        animator.SetBool("isOpen", true);
        foreach(Question q in questions) {
            this.questions.Enqueue(q);
        }
        currentQuestion = this.questions.Dequeue();
        numQuestions = questions.Length;
        obj.GetComponent<Canvas>().enabled = true;
        UpdateUI();
    }

    private void UpdateUI() {
        TMPro.TMP_Text questionText = obj.GetComponentInChildren<TMPro.TMP_Text>();
        questionText.text = currentQuestion.question;
        Button[] buttons = obj.GetComponentsInChildren<Button>(true); 
        GameObject input = obj.GetComponentInChildren<TMP_InputField>(true).gameObject;
        if(currentQuestion.isMultipleChoice) {// if multiple choice  
            for(int i = 0; i < buttons.Length-1; i++) {
                buttons[i].gameObject.SetActive(true);
                buttons[i].GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[i];
            }
            buttons[buttons.Length-1].gameObject.SetActive(false);
                input.SetActive(false);
        }
        else {
            for(int i = 0; i < buttons.Length; i++) {
                buttons[i].gameObject.SetActive(false);
            }
            buttons[buttons.Length-1].gameObject.SetActive(true);
            input.SetActive(true);
        }
    }

    public void AnswerMCQuiz(int answer) {
        if(answer == currentQuestion.correctAnswer) {
            numCorrect++;
            Debug.Log("Correct!");
        }
        else {
            Debug.Log("Incorrect!");
        }
        if(questions.Count > 0) {
            currentQuestion = questions.Dequeue();
        }
        else {
            StopAllCoroutines();
            StartCoroutine(StopQuiz());
        }
        UpdateUI();
    }

    public void AnswerTextQuiz() {
        TMP_InputField field = obj.GetComponentInChildren<TMP_InputField>();
        string answer = field.text;
        field.text = "";
        if(answer.ToLower().Equals(currentQuestion.correctAnswerText.ToLower())) {
            numCorrect++;
            Debug.Log("Correct!");
        }
        else {
            Debug.Log("Incorrect!");
        }
        if(questions.Count > 0) {
            currentQuestion = questions.Dequeue();
        }
        else {
            StopAllCoroutines();
            StartCoroutine(StopQuiz());
        }
        UpdateUI();
    }

    public void OnEndEdit() { // if user presses enter within the input field, submit the text
        if(Input.GetKeyDown(KeyCode.Return)) {
            AnswerTextQuiz();
        }
    }

    private IEnumerator StopQuiz() {
        // if(!isOpen) yield return null; // wait until the quiz is closed to continue
        EventSystem.current.SetSelectedGameObject(null); //deselect the buttons so the enter key does not activate the button after it closes
        isOpen = false;
        animator.SetBool("isOpen", false);
        yield return new WaitForSeconds(1f);
        obj.GetComponent<Canvas>().enabled = false;
        OnQuizEnd?.Invoke();
    }
}
