using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class GridPetElement : MonoBehaviour
{
    Pet pet;
    bool unlocked;
    double hunger;
    public void setPet(Pet p, bool s, double h) {
        pet = p;
        unlocked = s;
        hunger = h;
        transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = pet.Sprite;
        if(unlocked) {
            transform.GetChild(1).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate {
                if(unlocked) {
                    if(Holder.currentPet != pet.Id) {
                        Holder.currentPet = pet.Id;
                        transform.parent.parent.GetComponent<Tamagotchi>().DisplayText("You have chosen " + pet.Name + "!");
                        SetActivePet();
                    }
                    else {
                        Holder.currentPet = null;
                        transform.parent.parent.GetComponent<Tamagotchi>().DisplayText("You have chosen no pet!");
                        SetInactivePet();
                    }
                }
            });
            transform.GetChild(2).GetComponent<TMP_Text>().text = pet.Name;
            transform.GetChild(3).GetChild(1).transform.localScale = new Vector3((float)hunger/100f, 1f, 1f);
        }
        else {
            transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().color = new Color(0.122f, 0.122f, 0.122f);
            ColorBlock colors = transform.GetChild(1).GetComponent<UnityEngine.UI.Button>().colors;
            colors.highlightedColor = new Color(1f, 1f, 1f);
            colors.pressedColor = new Color(1f, 1f, 1f);
            transform.GetChild(1).GetComponent<UnityEngine.UI.Button>().colors = colors;
            transform.GetChild(2).GetComponent<TMP_Text>().text = "???";
            transform.GetChild(3).gameObject.SetActive(false);
        }
    }

    public void SetActivePet() {
        foreach(Transform c in transform.parent) {
            c.GetChild(0).GetComponent<Image>().enabled = false;
        }     
        transform.GetChild(0).GetComponent<Image>().enabled = true;
    }

    public void SetInactivePet() {
        foreach(Transform c in transform.parent) {
            c.GetChild(0).GetComponent<Image>().enabled = false;
        }   
    }

    public void Feed() {
        int feedAmount = 10;
        hunger = Math.Min(feedAmount+hunger,100);
        transform.GetChild(3).GetChild(1).transform.localScale = new Vector3((float)hunger/100f, 1f, 1f);
        Holder.petHunger[(int)Holder.currentPet] = Math.Min(feedAmount+Holder.petHunger[(int)Holder.currentPet], 100); 
        transform.parent.parent.GetComponent<Tamagotchi>().DisplayText("You have fed " + pet.Name + "!");
    }

    
}
