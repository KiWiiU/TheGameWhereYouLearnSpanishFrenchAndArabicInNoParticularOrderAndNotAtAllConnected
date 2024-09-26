using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
public class Holder : MonoBehaviour
{
    public string Name;
    public int money, Sprogress, Fprogress, Aprogress;
    public Vector4 color;
    public UnlockableItem bald;
    public UnityEngine.UI.Image image;
    [SerializeField] public Dictionary<UnlockableItem, bool> unlockedItems = new();



    //testing stuff
    public void Start() {
        unlockedItems.Add(bald, true);
    }

    public void btnPress() {
        Debug.Log(unlockedItems[bald]);

        image.sprite = bald.Sprite; // magnifique
    }
}