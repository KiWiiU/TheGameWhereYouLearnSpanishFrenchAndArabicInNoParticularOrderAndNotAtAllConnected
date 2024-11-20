using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    private DialogueManager dialogueManager;
    private QuizManager quizManager;

    public Pet pet;
    private GameObject petObject;
    private Stack<Vector3> positionHistory;
    private static float petDelay = 0.3f;
    private float timeSinceLastRecord;
    public void Start() {
        positionHistory  = new Stack<Vector3>();
        timeSinceLastRecord = 0f;
        petObject = new GameObject("Pet");
        petObject.transform.SetParent(transform);
        petObject.transform.position = transform.position;
        SpriteRenderer spriteRenderer = petObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = pet.Sprite;
        spriteRenderer.sortingLayerName = "Player";
        petObject.transform.localScale = transform.localScale / 2f;
        dialogueManager = GameObject.FindWithTag("Dialogue").GetComponent<DialogueManager>();
        quizManager = GameObject.FindWithTag("Quiz").GetComponent<QuizManager>();
    }
    public void FixedUpdate() {
        // cant move if dialogue or quiz is open
        if(!dialogueManager.IsOpen && !quizManager.IsOpen) { 
            Vector2 targetVelocity = new Vector2(
                Input.GetAxisRaw("Horizontal") * walkSpeed,
                Input.GetAxisRaw("Vertical") * walkSpeed
            );
            GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, targetVelocity, Time.deltaTime * 20f);
        }
        else GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        movePet();
    }

    private void movePet() {
        timeSinceLastRecord += Time.deltaTime;
        // Check if the player has moved and enough time has passed since the last record
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            if(timeSinceLastRecord >= petDelay) {
                Vector3 position = transform.position;
                if(!dialogueManager.IsOpen && !quizManager.IsOpen) {
                    if(Input.GetAxisRaw("Horizontal") > 0) {
                        position += new Vector3(-2.5f, -1f, 0f);
                    }
                    else if (Input.GetAxisRaw("Horizontal") < 0) {
                        position += new Vector3(2.5f, -1f, 0f);
                    }
                    if(Input.GetAxisRaw("Vertical") > 0) {
                        position += new Vector3(0f, -2f, 0f);
                    }
                    else if(Input.GetAxisRaw("Vertical") < 0) {
                        position += new Vector3(0f, 1f, 0f);
                    }
                }
                // Maintain a maximum length of the stack for position history
                if(positionHistory.Count >= 3) {
                    positionHistory.Pop(); // Remove the oldest position
                }
                positionHistory.Push(position);
                timeSinceLastRecord = 0f; // Reset the timer after recording
            }
        }

        // Move the pet towards the last recorded position if available
        if(positionHistory.Count > 0) {
            Vector3 targetPosition = positionHistory.Peek();
            // Only move the pet if the target position is significantly different
            if (Vector3.Distance(petObject.transform.position, targetPosition) > 1f) {
                petObject.transform.position = Vector3.Lerp(petObject.transform.position, targetPosition, Time.deltaTime * walkSpeed / 3f);
            }
        }

        // Have pet behind / in front of player
        if(petObject.transform.position.y+0.5f >= transform.position.y) {
            petObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        } else {
            petObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }
}
