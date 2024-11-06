using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheLerper : MonoBehaviour
{
    public Image image;

    public Color start;
    public Color end;
    public void Start()
    {
        if(Holder.color.Equals(new Color(0,0,0,0.0f))) {
            image.color = start;
            GetComponent<Slider>().value = 0;
        }
        else {
            image.color = Holder.color;
            GetComponent<Slider>().value = Mathf.InverseLerp(start.r, end.r, image.color.r);
        }
    }
    public void theLerper(float c)
    {
        Color color = Color.Lerp(start, end, c);
        image.color = color;
        Holder.color = color;
    }
}
