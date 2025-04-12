using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }
}
