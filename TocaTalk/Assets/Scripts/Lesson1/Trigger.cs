using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            GameObject.FindWithTag("Dialogue").GetComponent<DialogueTrigger>().TriggerDialogue();
            Destroy(this);
        }
    }
}
