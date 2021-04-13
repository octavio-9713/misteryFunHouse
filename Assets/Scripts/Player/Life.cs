using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [Header("Life Slider")]
    public Slider slider;

    [Header("Life Colors")]
    public Color fullLifeColor;
    public Color halfLifeColor;
    public Color lastLifeColor;

    public Image fill;


    public void SetLifeTo(int quantity)
    {
        slider.value = quantity;

        if (slider.value > slider.maxValue / 2 )
            fill.color = fullLifeColor;

        else
        {
            if (slider.value > slider.maxValue / 4)
                fill.color = halfLifeColor;

            else
                fill.color = lastLifeColor;

        }
        
    }

    public void ChangeMaxLife(int quantity, bool recover)
    {
        slider.maxValue = quantity;

        if (recover)
            SetLifeTo(quantity);
    }
}
