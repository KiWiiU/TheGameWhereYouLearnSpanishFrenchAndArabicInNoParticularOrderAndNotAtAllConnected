using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticItem : ScriptableObject
{
    [SerializeField] string Name;
    [SerializeField] Sprite front;
    [SerializeField] Sprite back;
    [SerializeField] double cost;
    
    public double Cost {get {return cost;}}
    public Sprite Front {get {return front;}}
    public Sprite Back {get {return back;}}
    public string GetName {get {return Name;}}
}
