using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LockLevels : MonoBehaviour
{
   public Sprite unlocked;
   public Sprite locked;
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

   public void startLevel(int level) {
    if(level-1 <= Holder.getProgress()) {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneSwap>().SwapScene("Lesson" + level);
    }
   }
}
