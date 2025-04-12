using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GridPetElement : MonoBehaviour
{
    Pet pet;
    bool unlocked;
    double hunger;
    public void setPet(Pet p, bool s, double h) {
        pet = p;
        unlocked = s;
        hunger = h;
        transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = pet.Sprite;
        if(unlocked) {
            transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate {
                if(unlocked) {
                    Holder.currentPet = pet.Id;
                    GameObject obj = GameObject.FindWithTag("GameController");
                    obj.GetComponent<TMP_Text>().text = "You have chosen " + pet.Name + "!";
                    obj.GetComponent<CanvasRenderer>().SetAlpha(1f);
                    obj.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0f, 2.5f, false); // animate fading out
                }
            });
            transform.GetChild(1).GetComponent<TMP_Text>().text = pet.Name;
            transform.GetChild(2).GetChild(1).transform.localScale = new Vector3((float)hunger/100f, 1f, 1f);
        }
        else {
            transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.122f, 0.122f, 0.122f);
            ColorBlock colors = transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().colors;
            colors.highlightedColor = new Color(1f, 1f, 1f);
            colors.pressedColor = new Color(1f, 1f, 1f);
            transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().colors = colors;
            transform.GetChild(1).GetComponent<TMP_Text>().text = "???";
            transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}
