using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class LockLevels : MonoBehaviour
{
   public Sprite unlocked;
   public Sprite locked;
   public GameObject warning;

   private bool userContinue = false;
   // Lock/Unlock all the levels based on progress
   public void Start() {
        int progress = Holder.getProgress();
        for(int i = 0; i < transform.childCount;i++) { 
            if(i <= progress) {
                transform.GetChild(i).transform.GetChild(1).gameObject.GetComponent<Image>().sprite = unlocked;
            }
            else {
                transform.GetChild(i).transform.GetChild(1).gameObject.GetComponent<Image>().sprite = locked;
            }
        }
   }


    // level = which lesson #, uses currentLanguage to decide which scene to go to
   public void startLevel(int level) {
    StartCoroutine(check(level));
   }
   private IEnumerator check(int level) {
    if(level-1 <= Holder.getProgress()) {
        warning.SetActive(true);
        yield return new WaitUntil(() => userContinue); // wait until user continues
        string lesson = "Lesson" + level;
        if(Holder.currentLanguage == 0) lesson += "S";
        else if(Holder.currentLanguage == 1) lesson += "F";
        else if(Holder.currentLanguage == 2) lesson += "A";
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneSwap>().SwapScene(lesson);
    }
    yield break; // exit coroutine
   }

   public void Continue() {
    userContinue = true;
   }
   public void Back() {
    userContinue = false;
    warning.SetActive(false);
    StopAllCoroutines();
   }
}
