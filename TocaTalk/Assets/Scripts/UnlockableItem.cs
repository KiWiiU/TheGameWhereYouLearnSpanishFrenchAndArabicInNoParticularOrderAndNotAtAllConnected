using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cosmetic", menuName = "Create Cosmetic")]
public class UnlockableItem : ScriptableObject
{


    [SerializeField] Sprite front;
    [SerializeField] Sprite back;
    [SerializeField] double cost;
    [SerializeField] CosmeticType type;

    public CosmeticType Type {get {return type;}}
    public double Cost {get {return cost;}}

    public Sprite Front {get {return front;}}
    public Sprite Back {get {return back;}}

    public enum CosmeticType {
        // Head
        Bald, // 0
        AllMight, // 1
        Banana, // 2
        Beanie,
        Beret,
        Bob,
        Cap,
        Cap2,
        Cornrows,
        Deku,
        Flowy,
        HeadScarf,
        HeadScarf2,
        Kufi,
        Jessie,
        Lick, // 15
        Long,
        Messy, // 17
        Miku,
        Mushroom,
        Ponytail,
        Rick,
        Sailor,
        ShadeHat,
        SideSwept,
        Smurf,
        Sombrero,
        Stone,
        Toad,
        Todoroki,
        TopHat,
        Witch, // 31
        Short, // 32

        // Face
        Clean, // 33
        Beard,
        Goatee,
        Mustache,
        Sideburns, // 37

        // Pants

        Sweatpants, // 38
        Jeans,
        Shorts,
        Skirt, // 41

        // Shirts

        Hoodie, // 42
        LongSleeve,
        Pijamas,
        Pullover,
        Robe,
        Striped,
        TShirt,
        HalfShirt, // 49

        // Shoes

        Boots, // 50
        Heels,
        Sandals,
        Sneakers, // 53

        // Accessories

        Bowtie, // 54
        Glasses,
        Headphones,
        Scarf,
        Scarf2,
        Sunglasses,

    }
    
}
