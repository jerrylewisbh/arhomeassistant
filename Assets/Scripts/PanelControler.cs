using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControler : MonoBehaviour
{
    [SerializeField]
    private GameObject weatherPanel;

    [SerializeField]
    private Services services;



    public void ToggleLight()
    {
        services.ChangeLightState(services.lightState ? "off": "on", "white", 1);
    }
    public void ToggleWeatherPanel()
    {
        weatherPanel.SetActive(!weatherPanel.activeInHierarchy);
    }
}
