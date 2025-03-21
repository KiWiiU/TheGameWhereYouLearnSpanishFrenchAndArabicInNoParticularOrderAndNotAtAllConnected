using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using ArabicSupport;
public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;


    public TMP_Text dialogueText;
    public Image avatar;
    private Queue<CharacterDialogue> dialogues;
    private Queue<string> sentences;
    private CharacterDialogue currentDialogue;
    public event Action OnDialogueEnd;
    public Animator animator;
    private bool typingText;
    private bool isOpen;
    private string currentSentence;
    // Start is called before the first frame update
    void Start()
    {
        dialogues = new();
        sentences = new();
    }

    public bool IsOpen {get {return isOpen;}}

    // dialogues is an array of dialogues that characters have in order
    public void StartDialogue(CharacterDialogue[] dialogues) {
        animator.SetBool("isOpen", true);
        isOpen = true;
        this.dialogues.Clear();
        sentences.Clear();
        foreach(CharacterDialogue a in dialogues) {
            this.dialogues.Enqueue(a);
        }
        currentDialogue = this.dialogues.Dequeue();
        
        // Load the first character's sentences
        foreach(string sentence in currentDialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if(sentences.Count == 0) {
            if(dialogues.Count == 0) {
                EndDialogue();
                return;
            }
            // Load next character's dialogue
            currentDialogue = dialogues.Dequeue();
            foreach(string sentence in currentDialogue.sentences) {
                sentences.Enqueue(sentence);
            }
        }
        if(Holder.currentLanguage == 2) {
            nameText.text = ArabicFixer.Fix(currentDialogue.npc.npc.Name);
        }
        
        // crop the sprite so it just sees the head of the character
        avatar.sprite = Sprite.Create(currentDialogue.npc.npc.Sprite.texture, new Rect(6.5f, 43, 20, 20), new Vector2(0.5f, 0.5f), 100);
        if(Holder.currentLanguage == 2) {
            currentSentence = ArabicFixer.Fix(sentences.Dequeue());
        } else {
            currentSentence = sentences.Dequeue();
        }
        
        StopAllCoroutines();
        StartCoroutine(Type());
    }



    void EndDialogue() {
       animator.SetBool("isOpen", false);
       isOpen = false;
       OnDialogueEnd?.Invoke();
    }
    public void Update() {
        if(Input.GetKeyDown(KeyCode.Return) && isOpen) {
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
                yield return new WaitForSeconds(0.075f);
        }
        typingText = false;
    }
}
