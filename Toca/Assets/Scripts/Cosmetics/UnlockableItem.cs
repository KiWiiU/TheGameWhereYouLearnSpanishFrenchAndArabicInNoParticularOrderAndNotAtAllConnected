using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cosmetic", menuName = "Create Cosmetic")]
public class UnlockableItem : ScriptableObject
{
    [SerializeField] string _name;

    [SerializeField] Sprite sprite;
    [SerializeField] double cost;
    [SerializeField] CosmeticType type;

    public string Name {get {return _name;} }
    // public Sprite Sprite {get;}
    public double Cost {get {return cost;}}

    public Sprite Sprite {get {return sprite;}}
    public CosmeticType Type {get {return type;}}
}

public enum CosmeticType { // can use this to determine in a script where to put the sprite on player's body
    Hair, Face, Shirt, Pants, Shoes, Hat, Pet
}