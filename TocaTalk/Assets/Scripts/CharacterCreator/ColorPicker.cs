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
    public CosmeticItem.CosmeticType cosmeticType;

    public void Start() {
        // set the cosmetic colors to the current slider values
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCosmetics>();
        transform.GetChild(0).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentCosmeticColors[(int)cosmeticType].r);
        transform.GetChild(1).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentCosmeticColors[(int)cosmeticType].g);
        transform.GetChild(2).GetComponent<Slider>().SetValueWithoutNotify(Holder.currentCosmeticColors[(int)cosmeticType].b);
    }
    public void SetCosmeticColor() {
        if(Holder.currentCosmetics[(int)cosmeticType].Colored) return;
        Color color = new Color(transform.GetChild(0).GetComponent<Slider>().value, transform.GetChild(1).GetComponent<Slider>().value, transform.GetChild(2).GetComponent<Slider>().value);
        Holder.currentCosmeticColors[(int)cosmeticType] = color;
        player.SetPlayerColors();
    }

    public void Reset() {
        transform.GetChild(0).GetComponent<Slider>().SetValueWithoutNotify(1f);
        transform.GetChild(1).GetComponent<Slider>().SetValueWithoutNotify(1f);
        transform.GetChild(2).GetComponent<Slider>().SetValueWithoutNotify(1f);
    }

    public void Set(Color color) {
        transform.GetChild(0).GetComponent<Slider>().SetValueWithoutNotify(color.r);
        transform.GetChild(1).GetComponent<Slider>().SetValueWithoutNotify(color.g);
        transform.GetChild(2).GetComponent<Slider>().SetValueWithoutNotify(color.b);
    }
}
