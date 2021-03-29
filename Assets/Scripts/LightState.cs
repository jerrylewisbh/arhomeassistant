using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LightState 
{
    public string power;
    public string color;
    public float brightness;
    public bool fast = true;


    public LightState(string power, string color, float brightness)
    {
        this.power = power;
        this.color = color;
        this.brightness = brightness;
    }
}
