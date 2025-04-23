using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Face", menuName = "Create MarketplaceItem")]
public class MarketplaceItem : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemImage;
    [SerializeField] private double itemCost;
    [SerializeField] private string itemDescription;

    public string Name {get {return itemName;}}
    public Sprite Sprite {get {return itemImage;}}
    public double Cost {get {return itemCost;}}
    public string Description {get {return itemDescription;}}
    
}
