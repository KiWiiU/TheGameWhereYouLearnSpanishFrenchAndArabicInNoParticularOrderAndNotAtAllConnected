using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tamagotchi : MonoBehaviour
{
    public GameObject grid;
    public GameObject gridElementPrefab;
    List<GameObject> gridElements = new List<GameObject>();
    Pet[] petObjects;
    void Start()
    {    
        GameObject.FindWithTag("GameController").GetComponent<CanvasRenderer>().SetAlpha(0f);
        petObjects = Resources.LoadAll<Pet>("Pets");
        for(int i = 0; i < Holder.petUnlocked.Length;i++) {
            GameObject gridElement = Instantiate(gridElementPrefab, grid.transform);
            gridElements.Add(gridElement);
            gridElement.GetComponent<GridPetElement>().setPet(petObjects[i], Holder.petUnlocked[i], Holder.petHunger[i]);
            if(Holder.currentPet == i) {
                gridElement.GetComponent<GridPetElement>().SetActivePet();
            }
        }
    }

    public void FeedPet() {
        if(Holder.currentPet == null) return;
        if(Holder.petHunger[(int)Holder.currentPet] >= 100) {
            DisplayText("Your pet is already full!");
            return;
        }
        gridElements[(int)Holder.currentPet].GetComponent<GridPetElement>().Feed();
    }
    public void DisplayText(string s) {
        GameObject obj = GameObject.FindWithTag("GameController");
        obj.GetComponent<TMP_Text>().text = s;
        obj.GetComponent<CanvasRenderer>().SetAlpha(1f);
        obj.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0f, 2.5f, false); // animate fading out
    }
}
