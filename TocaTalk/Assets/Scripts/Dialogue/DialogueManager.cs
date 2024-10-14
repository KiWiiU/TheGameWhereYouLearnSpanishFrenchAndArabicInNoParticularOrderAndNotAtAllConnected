using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Image avatar;
    private Queue<string> sentences;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new();
    }
    public void StartDialogue(Dialogue dialogue) {
        animator.SetBool("isOpen", true);
        sentences.Clear();
        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        nameText.text = dialogue.name;
        avatar.sprite = dialogue.avatar;
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if(sentences.Count == 0) {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(Type(sentence));
    }

    void EndDialogue() {
       animator.SetBool("isOpen", false);
    }
    public void Update() {
        if(Input.GetKeyDown(KeyCode.Return)) {
            DisplayNextSentence();    
        }
    }

    IEnumerator Type(string sentence) {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            if(letter.Equals(" ")) //doesnt lag with spaces
                yield return null;
            else
                yield return new WaitForSeconds(0.1f);
        }
    }
}
