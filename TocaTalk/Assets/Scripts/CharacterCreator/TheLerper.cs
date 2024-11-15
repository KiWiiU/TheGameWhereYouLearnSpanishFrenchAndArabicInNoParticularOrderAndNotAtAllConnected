using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheLerper : MonoBehaviour
{
    public Image image;

    public Gradient gradient;
    public void Start()
    {
        if(Holder.color.Equals(new Color(0,0,0,0.0f))) {
            image.color = gradient.Evaluate(0);
            GetComponent<Slider>().value = 0;
        }
        else {
            image.color = Holder.color;
            GetComponent<Slider>().value = FindGradientPosition(image.color, gradient);
        }
    }
    private float FindGradientPosition(Color targetColor, Gradient gradient)
    {
        float bestPosition = 0;
        float smallestDifference = float.MaxValue;
        const int samples = 50;
        for (int i = 0; i <= samples; i++)
        {
            float position = i / (float)samples;
            Color sampleColor = gradient.Evaluate(position);
            float difference = Mathf.Abs(sampleColor.r - targetColor.r) +
                             Mathf.Abs(sampleColor.g - targetColor.g) +
                             Mathf.Abs(sampleColor.b - targetColor.b);
            if (difference < smallestDifference)
            {
                smallestDifference = difference;
                bestPosition = position;
            }
        }
        //not exact, but close enough
        return bestPosition;
    }
    public void theLerper(float c)
    {
        Color color = gradient.Evaluate(c);
        image.color = color;
        Holder.color = color;
    }
}
