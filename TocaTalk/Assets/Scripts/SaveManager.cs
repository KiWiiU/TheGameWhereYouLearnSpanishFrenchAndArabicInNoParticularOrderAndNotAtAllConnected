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
                Holder.skinColor = new Color(r, g, b, 1f);
                Holder.currentHeadCosmetic = (CosmeticItem)Resources.Load("Cosmetics/Head/" + reader.ReadLine());
                Holder.currentFaceCosmetic = (CosmeticItem)Resources.Load("Cosmetics/Face/" + reader.ReadLine());
                Holder.currentPantsCosmetic = (CosmeticItem)Resources.Load("Cosmetics/Pants/" + reader.ReadLine());
                Holder.currentShirtCosmetic = (CosmeticItem)Resources.Load("Cosmetics/Shirt/" + reader.ReadLine());
                Holder.currentShoesCosmetic = (CosmeticItem)Resources.Load("Cosmetics/Shoes/" + reader.ReadLine());
                Holder.currentAccessoryCosmetic = (CosmeticItem)Resources.Load("Cosmetics/Accessory/" + reader.ReadLine());
                r = float.Parse(reader.ReadLine());
                g = float.Parse(reader.ReadLine());
                b = float.Parse(reader.ReadLine());
                Holder.currentHeadColor = new Color(r, g, b, 1f);
                r = float.Parse(reader.ReadLine());
                g = float.Parse(reader.ReadLine());
                b = float.Parse(reader.ReadLine());
                Holder.currentFaceColor = new Color(r, g, b, 1f);
                r = float.Parse(reader.ReadLine());
                g = float.Parse(reader.ReadLine());
                b = float.Parse(reader.ReadLine());
                Holder.currentPantsColor = new Color(r, g, b, 1f);
                r = float.Parse(reader.ReadLine());
                g = float.Parse(reader.ReadLine());
                b = float.Parse(reader.ReadLine());
                Holder.currentShirtColor = new Color(r, g, b, 1f);
                r = float.Parse(reader.ReadLine());
                g = float.Parse(reader.ReadLine());
                b = float.Parse(reader.ReadLine());
                Holder.currentShoesColor = new Color(r, g, b, 1f);
                r = float.Parse(reader.ReadLine());
                g = float.Parse(reader.ReadLine());
                b = float.Parse(reader.ReadLine());
                Holder.currentAccessoryColor = new Color(r, g, b, 1f);
            } else {
                Holder.Name = "";
                Holder.Money = 0;
                Holder.Sprogress = 0;
                Holder.Fprogress = 0;
                Holder.Aprogress = 0;
                Holder.currentLanguage = 0;
                Holder.skinColor = new Color(1f, 1f, 1f, 1f);
                Holder.currentHeadCosmetic = (CosmeticItem)Resources.Load("Cosmetics/0");
                Holder.currentFaceCosmetic = (CosmeticItem)Resources.Load("Cosmetics/0");
                Holder.currentPantsCosmetic = (CosmeticItem)Resources.Load("Cosmetics/0");
                Holder.currentShirtCosmetic = (CosmeticItem)Resources.Load("Cosmetics/0");
                Holder.currentShoesCosmetic = (CosmeticItem)Resources.Load("Cosmetics/0");
                Holder.currentAccessoryCosmetic = (CosmeticItem)Resources.Load("Cosmetics/0");
                Holder.currentHeadColor = new Color(1f, 1f, 1f, 1f);
                Holder.currentFaceColor = new Color(1f, 1f, 1f, 1f);
                Holder.currentPantsColor = new Color(1f, 1f, 1f, 1f);
                Holder.currentShirtColor = new Color(1f, 1f, 1f, 1f);
                Holder.currentShoesColor = new Color(1f, 1f, 1f, 1f);
                Holder.currentAccessoryColor = new Color(1f, 1f, 1f, 1f);
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
            writer.WriteLine(Holder.skinColor.r);
            writer.WriteLine(Holder.skinColor.g);
            writer.WriteLine(Holder.skinColor.b);
            writer.WriteLine(Holder.currentHeadCosmetic.name);
            writer.WriteLine(Holder.currentFaceCosmetic.name);
            writer.WriteLine(Holder.currentPantsCosmetic.name);
            writer.WriteLine(Holder.currentShirtCosmetic.name);
            writer.WriteLine(Holder.currentShoesCosmetic.name);
            writer.WriteLine(Holder.currentAccessoryCosmetic.name);
            writer.WriteLine(Holder.currentHeadColor.r);
            writer.WriteLine(Holder.currentHeadColor.g);
            writer.WriteLine(Holder.currentHeadColor.b);
            writer.WriteLine(Holder.currentFaceColor.r);
            writer.WriteLine(Holder.currentFaceColor.g);
            writer.WriteLine(Holder.currentFaceColor.b);
            writer.WriteLine(Holder.currentPantsColor.r);
            writer.WriteLine(Holder.currentPantsColor.g);
            writer.WriteLine(Holder.currentPantsColor.b);
            writer.WriteLine(Holder.currentShirtColor.r);
            writer.WriteLine(Holder.currentShirtColor.g);
            writer.WriteLine(Holder.currentShirtColor.b);
            writer.WriteLine(Holder.currentShoesColor.r);
            writer.WriteLine(Holder.currentShoesColor.g);
            writer.WriteLine(Holder.currentShoesColor.b);
            writer.WriteLine(Holder.currentAccessoryColor.r);
            writer.WriteLine(Holder.currentAccessoryColor.g);
            writer.WriteLine(Holder.currentAccessoryColor.b);
        }
    }
}
