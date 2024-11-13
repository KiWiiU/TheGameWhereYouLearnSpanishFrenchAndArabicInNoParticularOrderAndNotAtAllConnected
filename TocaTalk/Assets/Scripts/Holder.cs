using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Holder : MonoBehaviour
{
    public static string Name;
    public static int Money, Sprogress, Fprogress, Aprogress, currentLanguage;
    public static Color color;
    [SerializeField] public Dictionary<UnlockableItem, bool> unlockedItems = new();
    public void NameIt(string n)
    {
        Name = n;
    }

}