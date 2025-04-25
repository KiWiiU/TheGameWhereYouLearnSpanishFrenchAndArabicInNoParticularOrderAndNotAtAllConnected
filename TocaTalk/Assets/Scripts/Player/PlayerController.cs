using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    private DialogueManager dialogueManager;
    private QuizManager quizManager;

    public Pet pet;
    private GameObject petObject;

    private Rigidbody2D rb;

    public void Start() {
        // initialize pet
        if(transform.Find("Pet") != null) {
            petObject = transform.Find("Pet").gameObject;
            if(Holder.currentPet != null) {
                petObject.transform.position = transform.position;
                petObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Pet>("Pets")[(int)Holder.currentPet].Sprite;
                petObject.transform.position = transform.position + new Vector3(0.25f, 0f, 0f);
            }
        }
        rb = GetComponent<Rigidbody2D>();
        dialogueManager = GameObject.FindWithTag("Dialogue")?.GetComponent<DialogueManager>();
        quizManager = GameObject.FindWithTag("Quiz")?.GetComponent<QuizManager>();
    }
    public void FixedUpdate() {
        // cant move if dialogue or quiz is open
        if(Holder.canPlayerMove) { 
            Vector2 targetVelocity = new Vector2(
                Input.GetAxisRaw("Horizontal") * walkSpeed,
                Input.GetAxisRaw("Vertical") * walkSpeed
            );
            // smooth the player's movement so it isnt choppy
            rb.velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, targetVelocity, Time.fixedDeltaTime * 20f);
        }
        else rb.velocity = Vector2.zero;
        movePet();
    }

    private void movePet() {
        if(petObject == null) return;
        if(petObject.transform.position.x < transform.position.x) {
            petObject.GetComponent<SpriteRenderer>().flipX = false;
        } else {
            petObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if(petObject.transform.position.y/4 < transform.position.y) {
            petObject.GetComponent<SpriteRenderer>().sortingOrder = 100;
        } else {
            petObject.GetComponent<SpriteRenderer>().sortingOrder = -100;
        }
        Vector3 oldPosition = petObject.transform.position;
        Vector3 newPosition = petObject.transform.position;
        if(Vector3.Distance(transform.position, petObject.transform.position) > 7.5f) {
            petObject.transform.position = transform.position;
            newPosition = petObject.transform.position;
        }
        else if(Vector3.Distance(transform.position, petObject.transform.position) > 0.5f) {
            petObject.transform.position = Vector3.MoveTowards(petObject.transform.position, transform.position, Time.deltaTime * 4f);
            if(petObject.GetComponent<CircleCollider2D>().bounds.Contains(gameObject.transform.position)) { // keep pet from walking into you
                petObject.transform.position = oldPosition;
            } else {
                newPosition = petObject.transform.position;
            }
        }

        if(Holder.currentPet != null) {
            Holder.petHunger[(int)Holder.currentPet] -= 0.0005f * (1 + Vector3.Distance(oldPosition, newPosition));
            if (Holder.petHunger[(int)Holder.currentPet] < 0) {
                Holder.petHunger[(int)Holder.currentPet] = 0;
            }
            // Debug.Log(Holder.petHunger[(int)Holder.currentPet]);
        }
    }
}
