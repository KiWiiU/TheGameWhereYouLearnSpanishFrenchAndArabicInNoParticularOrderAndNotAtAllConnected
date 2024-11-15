using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomNameGenerator : MonoBehaviour
{
    public TMP_InputField field;
    public void Generate() {
        string[] names = new string[] {
            "Naruto", "Sasuke", "Ichigo", "Luffy",
            "Light", "Edward", "Eren", "Goku",
            "Kirito", "Lelouch", "Saitama", "Deku",
            "Tanjiro", "Gon", "Killua", "Levi",
            "Kaneki", "Natsu", "Jonathan", "Nathan", 
            "L", "Ash", "Joshua", "Yugi",
            "Brian", "Vegeta", "Steven", "Kakashi",
            "Edward", "Itachi", "Peter", "Zoro",
            "Timothy", "Adam", "Stephen", "Eric",
            "Jason", "Jeffrey", "Gregory", "Scott",
            "Patrick"
        };
        field.text = names[Random.Range(0, names.Length)];
        Holder.Name = field.text;
    }

    public void Start() {
        
        field.interactable = true;
        field.text = Holder.Name;
    }
}
