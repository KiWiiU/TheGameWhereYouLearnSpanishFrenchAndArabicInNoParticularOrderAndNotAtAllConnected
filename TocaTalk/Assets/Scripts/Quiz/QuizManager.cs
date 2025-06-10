using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public GameObject obj; // Quiz parent game object
    private Queue<Question> questions;
    private bool isOpen;
    public GameObject correctAnswer;
    public Animator animator;
    private int numCorrect; // number of questions the player got correct
    private int numQuestions;
    public event Action OnQuizEnd; // event to trigger when quiz ends
    private Question currentQuestion;

    // Same structure as the DialogueManager

    public bool IsOpen { get { return isOpen; } }
    public int NumCorrect { get { return numCorrect; } }
    public int NumQuestions { get { return numQuestions; } }

    public TMP_InputField LTLField;
    public void Start()
    {
        questions = new();
        isOpen = false;
    }
    public void StartQuiz(QuestionList questions)
    {
        numCorrect = 0;
        isOpen = true;
        animator.SetBool("isOpen", true);
        Holder.canPlayerMove = false;
        foreach (Question question in questions.questions)
        {
            this.questions.Enqueue(question);
        }

        LTLField.gameObject.SetActive(false);
        LTLField.gameObject.SetActive(true);
        currentQuestion = this.questions.Dequeue();
        numQuestions = questions.questions.Length;
        obj.GetComponent<Canvas>().enabled = true;
        UpdateUI();
    }

    private void UpdateUI()
    {
        obj.transform.GetChild(0).Find("bg").GetComponent<Image>().color = new Color(33f / 255f, 93f / 255f, 142f / 255f); // blue bg
        TMP_Text questionText = obj.GetComponentInChildren<TMPro.TMP_Text>();
        questionText.text = currentQuestion.question;
        Button[] buttons = obj.GetComponentsInChildren<Button>(true);
        
        if (currentQuestion.isMultipleChoice)
        {// if multiple choice  
            for (int i = 0; i < buttons.Length - 1; i++)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].GetComponentInChildren<TMP_Text>().text = (char)(i+65) + ": " + currentQuestion.answers[i];
            }
            buttons[buttons.Length - 1].gameObject.SetActive(false);
            LTLField.gameObject.SetActive(false);
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
            buttons[buttons.Length - 1].gameObject.SetActive(true);
            LTLField.gameObject.SetActive(true);
        }
        correctAnswer.SetActive(false);
    }

    private void RemoveUI()
    {
        Button[] buttons = obj.GetComponentsInChildren<Button>(true);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
            buttons[buttons.Length - 1].gameObject.SetActive(false);
            LTLField.gameObject.SetActive(false);
    }

    public void AnswerQuiz(int answer)
    {
        if (currentQuestion.isMultipleChoice)
        {
            StartCoroutine(AnswerMCQuiz(answer));
        }
        else
        {
            StartCoroutine(AnswerTextQuiz());
        }
    }

    public IEnumerator AnswerMCQuiz(int answer)
    {
        if (answer == currentQuestion.correctAnswer)
        {
            numCorrect++;
        }
        else
        {
            if (Holder.currentPet != null)
            {
                Holder.petHunger[(int)Holder.currentPet] -= 20f;
            }
            yield return StartCoroutine(ShowCorrectAnswer());
        }
        if (questions.Count > 0)
        {
            currentQuestion = questions.Dequeue();
            UpdateUI();
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(StopQuiz());
        }
    }

    public IEnumerator AnswerTextQuiz()
    {
        string answer = null;
        answer = LTLField.text;
        

        bool correct = false;
        for (int i = 0; i < currentQuestion.correctAnswerTexts.Length; i++)
        {
            if (answer.ToLower().Equals(currentQuestion.correctAnswerTexts[i].ToLower()))
            {
                numCorrect++;
                correct = true;
                break;
            }
        }
        if (!correct)
        {
            if (Holder.currentPet != null)
            {
                Holder.petHunger[(int)Holder.currentPet] -= 20f;
            }
            yield return StartCoroutine(ShowCorrectAnswer());
        }

        LTLField.text = "";
        if (questions.Count > 0)
        {
            currentQuestion = questions.Dequeue();
            UpdateUI();
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(StopQuiz());
        }
    }

    public void OnEndEdit()
    { // if user presses enter within the input field, submit the text
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(AnswerTextQuiz());
        }
    }

    private IEnumerator StopQuiz()
    {
        // if(!isOpen) yield return null; // wait until the quiz is closed to continue
        EventSystem.current.SetSelectedGameObject(null); //deselect the buttons so the enter key does not activate the button after it closes
        isOpen = false;
        animator.SetBool("isOpen", false);
        yield return new WaitForSeconds(1f);
        obj.GetComponent<Canvas>().enabled = false;
        Holder.canPlayerMove = true;
        OnQuizEnd?.Invoke();
    }

    private IEnumerator ShowCorrectAnswer()
    {
        RemoveUI();
        obj.transform.GetChild(0).Find("bg").GetComponent<Image>().color = new Color(132f / 255f, 17f / 255f, 0f); // red color bg
        TMP_Text questionText = obj.GetComponentInChildren<TMPro.TMP_Text>();
        questionText.text = "INCORRECT!";
        if (currentQuestion.isMultipleChoice)
        {
            Button[] buttons = obj.GetComponentsInChildren<Button>(true);
            correctAnswer.GetComponent<TMP_Text>().text = "Correct Answer:\n" + buttons[currentQuestion.correctAnswer].GetComponentInChildren<TMP_Text>().text;
        }
        else
        {
            correctAnswer.GetComponent<TMP_Text>().text = "Correct Answer:\n" + currentQuestion.correctAnswerTexts[0];
        }
        correctAnswer.SetActive(true);
        yield return new WaitForSeconds(2.5f);
    }
}
