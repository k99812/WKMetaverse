
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

using System;
using TMPro;

public class WeatherRev : MonoBehaviour
{
    private string APP_ID = "61a16d96f635fa70ac76f98964dc3ea1";
    private PublicVariable WeatherData;
    private SetSkyBox setSky;

    private void Awake()
    {
        WeatherData = GetComponent<PublicVariable>();
        setSky = new SetSkyBox();
        CheckCityWeather("Iksan");
    }

    private void Update()
    {
        if (System.DateTime.Now.Minute == 0)
        {
            CheckCityWeather("Iksan");
        }
    }
    public void CheckCityWeather(string city)
    {
        StartCoroutine(GetWeather(city));
    }

    IEnumerator GetWeather(string city)
    {
        city = UnityWebRequest.EscapeURL(city);
        string url = "http://api.openweathermap.org/data/2.5/weather?q="+city+"&units=metric&appid="+APP_ID+"&lang=kr&units=metric";

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        string json = www.downloadHandler.text;
        json = json.Replace("\"base\":", "\"basem\":");
        WeatherData.WeatherInfo = JsonUtility.FromJson<WeatherData>(json);
        if (WeatherData.WeatherInfo.weather.Length > 0)
        {
            StartCoroutine(GetWeatherIcon(WeatherData.WeatherInfo.weather[0].icon));
        }
        setSky.Initiallize(WeatherData.WeatherInfo.sys.sunrise, WeatherData.WeatherInfo.sys.sunset);
    }

    IEnumerator GetWeatherIcon(string icon)
    {
        //http://openweathermap.org/img/wn/10d@2x.png
        string url = "http://openweathermap.org/img/wn/" + icon + "@2x.png";
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        WeatherData.WeatherIconTexture = DownloadHandlerTexture.GetContent(www);
    }
}