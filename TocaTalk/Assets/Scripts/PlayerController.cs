using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    private DialogueManager dialogueManager;
    private QuizManager quizManager;

    public Pet pet;
    private GameObject petObject;
     private Queue<Vector3> positionHistory = new Queue<Vector3>();
    private float recordInterval = 0.05f;
    private float timeSinceLastRecord = 0f;
    private int maxPositions = 10;
    private bool isOnRightSide = true;
    private bool isOnTopSide = true;
    private Vector2 currentOffset = new Vector2(2f, -1f);
    private Vector2 targetOffset = new Vector2(2f, -1f);

    public void Start() {
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
            GetComponent<Rigidbody2D>().velocity = new Vector2(
                Input.GetAxisRaw("Horizontal") * walkSpeed,
                Input.GetAxisRaw("Vertical") * walkSpeed
            );
        }
        else GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        movePet();
    }

    private void movePet() {
        // Record player position
        timeSinceLastRecord += Time.deltaTime;
        if (timeSinceLastRecord >= recordInterval) {
            // Update side based on player's horizontal movement
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            if (horizontalInput != 0) {
                isOnRightSide = horizontalInput < 0;
                // Update target offset instead of immediate position change
                targetOffset.x = isOnRightSide ? 2f : -2f;
            }
            if (verticalInput != 0) {
                isOnTopSide = verticalInput > 0;
                // Update target offset instead of immediate position change
                targetOffset.y = isOnTopSide ? -1f : 1f;
            }

            // Smoothly interpolate the current offset
            currentOffset = Vector2.Lerp(currentOffset, targetOffset, 3f * Time.deltaTime);

            // Use the interpolated offset for position
            Vector3 position = transform.position + new Vector3(currentOffset.x, currentOffset.y, 0f);
            
            positionHistory.Enqueue(position);
            if (positionHistory.Count > maxPositions) {
                positionHistory.Dequeue();
            }
            timeSinceLastRecord = 0f;
        }

        // Move pet to the oldest recorded position
        if (positionHistory.Count > 0) {
            Vector3 targetPosition = positionHistory.Peek();
            petObject.transform.position = Vector3.Lerp(petObject.transform.position, targetPosition, 5f * Time.deltaTime);
        }
    }
}
