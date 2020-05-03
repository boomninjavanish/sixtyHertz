using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSliderLabelValues : MonoBehaviour
{
   public Text label;

    public void UpdateSliderLabelValue(float value)
    {
        // round to two decimal places
        float mult = Mathf.Pow(10.0f, 2.0f);
        label.text = (Mathf.Round(value * mult) / mult).ToString();
    }
}