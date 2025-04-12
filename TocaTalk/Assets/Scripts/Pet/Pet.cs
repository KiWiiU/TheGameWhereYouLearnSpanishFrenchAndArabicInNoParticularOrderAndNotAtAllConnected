using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pet", menuName = "Create Pet")]
public class Pet : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] string _name;
    [SerializeField] Sprite sprite;
    [SerializeField] double cost;


    public int Id {get {return id;} set {id = value;}}

    public string Name {get {return _name;} }

    public double Cost {get {return cost;}}

    public Sprite Sprite {get {return sprite;}}
}
