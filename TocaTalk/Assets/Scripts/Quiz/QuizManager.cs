using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public GameObject obj;
    private Queue<Question> questions;
    private bool isOpen;
    public Animator animator;
    private int numCorrect;
    public event Action OnQuizEnd;
    private Question currentQuestion;

    public bool IsOpen {get {return isOpen;}}
    public int NumCorrect {get {return numCorrect;}}
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
        obj.GetComponent<Canvas>().enabled = true;
        UpdateUI();
    }

    private void UpdateUI() {
        TMPro.TMP_Text questionText = obj.GetComponentInChildren<TMPro.TMP_Text>();
        questionText.text = currentQuestion.question;
        Button[] buttons = obj.GetComponentsInChildren<Button>();
        for(int i = 0; i < buttons.Length; i++) {
            buttons[i].GetComponentInChildren<TMPro.TMP_Text>().text = currentQuestion.answers[i];
        }
    }

    public void AnswerQuiz(int answer) {
        if(answer == currentQuestion.correctAnswer) {
            numCorrect++;
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

    private IEnumerator StopQuiz() {
        isOpen = false;
        animator.SetBool("isOpen", false);
        yield return new WaitForSeconds(1f);
        obj.GetComponent<Canvas>().enabled = false;
        OnQuizEnd?.Invoke();
    }
}
