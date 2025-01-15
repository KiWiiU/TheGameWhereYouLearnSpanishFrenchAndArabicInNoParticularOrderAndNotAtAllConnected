using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCosmetics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetHeadCosmetic();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            transform.Find("Pants").GetComponent<SpriteRenderer>().sprite = Holder.currentShoesCosmetic.Front;
        }
    }
    public void SetShirtCosmetic() {
        if(Holder.currentShirtCosmetic != null) {
            transform.Find("Pants").GetComponent<SpriteRenderer>().sprite = Holder.currentShirtCosmetic.Front;
        }
    }
    public void SetAccessoryCosmetic() {
        if(Holder.currentShoesCosmetic != null) {
            transform.Find("Pants").GetComponent<SpriteRenderer>().sprite = Holder.currentAccessoryCosmetic.Front;
        }
    }
}
