using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public GameObject obj;

    public void StartQuiz(Question question) {
        obj.GetComponent<Canvas>().enabled = true;
        TMPro.TMP_Text questionText = obj.GetComponentInChildren<TMPro.TMP_Text>();
        questionText.text = question.question;
    }
}
