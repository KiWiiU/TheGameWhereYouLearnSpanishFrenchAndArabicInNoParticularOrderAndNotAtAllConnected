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

        // constant offset of 33 to account for the fact that there are 32 head cosmetics
        menu[1].value = (int)(Holder.currentFaceCosmetic.Type-33);
        menu[1].RefreshShownValue();

        // constant offset of 38 to account for face cosmetics
        menu[2].value = (int)(Holder.currentPantsCosmetic.Type-38);
        menu[2].RefreshShownValue();

        menu[3].value = (int)(Holder.currentShirtCosmetic.Type-42);
        menu[3].RefreshShownValue();

        menu[4].value = (int)(Holder.currentShoesCosmetic.Type-50);
        menu[4].RefreshShownValue();

        menu[5].value = (int)(Holder.currentAccessoryCosmetic.Type-54);
        menu[5].RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // set the head cosmetic of player
    public void setHead(int option) {
        Holder.currentHeadCosmetic = (UnlockableItem)Resources.Load("Cosmetics/Head/" + (UnlockableItem.CosmeticType)option);
        player.SetHeadCosmetic();
    }

    public void setFace(int option) {
        Holder.currentFaceCosmetic = (UnlockableItem)Resources.Load("Cosmetics/Face/" + (UnlockableItem.CosmeticType)(option+33));
        player.SetFaceCosmetic();
    }

    public void setPants(int option) {
        Holder.currentPantsCosmetic = (UnlockableItem)Resources.Load("Cosmetics/Pants/" + (UnlockableItem.CosmeticType)(option+38));
        player.SetPantsCosmetic();
    }

    public void setShirt(int option) {
        Holder.currentShirtCosmetic = (UnlockableItem)Resources.Load("Cosmetics/Shirt/" + (UnlockableItem.CosmeticType)(option+42));
        player.SetShirtCosmetic();
    }
    public void setAccessory(int option) {
        Holder.currentAccessoryCosmetic = (UnlockableItem)Resources.Load("Cosmetics/Accessory/" + (UnlockableItem.CosmeticType)(option+54));
        player.SetAccessoryCosmetic();
    }

    public void setShoes(int option) {
        Holder.currentShoesCosmetic = (UnlockableItem)Resources.Load("Cosmetics/Shoes/" + (UnlockableItem.CosmeticType)(option+50));
        player.SetShoesCosmetic();
    }
}
