using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheLerper : MonoBehaviour
{
    public SpriteRenderer player;

    public Gradient gradient;
    public void Start()
    {
        if(Holder.skinColor.Equals(new Color(0, 0, 0, 1f))) {
            player.color = gradient.Evaluate(0);
            GetComponent<Slider>().value = 0;
            Holder.skinColor = player.color;
        }
        else {
            player.color = Holder.skinColor;
            GetComponent<Slider>().value = FindGradientPosition(player.color, gradient);
        }
    }

    // sample based search to find nearest color in gradient
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
        player.color = color;
        Holder.skinColor = color;
    }
}
