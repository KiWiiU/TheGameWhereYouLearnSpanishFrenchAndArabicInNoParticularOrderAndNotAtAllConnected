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
            Holder.currentHeadCosmetic = (CosmeticItem)Resources.Load("Cosmetics/0");
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
                Holder.currentHeadCosmetic = (CosmeticItem)Resources.Load("Cosmetics/Head/" + reader.ReadLine());
                Holder.currentFaceCosmetic = (CosmeticItem)Resources.Load("Cosmetics/Face/" + reader.ReadLine());
                Holder.currentPantsCosmetic = (CosmeticItem)Resources.Load("Cosmetics/Pants/" + reader.ReadLine());
                Holder.currentShirtCosmetic = (CosmeticItem)Resources.Load("Cosmetics/Shirt/" + reader.ReadLine());
                Holder.currentShoesCosmetic = (CosmeticItem)Resources.Load("Cosmetics/Shoes/" + reader.ReadLine());
                Holder.currentAccessoryCosmetic = (CosmeticItem)Resources.Load("Cosmetics/Accessory/" + reader.ReadLine());
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
            writer.WriteLine(Holder.currentHeadCosmetic.name);
            writer.WriteLine(Holder.currentFaceCosmetic.name);
            writer.WriteLine(Holder.currentPantsCosmetic.name);
            writer.WriteLine(Holder.currentShirtCosmetic.name);
            writer.WriteLine(Holder.currentShoesCosmetic.name);
            writer.WriteLine(Holder.currentAccessoryCosmetic.name);
        }
    }
}
