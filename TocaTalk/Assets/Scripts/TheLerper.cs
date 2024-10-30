using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheLerper : MonoBehaviour
{
    public UnityEngine.UI.Image image;

    public Color start;
    public Color end;
    public void Start()
    {
        image.color = start;
    }
    public void theLerper(float c)
    {
        Color color = Color.Lerp(start, end, c);
        image.color = color;

    }
}
