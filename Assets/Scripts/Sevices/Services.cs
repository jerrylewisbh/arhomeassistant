﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public delegate void WebServiceEvent(WeatherData data);

public class Services : MonoBehaviour
{
    public static event WebServiceEvent OnWeatherRetrieved;
    public static event WebServiceEvent OnWeatherFailed;

    [SerializeField]
    private AppSettings settings;

    public void GetWeatherByLocation()
    {
        StartCoroutine(GetGeoLocation());
    }
    IEnumerator GetGeoLocation()
    {
        Input.location.Start();

        // Wait until service initializes
        int maxWait = settings.maxTimeToWait;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if (maxWait < 1)
        {
            OnWeatherFailed?.Invoke(null);
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            OnWeatherFailed?.Invoke(null);
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            StartCoroutine(GetWeather(Input.location.lastData.latitude, Input.location.lastData.longitude));
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }

    IEnumerator GetWeather(float latitute, float longitude)
    {

#if UNITY_EDITOR
        if (settings.devEnabled)
        {
            latitute = settings.lat;
            longitude = settings.lon;
        }
#endif

        string uri = string.Format(settings.APIEndpoint, latitute, longitude, settings.unit, settings.APIKey);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {

            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                OnWeatherFailed?.Invoke(null);
            }
            else
            {

                WeatherData data = JsonUtility.FromJson<WeatherData>(webRequest.downloadHandler.text);
                OnWeatherRetrieved?.Invoke(data);
            }
        }

    }
}