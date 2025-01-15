using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class SaveManager : MonoBehaviour
{
    void Awake()
    {
        float r, g, b;
        // Reads in the save file
        if (!File.Exists("Assets/SaveFile.txt")) {
            File.Create("Assets/SaveFile.txt").Close();
            Holder.currentHeadCosmetic = (UnlockableItem)Resources.Load("Cosmetics/0");
        }
        using(StreamReader reader = new StreamReader("Assets/SaveFile.txt"))
        {
            if (!reader.EndOfStream)
            {
                Holder.Name = reader.ReadLine();
                Holder.Money = int.Parse(reader.ReadLine());
                Holder.Sprogress = int.Parse(reader.ReadLine());
                Holder.Fprogress = int.Parse(reader.ReadLine());
                Holder.Aprogress = int.Parse(reader.ReadLine());
                Holder.currentLanguage = int.Parse(reader.ReadLine());
                r = float.Parse(reader.ReadLine());
                g = float.Parse(reader.ReadLine());
                b = float.Parse(reader.ReadLine());
                Holder.color = new Color(r, g, b, 1f);
                Holder.currentHeadCosmetic = (UnlockableItem)Resources.Load("Cosmetics/" + int.Parse(reader.ReadLine()));
                // Holder.currentAccessoryCosmetic = (UnlockableItem)Resources.Load("Cosmetics/" + int.Parse(reader.ReadLine()));
                // Holder.currentFaceCosmetic = (UnlockableItem)Resources.Load("Cosmetics/" + int.Parse(reader.ReadLine()));
                // Holder.currentPantsCosmetic = (UnlockableItem)Resources.Load("Cosmetics/" + int.Parse(reader.ReadLine()));
                // Holder.currentShirtCosmetic = (UnlockableItem)Resources.Load("Cosmetics/" + int.Parse(reader.ReadLine()));
                // Holder.currentShoesCosmetic = (UnlockableItem)Resources.Load("Cosmetics/" + int.Parse(reader.ReadLine()));
            }
        }
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
            writer.WriteLine(Holder.currentLanguage);
            writer.WriteLine(Holder.color.r);
            writer.WriteLine(Holder.color.g);
            writer.WriteLine(Holder.color.b);
            writer.WriteLine((int)Holder.currentHeadCosmetic.Type);
            // writer.WriteLine((int)Holder.currentAccessoryCosmetic.Type);
            // writer.WriteLine((int)Holder.currentFaceCosmetic.Type);
            // writer.WriteLine((int)Holder.currentPantsCosmetic.Type);
            // writer.WriteLine((int)Holder.currentShirtCosmetic.Type);
            // writer.WriteLine((int)Holder.currentShoesCosmetic.Type);
        }
    }
}
