using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomizer : MonoBehaviour
{

    public PlayerCosmetics player;
    public TMPro.TMP_Dropdown[] menu;
    void Awake() {
    }
    // Start is called before the first frame update
    void Start()
    {
        menu[0].value = (int)Holder.currentHeadCosmetic.Type;
        menu[0].RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // set the head cosmetic of player
    public void setHead(int option) {
        Holder.currentHeadCosmetic = (UnlockableItem)Resources.Load("Cosmetics/" + option);
        player.SetHeadCosmetic();
    }

    public void setFace(int option) {

    }

    public void setPants(int option) {

    }
    public void setAccessory(int option) {

    }

    public void setShoes(int option) {

    }
}
