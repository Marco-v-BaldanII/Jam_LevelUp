using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicManager : MonoBehaviour
{
    public int valueToReset = 0;
    public Slider slider;

    public void resetSlider()
    {
        slider.value = valueToReset;
    }
}
