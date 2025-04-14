using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question 
{
    [TextArea(3, 10)]
    public string question;
    public string[] answers;
    public int correctAnswer;

    public bool isMultipleChoice;
    [TextArea(3, 10)]
    public string[] correctAnswerTexts; // in case there are multiple correct answers
}
