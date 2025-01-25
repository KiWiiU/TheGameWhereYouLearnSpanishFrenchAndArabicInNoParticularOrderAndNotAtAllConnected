using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCosmetics : MonoBehaviour
{
    // Set cosmetics when scene launched
    void Start()
    {
        SetHeadCosmetic();
        SetFaceCosmetic();
        SetPantsCosmetic();
        SetShoesCosmetic();
        SetShirtCosmetic();
        SetAccessoryCosmetic();
        SetPlayerColors();
    }

    public void SetPlayerColors() {
        // Indeces of children need to be correct (head, face, accessory, pants, shirt, shoes)
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Holder.currentHeadColor;
        transform.GetChild(1).GetComponent<SpriteRenderer>().color = Holder.currentFaceColor;
        transform.GetChild(2).GetComponent<SpriteRenderer>().color = Holder.currentAccessoryColor;
        transform.GetChild(3).GetComponent<SpriteRenderer>().color = Holder.currentPantsColor;
        transform.GetChild(4).GetComponent<SpriteRenderer>().color = Holder.currentShirtColor;
        transform.GetChild(5).GetComponent<SpriteRenderer>().color = Holder.currentShoesColor;
    }

    public void SetHeadCosmetic() {
        if(Holder.currentHeadCosmetic != null) {
            transform.Find("Head").GetComponent<SpriteRenderer>().sprite = Holder.currentHeadCosmetic.Front;
        }
    }
    public void SetFaceCosmetic() {
        if(Holder.currentFaceCosmetic != null) {
            transform.Find("Face").GetComponent<SpriteRenderer>().sprite = Holder.currentFaceCosmetic.Front;
        }
    }
    public void SetPantsCosmetic() {
        if(Holder.currentPantsCosmetic != null) {
            transform.Find("Pants").GetComponent<SpriteRenderer>().sprite = Holder.currentPantsCosmetic.Front;
        }
    }
    public void SetShoesCosmetic() {
        if(Holder.currentShoesCosmetic != null) {
            transform.Find("Shoes").GetComponent<SpriteRenderer>().sprite = Holder.currentShoesCosmetic.Front;
        }
    }
    public void SetShirtCosmetic() {
        if(Holder.currentShirtCosmetic != null) {
            transform.Find("Shirt").GetComponent<SpriteRenderer>().sprite = Holder.currentShirtCosmetic.Front;
        }
    }
    public void SetAccessoryCosmetic() {
        if(Holder.currentAccessoryCosmetic != null) {
            transform.Find("Accessory").GetComponent<SpriteRenderer>().sprite = Holder.currentAccessoryCosmetic.Front;
        }
    }
}
