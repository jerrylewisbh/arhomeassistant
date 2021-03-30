using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeatherPanel : MonoBehaviour
{

    [SerializeField]
    public TMP_Text city;

    [SerializeField]
    public TMP_Text main;

    [SerializeField]
    public TMP_Text description;

    [SerializeField]
    public TMP_Text temperature;

    [SerializeField]
    public TMP_Text feelsLike;

    [SerializeField]
    public TMP_Text tempRange;

    [SerializeField]
    public TMP_Text pressure;

    [SerializeField]
    public TMP_Text humidity;

    [SerializeField]
    public TMP_Text wind;

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
}
