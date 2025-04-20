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
                Holder.currentCosmetics[0] = (CosmeticItem)Resources.Load("Cosmetics/Head/" + reader.ReadLine());
                Holder.currentCosmetics[1] = (CosmeticItem)Resources.Load("Cosmetics/Face/" + reader.ReadLine());
                Holder.currentCosmetics[2] = (CosmeticItem)Resources.Load("Cosmetics/Accessory/" + reader.ReadLine());
                Holder.currentCosmetics[3] = (CosmeticItem)Resources.Load("Cosmetics/Pants/" + reader.ReadLine());
                Holder.currentCosmetics[4] = (CosmeticItem)Resources.Load("Cosmetics/Shirt/" + reader.ReadLine());
                Holder.currentCosmetics[5] = (CosmeticItem)Resources.Load("Cosmetics/Shoes/" + reader.ReadLine());
                
                r = float.Parse(reader.ReadLine());
                g = float.Parse(reader.ReadLine());
                b = float.Parse(reader.ReadLine());
                Holder.currentCosmeticColors[0] = new Color(r, g, b, 1f);
                r = float.Parse(reader.ReadLine());
                g = float.Parse(reader.ReadLine());
                b = float.Parse(reader.ReadLine());
                Holder.currentCosmeticColors[1] = new Color(r, g, b, 1f);
                r = float.Parse(reader.ReadLine());
                g = float.Parse(reader.ReadLine());
                b = float.Parse(reader.ReadLine());
                Holder.currentCosmeticColors[2] = new Color(r, g, b, 1f);
                r = float.Parse(reader.ReadLine());
                g = float.Parse(reader.ReadLine());
                b = float.Parse(reader.ReadLine());
                Holder.currentCosmeticColors[3] = new Color(r, g, b, 1f);
                r = float.Parse(reader.ReadLine());
                g = float.Parse(reader.ReadLine());
                b = float.Parse(reader.ReadLine());
                Holder.currentCosmeticColors[4] = new Color(r, g, b, 1f);
                r = float.Parse(reader.ReadLine());
                g = float.Parse(reader.ReadLine());
                b = float.Parse(reader.ReadLine());
                Holder.currentCosmeticColors[5] = new Color(r, g, b, 1f);
                for(int i = 0; i < Holder.petHunger.Length;i++) {
                    Holder.petHunger[i] = double.Parse(reader.ReadLine());
                }
                string s = reader.ReadLine();
                if(s == "null")
                    Holder.currentPet = null;
                else
                    Holder.currentPet = int.Parse(s);
                Holder.volume = float.Parse(reader.ReadLine());
            } else {
                Holder.Name = "";
                Holder.Money = 0;
                Holder.Sprogress = 0;
                Holder.Fprogress = 0;
                Holder.Aprogress = 0;
                Holder.currentLanguage = 0;      
                Holder.skinColor = new Color(0, 0, 0, 1f); // SKIN DEFAULT COLOR         
                Holder.currentCosmetics[0] = (CosmeticItem)Resources.Load("Cosmetics/Head/Bald");
                Holder.currentCosmetics[1] = (CosmeticItem)Resources.Load("Cosmetics/Face/Clean");
                Holder.currentCosmetics[2] = (CosmeticItem)Resources.Load("Cosmetics/Accessory/None");
                Holder.currentCosmetics[3] = (CosmeticItem)Resources.Load("Cosmetics/Pants/Jeans");
                Holder.currentCosmetics[4] = (CosmeticItem)Resources.Load("Cosmetics/Shirt/TShirt");
                Holder.currentCosmetics[5] = (CosmeticItem)Resources.Load("Cosmetics/Shoes/Sneakers");
                Holder.currentPet = null;
                for(int i = 0; i < Holder.petHunger.Length;i++) {
                    Holder.petHunger[i] = 50;
                }
                for(int i = 0; i < Holder.currentCosmetics.Length;i++) {
                    Holder.currentCosmeticColors[i] = Holder.currentCosmetics[i].DefaultColor;
                }
                Holder.volume = 1f;
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
            writer.WriteLine(Holder.currentCosmetics[0].name);
            writer.WriteLine(Holder.currentCosmetics[1].name);
            writer.WriteLine(Holder.currentCosmetics[2].name);
            writer.WriteLine(Holder.currentCosmetics[3].name);
            writer.WriteLine(Holder.currentCosmetics[4].name);
            writer.WriteLine(Holder.currentCosmetics[5].name);
            writer.WriteLine(Holder.currentCosmeticColors[0].r);
            writer.WriteLine(Holder.currentCosmeticColors[0].g);
            writer.WriteLine(Holder.currentCosmeticColors[0].b);
            writer.WriteLine(Holder.currentCosmeticColors[1].r);
            writer.WriteLine(Holder.currentCosmeticColors[1].g);
            writer.WriteLine(Holder.currentCosmeticColors[1].b);
            writer.WriteLine(Holder.currentCosmeticColors[2].r);
            writer.WriteLine(Holder.currentCosmeticColors[2].g);
            writer.WriteLine(Holder.currentCosmeticColors[2].b);
            writer.WriteLine(Holder.currentCosmeticColors[3].r);
            writer.WriteLine(Holder.currentCosmeticColors[3].g);
            writer.WriteLine(Holder.currentCosmeticColors[3].b);
            writer.WriteLine(Holder.currentCosmeticColors[4].r);
            writer.WriteLine(Holder.currentCosmeticColors[4].g);
            writer.WriteLine(Holder.currentCosmeticColors[4].b);
            writer.WriteLine(Holder.currentCosmeticColors[5].r);
            writer.WriteLine(Holder.currentCosmeticColors[5].g);
            writer.WriteLine(Holder.currentCosmeticColors[5].b);
            for(int i = 0; i < Holder.petHunger.Length;i++) {
                writer.WriteLine(Holder.petHunger[i]);
            }
            if(Holder.currentPet != null)
                writer.WriteLine(Holder.currentPet);
            else
                writer.WriteLine("null");
            writer.WriteLine(Holder.volume);
        }
    }
}
