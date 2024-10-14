using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CousinDialogue : MonoBehaviour
{
    private bool isTriggered;

    void Start() {
        isTriggered = false; 
    }
    void Update() {
        if(Input.anyKeyDown && !isTriggered) {
            isTriggered = true;
            GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }
}
