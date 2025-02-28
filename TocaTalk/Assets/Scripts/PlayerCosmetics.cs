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
        if(!Holder.currentCosmetics[0].Colored)
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[0];
        if(!Holder.currentCosmetics[1].Colored)
            transform.GetChild(1).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[1];
        if(!Holder.currentCosmetics[2].Colored)
            transform.GetChild(2).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[2];
        if(!Holder.currentCosmetics[3].Colored)
            transform.GetChild(3).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[3];
        if(!Holder.currentCosmetics[4].Colored)
            transform.GetChild(4).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[4];
        if(!Holder.currentCosmetics[5].Colored)
            transform.GetChild(5).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[5];
    }

    public void SetPlayerCosmetic(int type) {
        if(Holder.currentCosmetics[type] != null) {
                transform.GetChild(type).GetComponent<SpriteRenderer>().sprite = Holder.currentCosmetics[type].Front;
        }
    }
}
