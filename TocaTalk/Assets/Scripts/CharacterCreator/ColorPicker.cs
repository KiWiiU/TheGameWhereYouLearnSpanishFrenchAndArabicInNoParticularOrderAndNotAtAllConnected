using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    GameObject player;

    public void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void SetHeadColor() {
        // "Head" Child needs to be index 0
        player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(transform.GetChild(0).GetComponent<Slider>().value, transform.GetChild(1).GetComponent<Slider>().value, transform.GetChild(2).GetComponent<Slider>().value);
    }
    public void SetFaceColor() {
        // "Face" Child needs to be index 1
        player.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(transform.GetChild(0).GetComponent<Slider>().value, transform.GetChild(1).GetComponent<Slider>().value, transform.GetChild(2).GetComponent<Slider>().value);
    }
    public void SetShirtColor() {
        // "Shirt" Child needs to be index 4
        player.transform.GetChild(4).GetComponent<SpriteRenderer>().color = new Color(transform.GetChild(0).GetComponent<Slider>().value, transform.GetChild(1).GetComponent<Slider>().value, transform.GetChild(2).GetComponent<Slider>().value);
    }
    public void SetPantsColor() {
        // "Pants" Child needs to be index 3
        player.transform.GetChild(3).GetComponent<SpriteRenderer>().color = new Color(transform.GetChild(0).GetComponent<Slider>().value, transform.GetChild(1).GetComponent<Slider>().value, transform.GetChild(2).GetComponent<Slider>().value);
    }
    public void SetAccessoryColor() {
        // "Accessory" Child needs to be index 2
        player.transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(transform.GetChild(0).GetComponent<Slider>().value, transform.GetChild(1).GetComponent<Slider>().value, transform.GetChild(2).GetComponent<Slider>().value);
    }
    public void SetShoesColor() {
        // "Shoes" Child needs to be index 5
        player.transform.GetChild(5).GetComponent<SpriteRenderer>().color = new Color(transform.GetChild(0).GetComponent<Slider>().value, transform.GetChild(1).GetComponent<Slider>().value, transform.GetChild(2).GetComponent<Slider>().value);
    }
}
