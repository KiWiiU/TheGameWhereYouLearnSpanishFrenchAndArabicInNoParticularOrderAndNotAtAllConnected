using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public string name;
    public int money, Sprogress, Fprogress, Aprogress;
    public Vector4 color;
    public Dictionary<UnlockableItem.Hair, bool> hairLock = new Dictionary<UnlockableItem.Hair, bool>();
    public Dictionary<UnlockableItem.Shoe, bool> shoeLock = new Dictionary<UnlockableItem.Shoe, bool>();
    public Dictionary<UnlockableItem.Shirt, bool> shirtLock = new Dictionary<UnlockableItem.Shirt, bool>();
    public Dictionary<UnlockableItem.Face, bool> faceLock = new Dictionary<UnlockableItem.Face, bool>();
    public Dictionary<UnlockableItem.Pants, bool> pantsLock = new Dictionary<UnlockableItem.Pants, bool>();
    public Dictionary<UnlockableItem.Hat, bool> hatLock = new Dictionary<UnlockableItem.Hat, bool>();
    public Dictionary<UnlockableItem.Pet, bool> petLock = new Dictionary<UnlockableItem.Pet, bool>();
}