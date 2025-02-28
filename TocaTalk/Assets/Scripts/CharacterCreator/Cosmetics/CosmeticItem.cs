using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticItem : ScriptableObject
{
    [SerializeField] string Name;
    [SerializeField] Sprite front;
    [SerializeField] Sprite back;
    [SerializeField] double cost;
    [SerializeField] bool isColored;
    [SerializeField] Color defaultColor;
    
    public double Cost {get {return cost;}}
    public Sprite Front {get {return front;}}
    public Sprite Back {get {return back;}}
    public string GetName {get {return Name;}}
    public bool Colored {get {return isColored;}}

    public Color DefaultColor {get {return defaultColor;}}

    public enum CosmeticType {
        Head = 0,
        Face = 1,
        Accessory = 2,
        Pants = 3,
        Shirt = 4,
        Shoes = 5
    }
}
