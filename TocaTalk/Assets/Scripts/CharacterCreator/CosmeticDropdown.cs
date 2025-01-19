using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticDropdown : MonoBehaviour
{

    public List<CosmeticItem> cosmeticItems;
    PlayerCosmetics player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCosmetics>();
        var dropdown = GetComponent<TMPro.TMP_Dropdown>();
        dropdown.options.Clear();
        //add all the items in the list to the dropdown
        foreach (var item in cosmeticItems) {
            if(item.GetName != null && !item.GetName.Equals(""))
                dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = item.GetName });
            else // use custom name if custom name isn't set
                dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = item.name });
        }

        // set default value for dropdown
        if(cosmeticItems[0] is HeadCosmetic) {
            for(int i = 0; i < cosmeticItems.Count;i++) {
                if(cosmeticItems[i].Equals(Holder.currentHeadCosmetic)) {
                    dropdown.value = i;
                    break;
                }
            }
            if(dropdown.value == 0)
                SetHeadCosmetic(0);
        } else if(cosmeticItems[0] is PantsCosmetic) {
            for(int i = 0; i < cosmeticItems.Count;i++) {
                if(cosmeticItems[i].Equals(Holder.currentPantsCosmetic)) {
                    dropdown.value = i;
                    break;
                }
            }
            if(dropdown.value == 0)
                SetPantsCosmetic(0);
        } else if(cosmeticItems[0] is ShirtCosmetic) {
            for(int i = 0; i < cosmeticItems.Count;i++) {
                if(cosmeticItems[i].Equals(Holder.currentShirtCosmetic)) {
                    dropdown.value = i;
                    break;
                }
            } if(dropdown.value == 0)
                SetShirtCosmetic(0);
        } else if(cosmeticItems[0] is ShoeCosmetic) {
            for(int i = 0; i < cosmeticItems.Count;i++) {
                if(cosmeticItems[i].Equals(Holder.currentShoesCosmetic)) {
                    dropdown.value = i;
                    break;
                }
            }
            if(dropdown.value == 0)
                SetShoesCosmetic(0);
        } else if(cosmeticItems[0] is FaceCosmetic) {
            for(int i = 0; i < cosmeticItems.Count;i++) {
                if(cosmeticItems[i].Equals(Holder.currentFaceCosmetic)) {
                    dropdown.value = i;
                    break;
                }
            }
            if(dropdown.value == 0)
                SetFaceCosmetic(0);
        } else if(cosmeticItems[0] is AccessoryCosmetic) {
            for(int i = 0; i < cosmeticItems.Count;i++) {
                if(cosmeticItems[i].Equals(Holder.currentAccessoryCosmetic)) {
                    dropdown.value = i;
                    break;
                }
            }
            if(dropdown.value == 0)
                SetAccessoryCosmetic(0);
        }

        dropdown.RefreshShownValue();
    }

    public void SetHeadCosmetic(int index) {
        Holder.currentHeadCosmetic = cosmeticItems[index];
        player.SetHeadCosmetic();
    }
    public void SetPantsCosmetic(int index) {
        Holder.currentPantsCosmetic = cosmeticItems[index];
        player.SetPantsCosmetic();
    }
    public void SetShirtCosmetic(int index) {
        Holder.currentShirtCosmetic = cosmeticItems[index];
        player.SetShirtCosmetic();
    }
    public void SetShoesCosmetic(int index) {
        Holder.currentShoesCosmetic = cosmeticItems[index];
        player.SetShoesCosmetic();
    }
    public void SetFaceCosmetic(int index) {
        Holder.currentFaceCosmetic = cosmeticItems[index];
        player.SetFaceCosmetic();
    }
    public void SetAccessoryCosmetic(int index) {
        Holder.currentAccessoryCosmetic = cosmeticItems[index];
        player.SetAccessoryCosmetic();
    }
}
