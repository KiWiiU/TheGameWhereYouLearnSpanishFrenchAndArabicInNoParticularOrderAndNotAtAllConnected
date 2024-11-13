using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    private DialogueManager dialogueManager;
    private QuizManager quizManager;

    public void Start() {
        dialogueManager = GameObject.FindWithTag("Dialogue").GetComponent<DialogueManager>();
        quizManager = GameObject.FindWithTag("Quiz").GetComponent<QuizManager>();
    }
    public void FixedUpdate() {
        // cant move if dialogue or quiz is open
        if(!dialogueManager.IsOpen && !quizManager.IsOpen) { 
            GetComponent<Rigidbody2D>().velocity = new Vector2(
                Input.GetAxisRaw("Horizontal") * walkSpeed,
                Input.GetAxisRaw("Vertical") * walkSpeed
            );
        }
        else GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
