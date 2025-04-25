using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// Basically "holds" all the data for the player
public class Holder : MonoBehaviour
{
    public static string Name;
    public static int Money, Sprogress, Fprogress, Aprogress, currentLanguage;
    // Current Language -> 0 = Spanish, 1 = French, 2 = Arabic
    
    // Cosmetic list (head, face, accessory, pants, shirt, shoes)
    public static CosmeticItem[] currentCosmetics = new CosmeticItem[6];
    public static Color[] currentCosmeticColors = new Color[6];
    public static Color skinColor;

    public static int? currentPet = null;
    public static double[] petHunger = new double[8]; // num of pets
    public static bool[] petUnlocked = {true, true, true, true, true, false, true, true};

    public static float volume = 1f;
    public static bool canPlayerMove = true;
    [SerializeField] public Dictionary<CosmeticItem, bool> unlockedItems = new();
    public void NameIt(string n)
    {
        Name = n;
    }

    public void setCurrentLanguage(int language) {
        currentLanguage = language;
    }

    // Lesson = current lesson
    public static void progress(int lesson) {
        if(getProgress() == lesson-1) //so you progress linearly (can't do lesson 1 multiple times and unlock every single lesson)
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