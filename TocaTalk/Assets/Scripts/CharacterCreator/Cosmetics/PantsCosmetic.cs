using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Pants", menuName = "Create Customization/Pants")]
public class PantsCosmetic : CosmeticItem
{
    [SerializeField] private PantsType pantsType;
    public PantsType Type => pantsType;
     public enum PantsType {
        // Pants

        Sweatpants, // 38
        Jeans,
        Shorts,
        Skirt, // 41
    }   
}
