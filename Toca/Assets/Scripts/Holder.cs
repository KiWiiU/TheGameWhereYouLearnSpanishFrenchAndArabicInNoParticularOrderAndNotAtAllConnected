using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Holder : MonoBehaviour
{
    public static string Name;
    public static int Money, Sprogress, Fprogress, Aprogress, currentLanguage;
    public static Color color;
    public UnityEngine.UI.Image image;
    [SerializeField] public Dictionary<UnlockableItem, bool> unlockedItems = new();

    public void NameIt(string n)
    {
        Name = n;
    }
 
    public void theLerper(float c)
    {
        color = Color.Lerp(new Color(255, 219, 172), new Color(156, 114, 72), c);
        Debug.Log(color);
        image.color = color;
    }
}