using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBSliderController : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    public void SetMaxAmmo(int value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void SetAmmo(int value)
    {
        slider.value = value;
    }
}
