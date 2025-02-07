using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Accessory", menuName = "Create Customization/Accessory")]
public class AccessoryCosmetic : CosmeticItem
{
    [SerializeField] private AccessoryType accessoryType;
    public AccessoryType Type => accessoryType;
     public enum AccessoryType {
        // Accessories

        None, // 54
        Bowtie, // 55
        Glasses,
        Headphones,
        Scarf,
        Scarf2,
        Sunglasses,
    }
}
