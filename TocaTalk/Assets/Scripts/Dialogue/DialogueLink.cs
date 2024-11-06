using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLink : MonoBehaviour
{
    public Dialogue[] dialogues;

    public void TriggerDialogue() {
        StartCoroutine(TriggerDialogueSequence());
    }

    private IEnumerator TriggerDialogueSequence() {
        DialogueManager dialogueManager = GetComponent<DialogueManager>();
        for(int i = 0; i < dialogues.Length; i++) {
            dialogueManager.StartDialogue(dialogues[i]);
            
            // Wait until the dialogue is closed before continuing to the next one
            while(dialogueManager.isOpen) {
                yield return null;
            }
        }
    }
}
