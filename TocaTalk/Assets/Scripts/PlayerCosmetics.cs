using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCosmetics : MonoBehaviour
{
    // Set cosmetics when scene launched
    void Start()
    {
        SetAllPlayerCosmetics();
        SetPlayerColors();
    }

    public void SetAllPlayerCosmetics() {
        for(int i = 0; i < Enum.GetNames(typeof(CosmeticItem.CosmeticType)).Length;i++) {
            if(Holder.currentCosmetics[i] != null) {
                transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = Holder.currentCosmetics[i].Front;
            }
        }
    }
    public void SetPlayerColors() {
        // Indeces of children need to be correct (head, face, accessory, pants, shirt, shoes)
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[0];
        transform.GetChild(1).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[1];
        transform.GetChild(2).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[2];
        transform.GetChild(3).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[3];
        transform.GetChild(4).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[4];
        transform.GetChild(5).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[5];
    }

    public void SetPlayerCosmetic(int type) {
        if(Holder.currentCosmetics[type] != null) {
                transform.GetChild(type).GetComponent<SpriteRenderer>().sprite = Holder.currentCosmetics[type].Front;
        }
    }
}
