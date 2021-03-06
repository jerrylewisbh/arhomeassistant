using System.Collections.Generic;
[System.Serializable]
public class Coord
{
    public double lon;
    public double lat;
}
[System.Serializable]
public class Weather
{
    public int id;
    public string main;
    public string description;
    public string icon;
}
[System.Serializable]
public class Main
{
    public double temp;
    public double feels_like;
    public double temp_min;
    public double temp_max;
    public int pressure;
    public int humidity;
}
[System.Serializable]
public class Wind
{
    public double speed;
    public int deg;
}

[System.Serializable]
public class Clouds
{
    public int all;
}

[System.Serializable]
public class Sys
{
    public int type;
    public int id;
    public string country;
    public int sunrise;
    public int sunset;
}

[System.Serializable]
public class WeatherData
{
    public Coord coord;
    public List<Weather> weather;
    public string @base;
    public Main main;
    public int visibility;
    public Wind wind;
    public Clouds clouds;
    public int dt;
    public Sys sys;
    public int timezone;
    public int id;
    public string name;
    public int cod;
}

