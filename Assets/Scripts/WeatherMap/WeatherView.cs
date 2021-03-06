using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class WeatherDescriptor
{
    public int minRange;
    public int maxRange;
    public bool hidesSky = false;
    public GameObject reference;
}

public class WeatherView : MonoBehaviour
{

    [SerializeField]
    private List<WeatherDescriptor> weatherReferences;
    [SerializeField]
    private GameObject day;
    [SerializeField]
    private GameObject night;

    [SerializeField]
    private Services services;

    [SerializeField]
    private AppSettings settings;

    [SerializeField]
    private WeatherPanel panel;


    private void Awake()
    {
        panel = FindObjectOfType<WeatherPanel>();
    }

    private void Start()
    {
        DisableAll();
    }

    public void OnEnable()
    {

        services = FindObjectOfType<Services>();

        if (settings == null || services == null)
        {
            return;
        }

        Services.OnWeatherRetrieved += OnWeatherRetrieved;
        Services.OnWeatherFailed += OnWeatherFailed;

        StartCoroutine(UpdateWeatherState());
    }

    private void OnWeatherRetrieved(WeatherData data)
    {
        DisableAll();
        EnableWeatherRepresentation(data.weather[0].id, data.dt, data.sys.sunrise, data.sys.sunset);
        panel.UpdateData(data);
        StartCoroutine(GerURLIcon(data.weather[0].icon));
    }
    private void OnWeatherFailed(WeatherData data)
    {
        Debug.LogError("Unable to retrieve data");
    }

    public void OnDisable()
    {
        Services.OnWeatherRetrieved -= OnWeatherRetrieved;
        Services.OnWeatherFailed -= OnWeatherFailed;
        StopAllCoroutines();
    }

    public void EnableWeatherRepresentation(int code, int currentTime, int sunRise, int sunSet)
    {

        WeatherDescriptor active = weatherReferences.Find(item => code >= item.minRange && code <= item.maxRange);

        if (active != null)
        {
            active.reference.SetActive(true);
            if (active.hidesSky)
            {
                day.SetActive(false);
                night.SetActive(false);
            }
            else
            {
                SetTimeofDay(currentTime, sunRise, sunSet);

            }

        }
    }

    public void SetTimeofDay(int currentTime, int sunRise, int sunSet)
    {
        if (currentTime >= sunRise && currentTime <= sunSet)
        {
            day.SetActive(true);
        }
        else
        {
            night.SetActive(true);
        }
    }

    public void DisableAll()
    {
        foreach (var item in weatherReferences)
        {
            item.reference.SetActive(false);
        }

        day.SetActive(false);
        night.SetActive(false);
    }

    private IEnumerator UpdateWeatherState()
    {
        while (true)
        {
            services.GetWeatherByLocation();
            yield return new WaitForSeconds(settings.updateInterval);
        }
    }

    IEnumerator GerURLIcon(string icon)
    {
        using (var www = UnityWebRequestTexture.GetTexture($"https://openweathermap.org/img/wn/{icon}@4x.png"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    var texture = DownloadHandlerTexture.GetContent(www);
                    var rect = new Rect(0, 0, 200, 200);
                    var sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
                    panel.SetIcon(sprite);

                }
            }


        }
    }
}
