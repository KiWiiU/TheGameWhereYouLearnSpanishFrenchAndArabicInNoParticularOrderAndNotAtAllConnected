using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Head", menuName = "Create Customization/Head")]
public class HeadCosmetic : CosmeticItem
{
    [SerializeField] private HeadType type;
    public HeadType Type {get {return type;}}

    public enum HeadType {
        Afro, Agal, AllMight, Beanie, Beret, Bob, Buzzcut, Cap,
        Cornrows, Deku, Dreadlocks, Floppyhat, Hijab, Jessie, Kufi, 
        Lick, Miku, MushroomCover, MushroomHat, PaperboyHat,
        Ponytail, Rick, Sailor, Senku, ShadeHat, Short, SideSwept,
        Smurf, Sombrero, Todoroki, Tokoyami, TopHat, Witch,
    }
}
