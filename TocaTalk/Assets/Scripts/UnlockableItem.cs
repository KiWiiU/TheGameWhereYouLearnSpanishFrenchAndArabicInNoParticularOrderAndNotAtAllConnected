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
        Bald,
        AllMight,
        Banana,
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
        Lick,
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
        Witch,

        // Face
        Clean,
        Beard,
        Goatee,
        Handlebar,
        Mustache,
        Sideburns,

        // Pants

    }
    
}
