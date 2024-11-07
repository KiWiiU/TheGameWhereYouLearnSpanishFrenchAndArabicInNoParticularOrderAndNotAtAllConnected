using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public void FixedUpdate() {
        // cant move if dialogue is open
        if(!GameObject.FindWithTag("Dialogue").GetComponent<DialogueManager>().isOpen) { 
            GetComponent<Rigidbody2D>().velocity = new Vector2(
                Input.GetAxisRaw("Horizontal") * walkSpeed,
                Input.GetAxisRaw("Vertical") * walkSpeed
            );
        }
        else GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
