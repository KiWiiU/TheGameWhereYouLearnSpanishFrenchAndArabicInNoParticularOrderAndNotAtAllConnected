using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shoe", menuName = "Create Customization/Shoe")]
public class ShoeCosmetic : CosmeticItem
{
    [SerializeField] private ShoesType shoesType;
    public ShoesType Type => shoesType;
    public enum ShoesType {
        // Shoes

        Boots, // 50
        Heels,
        Sandals,
        Sneakers, // 53
    }
}
