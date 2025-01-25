using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LockLevels : MonoBehaviour
{
   public Sprite unlocked;
   public Sprite locked;

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
    if(level-1 <= Holder.getProgress()) {
        string lesson = "Lesson" + level;
        if(Holder.currentLanguage == 0) lesson += "S";
        else if(Holder.currentLanguage == 1) lesson += "F";
        else if(Holder.currentLanguage == 2) lesson += "A";
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneSwap>().SwapScene(lesson);
    }
   }
}
