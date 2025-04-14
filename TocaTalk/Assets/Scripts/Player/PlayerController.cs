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

    public void Start() {
        // set player's skin color
        GetComponent<SpriteRenderer>().color = Holder.skinColor;
        // initialize pet (position history for it to follow, create the pet object)
        petObject = new GameObject("Pet");
        petObject.transform.SetParent(transform);
        petObject.transform.position = transform.position;
        SpriteRenderer spriteRenderer = petObject.AddComponent<SpriteRenderer>();

        if(Holder.currentPet == null)
            spriteRenderer.sprite = null;
        else 
            spriteRenderer.sprite = Resources.LoadAll<Pet>("Pets")[(int)Holder.currentPet].Sprite;

        spriteRenderer.sortingLayerName = "Player";
        spriteRenderer.sortingOrder = -100;
        // clifford the big red dog
        petObject.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
        petObject.transform.localPosition = transform.localPosition + new Vector3(0.25f, 0f, 0f);
        CircleCollider2D petCollider = petObject.AddComponent<CircleCollider2D>();
        petCollider.radius = 0.6f;
        Rigidbody2D petRigidbody = petObject.AddComponent<Rigidbody2D>();
        petRigidbody.gravityScale = 0f;
        petRigidbody.freezeRotation = true;
        petObject.layer = LayerMask.NameToLayer("Pet");
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
            // smooth the player's movement so it isnt choppy
            GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, targetVelocity, Time.deltaTime * 20f);
        }
        else GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        movePet();
        
    }

    private void movePet() {
        if(petObject.transform.position.x < transform.position.x) {
            petObject.GetComponent<SpriteRenderer>().flipX = false;
        } else {
            petObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        Vector3 oldPosition = petObject.transform.position;
        Vector3 newPosition = petObject.transform.position;
        if(Vector3.Distance(transform.position, petObject.transform.position) > 20f) {
            // Debug.Log("teleporting");
            petObject.transform.position = transform.position;
            newPosition = petObject.transform.position;
        }
        else if(Vector3.Distance(transform.position, petObject.transform.position) > 0.5f) {
            petObject.transform.position = Vector3.MoveTowards(petObject.transform.position, transform.position, Time.deltaTime * 4f);
            newPosition = petObject.transform.position;
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
