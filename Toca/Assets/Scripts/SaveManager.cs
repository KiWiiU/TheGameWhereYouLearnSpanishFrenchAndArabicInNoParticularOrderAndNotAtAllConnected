using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class SaveManager : MonoBehaviour
{
    public TMP_InputField TMP_Input;
    void Start()
    {
        float r, g, b;
        // Reads in the save file
        if (!File.Exists("Assets/SaveFile.txt"))
            File.Create("Assets/SaveFile.txt").Close();
        using(StreamReader reader = new StreamReader("Assets/SaveFile.txt"))
        {
            if (!reader.EndOfStream)
            {
                Holder.Name = reader.ReadLine();
                Holder.Money = int.Parse(reader.ReadLine());
                Holder.Sprogress = int.Parse(reader.ReadLine());
                Holder.Fprogress = int.Parse(reader.ReadLine());
                Holder.Aprogress = int.Parse(reader.ReadLine());
                r = float.Parse(reader.ReadLine());
                g = float.Parse(reader.ReadLine());
                b = float.Parse(reader.ReadLine());
                Holder.color = new Color(r, g, b, 1f);
            }
        }

        TMP_Input.interactable = true;
        TMP_Input.text = Holder.Name;
    }

    public static void Save()
    {
        using(StreamWriter writer = new StreamWriter("Assets/SaveFile.txt"))
        {
            writer.WriteLine(Holder.Name);
            writer.WriteLine(Holder.Money);
            writer.WriteLine(Holder.Sprogress);
            writer.WriteLine(Holder.Fprogress);
            writer.WriteLine(Holder.Aprogress);
            writer.WriteLine(Holder.color.r);
            writer.WriteLine(Holder.color.g);
            writer.WriteLine(Holder.color.b);
        }
    }
}
