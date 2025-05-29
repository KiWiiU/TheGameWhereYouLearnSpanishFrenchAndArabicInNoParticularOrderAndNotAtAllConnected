using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shirt", menuName = "Create Customization/Shirt")]
public class ShirtCosmetic : CosmeticItem
{
    [SerializeField] private ShirtType shirtType;
    public ShirtType Type => shirtType;
    public enum ShirtType {
        Guaybera, // 42
        Hoodie,
        LongSleeve,
        Pollera,
        Sweater,
        TShirt,
        Tanktop,
        Waldo, // 49
    }
}
