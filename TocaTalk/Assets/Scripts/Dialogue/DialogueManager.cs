using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;


    public TMP_Text dialogueText;
    public Image avatar;
    private Queue<string> sentences;
    private Dialogue currentDialogue;

    public Animator animator;
    private bool typingText;
    public bool isOpen;
    private string currentSentence;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new();
    }
    public void StartDialogue(Dialogue dialogue) {
        animator.SetBool("isOpen", true);
        isOpen = true;
        sentences.Clear();
        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        currentDialogue = dialogue;
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if(sentences.Count == 0) {
            EndDialogue();
            return;
        }
        nameText.text = currentDialogue.npc.name;
        avatar.sprite = currentDialogue.npc.Sprite;
        currentSentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(Type());
    }

    void EndDialogue() {
       animator.SetBool("isOpen", false);
       isOpen = false;
    }
    public void Update() {
        if(Input.GetKeyDown(KeyCode.Return)) {
            if(typingText) { // skip dialogue if still typing
                StopAllCoroutines();
                dialogueText.text = currentSentence;
                typingText = false;
            } else {
                DisplayNextSentence();
            }    
        }
    }

    IEnumerator Type() {
        typingText = true;
        dialogueText.text = "";
        foreach(char letter in currentSentence.ToCharArray()) {
            dialogueText.text += letter;
            if(!letter.Equals(" "))
                yield return new WaitForSeconds(0.1f);
        }
        typingText = false;
    }
}
