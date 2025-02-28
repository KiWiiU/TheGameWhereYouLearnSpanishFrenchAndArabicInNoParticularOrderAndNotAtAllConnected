using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticDropdown : MonoBehaviour
{

    public List<CosmeticItem> cosmeticItems;
    public ColorPicker associatedColorPicker;
    public CosmeticItem.CosmeticType cosmeticType;
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
        for(int i = 0; i < cosmeticItems.Count;i++) {
            if(cosmeticItems[i].Equals(Holder.currentCosmetics[(int)cosmeticType])) {
                dropdown.SetValueWithoutNotify(i);
                SetCosmeticFromDropdown(i);

                break;
            }
        }
        dropdown.RefreshShownValue();
    }

    public void SetCosmetic(int index) {
        Holder.currentCosmetics[(int)cosmeticType] = cosmeticItems[index];
        Holder.currentCosmeticColors[(int)cosmeticType] = new Color(cosmeticItems[index].DefaultColor.r, cosmeticItems[index].DefaultColor.g, cosmeticItems[index].DefaultColor.b, 1f);
        player.SetAllPlayerCosmetics();
        player.SetPlayerColors();
        
        if(Holder.currentCosmetics[(int)cosmeticType].Colored) {
            associatedColorPicker.gameObject.SetActive(false);
            Holder.currentCosmeticColors[(int)cosmeticType] = new Color(211, 211, 211);
        } else {
            associatedColorPicker.gameObject.SetActive(true);
        }
        associatedColorPicker.Set(Holder.currentCosmeticColors[(int)cosmeticType]);
    }

    private void SetCosmeticFromDropdown(int index) {
        Holder.currentCosmetics[(int)cosmeticType] = cosmeticItems[index];
        if(Holder.currentCosmetics[(int)cosmeticType].Colored) {
            associatedColorPicker.gameObject.SetActive(false);
            Holder.currentCosmeticColors[(int)cosmeticType] = new Color(211, 211, 211);
        } else {
            associatedColorPicker.gameObject.SetActive(true);
        }
        player.SetPlayerCosmetic((int)cosmeticType);
        if(Holder.currentCosmeticColors[(int)cosmeticType].a == 0f) { // if save file is new
            Holder.currentCosmeticColors[(int)cosmeticType] = new Color(cosmeticItems[index].DefaultColor.r, cosmeticItems[index].DefaultColor.g, cosmeticItems[index].DefaultColor.b, 1f);
        }
        player.SetPlayerColors();
        associatedColorPicker.Set(Holder.currentCosmeticColors[(int)cosmeticType]);
    }
}
