using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    PlayerCosmetics player;

    // 0 = Head
    // 1 = Face
    // 2 = Accessory
    // 3 = Pants
    // 4 = Shirt
    // 5 = Shoes
    public int cosmeticType;

    public void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCosmetics>();
        
        // set the cosmetic colors to the current slider values
        switch(cosmeticType) {
            case 0:
                transform.GetChild(0).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentHeadColor.r);
                transform.GetChild(1).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentHeadColor.g);
                transform.GetChild(2).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentHeadColor.b);
                break;
            case 1:
                transform.GetChild(0).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentFaceColor.r);
                transform.GetChild(1).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentFaceColor.g);
                transform.GetChild(2).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentFaceColor.b);
                break;
            case 2:
                transform.GetChild(0).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentAccessoryColor.r);
                transform.GetChild(1).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentAccessoryColor.g);
                transform.GetChild(2).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentAccessoryColor.b);
                break;
            case 3:
                transform.GetChild(0).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentPantsColor.r);
                transform.GetChild(1).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentPantsColor.g);
                transform.GetChild(2).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentPantsColor.b);
                break;
            case 4:
                transform.GetChild(0).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentShirtColor.r);
                transform.GetChild(1).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentShirtColor.g);
                transform.GetChild(2).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentShirtColor.b);
            break;
            case 5:
                transform.GetChild(0).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentShoesColor.r);
                transform.GetChild(1).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentShoesColor.g);
                transform.GetChild(2).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentShoesColor.b);
            break;
        }
    }
    public void SetHeadColor() {
        Color color = new Color(transform.GetChild(0).GetComponent<Slider>().value, transform.GetChild(1).GetComponent<Slider>().value, transform.GetChild(2).GetComponent<Slider>().value);
        Holder.currentHeadColor = color;
        player.SetPlayerColors();
    }
    public void SetFaceColor() {
        Color color = new Color(transform.GetChild(0).GetComponent<Slider>().value, transform.GetChild(1).GetComponent<Slider>().value, transform.GetChild(2).GetComponent<Slider>().value);
        Holder.currentFaceColor = color;
        player.SetPlayerColors();
    }
    public void SetShirtColor() {
        Color color = new Color(transform.GetChild(0).GetComponent<Slider>().value, transform.GetChild(1).GetComponent<Slider>().value, transform.GetChild(2).GetComponent<Slider>().value);
        Holder.currentShirtColor = color;
        player.SetPlayerColors();
    }
    public void SetPantsColor() {
        Color color = new Color(transform.GetChild(0).GetComponent<Slider>().value, transform.GetChild(1).GetComponent<Slider>().value, transform.GetChild(2).GetComponent<Slider>().value);
        Holder.currentPantsColor = color;
        player.SetPlayerColors();   
    }
    public void SetAccessoryColor() {
        Color color = new Color(transform.GetChild(0).GetComponent<Slider>().value, transform.GetChild(1).GetComponent<Slider>().value, transform.GetChild(2).GetComponent<Slider>().value);
        Holder.currentAccessoryColor = color;
        player.SetPlayerColors();   
    }
    public void SetShoesColor() {
        Color color = new Color(transform.GetChild(0).GetComponent<Slider>().value, transform.GetChild(1).GetComponent<Slider>().value, transform.GetChild(2).GetComponent<Slider>().value);
        Holder.currentShoesColor = color;
        player.SetPlayerColors();
    }
}
