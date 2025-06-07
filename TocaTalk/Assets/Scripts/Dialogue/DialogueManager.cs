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

    public GameObject dialogueBox;
    public TMP_Text LTLdialogueText;
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
    public void StartDialogue(DialogueList dialogues) {
        animator.SetBool("isOpen", true);
        Holder.canPlayerMove = false;
        isOpen = true;
        this.dialogues.Clear();
        LTLdialogueText.gameObject.SetActive(true);

        sentences.Clear();
        foreach(CharacterDialogue a in dialogues.dialogues) {
            this.dialogues.Enqueue(a);
        }
        currentDialogue = this.dialogues.Dequeue();
        
        // Load the first character's sentences
        string sentence = currentDialogue.sentence.Replace("[name]", Holder.Name);
        sentences.Enqueue(sentence);
        
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
            string a = currentDialogue.sentence.Replace("[name]", Holder.Name);
            sentences.Enqueue(a);
        }
        nameText.text = currentDialogue.npc.npc.Name;
        // crop the sprite so it just sees the head of the character
        if (currentDialogue.npc.npc.Name == "" || currentDialogue.npc.npc.Name == "You") // Narrator or Player
        {
            avatar.gameObject.SetActive(false);
            dialogueBox.transform.Find("AvatarBackground").gameObject.SetActive(false);
        }
        else
        {
            avatar.gameObject.SetActive(true);
            dialogueBox.transform.Find("AvatarBackground").gameObject.SetActive(true);
            avatar.sprite = Sprite.Create(currentDialogue.npc.npc.Sprite.texture, new Rect(6.5f, 43, 20, 20), new Vector2(0.5f, 0.5f), 100);
        }
        currentSentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(Type());
    }



    public void EndDialogue() {
       animator.SetBool("isOpen", false);
       isOpen = false;
       Holder.canPlayerMove = true;
       OnDialogueEnd?.Invoke();
    }
    public void Update() {
        if(Input.GetKeyDown(KeyCode.Return) && isOpen) {
            if(typingText) { // skip dialogue if still typing
                StopAllCoroutines();
                LTLdialogueText.text = currentSentence;
                typingText = false;
            } else {
                DisplayNextSentence();
            }    
        }
    }

    IEnumerator Type() {
        typingText = true;
        LTLdialogueText.text = "";
        foreach(char letter in currentSentence.ToCharArray()) {
            LTLdialogueText.text += letter;
            if(!letter.Equals(" "))
                yield return new WaitForSeconds(0.075f);
        }
        typingText = false;
    }
}
