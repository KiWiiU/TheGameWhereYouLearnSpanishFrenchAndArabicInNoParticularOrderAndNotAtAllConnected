using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Head", menuName = "Create Customization/Head")]
public class HeadCosmetic : CosmeticItem
{
    [SerializeField] private HeadType type;
    public HeadType Type {get {return type;}}

    public enum HeadType {
        Bald, AllMight, Banana, Beanie, Beret, Bob, Cap, Cap2,
        Cornrows, Deku, Flowy, HeadScarf, HeadScarf2, Kufi, 
        Jessie, Lick, Long, Messy, Miku, Mushroom, Ponytail,
        Rick, Sailor, ShadeHat, SideSwept, Smurf, Sombrero,
        Stone, Toad, Todoroki, TopHat, Witch, Short
    }
}
