using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BarsStats
{
    [SerializeField]
    private Bars bar;

    public float maxValue1;
    public float currentValue1;


    public void UpdateValue()
    {
        bar.maxValue = maxValue1;
        bar.currentValue = currentValue1;
        bar.UpdateBarFill();
    }

}
