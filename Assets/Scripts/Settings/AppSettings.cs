using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppSettings : ScriptableObject
{
    [Header("Weather API")]
    public string APIEndpoint = "https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units={2}&appid={3}";
    public string APIKey = "17c2675a2edd4cec3a5c68b867ae440f";
    [Tooltip("Time (in seconds) to query for weather data again")]
    public int updateInterval = 60;
    [Tooltip("Measurement unit: metric, standard or imperial")]
    public string unit = "metric";


    [Header("Geolocation")]
    [Tooltip("Timeout (in seconds) to wait for GPS access")]
    public int maxTimeToWait = 20;

    [Header("DEV")]
    public bool devEnabled = false;

    [Tooltip("City to be used while testing without a GPS")]
    public float lat = -23.5475f;
    public float lon = -46.6361f;

}
