using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    
    public Dialogue dialogue;
    public void TriggerDialogue() {
        // MAKE SURE OBJECT WITH "DIALOGUEMANAGER" SCRIPT HAS THE "DIALOGUE" TAG!
        GameObject.FindWithTag("Dialogue").GetComponent<DialogueManager>().StartDialogue(dialogue); 
    }

    // public void Update() {
    //     if(Input.GetKeyDown(KeyCode.Return)) {
    //         TriggerDialogue();
    //     }
    // }

    
}
