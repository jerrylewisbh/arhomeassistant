using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeatherPanel : MonoBehaviour
{

    [SerializeField]
    private Image icon;


    [SerializeField]
    private TMP_Text city;

    [SerializeField]
    private TMP_Text main;

    [SerializeField]
    private TMP_Text description;

    [SerializeField]
    private TMP_Text temperature;

    [SerializeField]
    private TMP_Text feelsLike;

    [SerializeField]
    private TMP_Text tempRange;

    [SerializeField]
    private TMP_Text pressure;

    [SerializeField]
    private TMP_Text humidity;

    [SerializeField]
    private TMP_Text wind;

    public void UpdateData(WeatherData data)
    {

        city.text = data.name;
        main.text = data.weather[0].main;
        description.text = data.weather[0].description;
        temperature.text = data.main.temp + " º";
        tempRange.text = data.main.temp_min + "º / " + data.main.temp_max + "º";
        feelsLike.text = "Feels like: " + data.main.feels_like + "º C";
        humidity.text = "Humidity: " + data.main.humidity + "%";
        pressure.text = "Pressure: " + data.main.pressure.ToString();
        wind.text = "Wind: " + data.wind.speed + "Km/h";
    }

    public void SetIcon(Sprite image)
    {
        icon.sprite = image;
    }
}
