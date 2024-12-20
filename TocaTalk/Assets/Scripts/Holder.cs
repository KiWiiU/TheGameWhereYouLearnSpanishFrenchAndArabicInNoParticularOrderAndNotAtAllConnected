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

    public void setCurrentLanguage(int language) {
        currentLanguage = language;
    }

    // Lesson = current lesson
    public static void progress(int lesson) {
        if(getProgress() == lesson-1) //so you progress linearly (can't do lesson 1 and unlock every single lesson)
            switch(currentLanguage) {
                case 0 : 
                    Sprogress += 1;
                    break;
                case 1 :
                    Fprogress += 1;
                    break;
                case 2 : 
                    Aprogress += 1;
                    break;
            }
    }

    public static int getProgress() {
        switch(currentLanguage) {
            case 0 : 
                return Sprogress;
            case 1 :
                return Fprogress;
            case 2 : 
                return Aprogress;
        }
        return -1;
    }
}