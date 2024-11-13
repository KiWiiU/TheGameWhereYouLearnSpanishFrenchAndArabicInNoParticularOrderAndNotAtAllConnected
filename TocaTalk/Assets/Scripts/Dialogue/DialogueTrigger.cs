using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public CharacterDialogue[] dialogues;
    public void TriggerDialogue() {
        GetComponent<DialogueManager>().StartDialogue(dialogues);
    }
}
