using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pet", menuName = "Create Pet")]
public class Pet : ScriptableObject
{
    [SerializeField] string _name;

    [SerializeField] Sprite sprite;
    [SerializeField] double cost;

    public string Name {get {return _name;} }
    // public Sprite Sprite {get;}
    public double Cost {get {return cost;}}

    public Sprite Sprite {get {return sprite;}}
}
