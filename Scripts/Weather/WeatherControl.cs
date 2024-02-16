using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeatherControl : MonoBehaviour
{
    [SerializeField]
    private RawImage WeatherImg;
    [SerializeField]
    private TMP_Text FeelsLikeTemp;
    [SerializeField]
    private TMP_Text AverageTemp;
    [SerializeField]
    private TMP_Text Pressure;
    [SerializeField]
    private TMP_Text Humidity;
    [SerializeField]
    private TMP_Text WindText;
    [SerializeField]
    private TMP_Text Description;
    private PublicVariable WeatherData;
    private string WeatherName;
    [SerializeField]
    private List<Texture> WeatherSprites;

    private void Awake()
    {
        WeatherData = GameObject.Find("Public Variable").GetComponent<PublicVariable>();
    }

    private void Start()
    {
        SetWeatherDataText();
    }

    public void SetWeatherDataText()
    {
        AverageTemp.text = WeatherData.WeatherInfo.main.temp.ToString() + " ¡ÆC";
        FeelsLikeTemp.text = WeatherData.WeatherInfo.main.feels_like.ToString("N1") + " ¡ÆC";
        Pressure.text = WeatherData.WeatherInfo.main.pressure.ToString() + " mb";
        Humidity.text = WeatherData.WeatherInfo.main.humidity.ToString() + "%";
        WindText.text = GetWindDeg() + " " + WeatherData.WeatherInfo.wind.speed.ToString() + " m/s";
        WeatherName = WeatherData.WeatherInfo.weather[0].description.ToString();
        SetDesText();
    }

    private void SetDesText()
    {
        switch (WeatherName)
        {
            case "clear sky":
                Description.text = "¸¼À½";
                WeatherImg.texture = WeatherSprites[0];
                break;

            case "few clouds":
                Description.text = "±¸¸§ Á¶±Ý";
                WeatherImg.texture = WeatherSprites[1];
                break;

            case "scattered clouds":
                Description.text = "±¸¸§ ¸¹À½";
                WeatherImg.texture = WeatherSprites[2];
                break;

            case "broken clouds":
                Description.text = "Èå¸²";
                WeatherImg.texture = WeatherSprites[3];
                break;

            case "shower rain":
                Description.text = "¼Ò³ª±â";
                WeatherImg.texture = WeatherSprites[4];
                break;

            case "rain":
                Description.text = "ºñ";
                WeatherImg.texture = WeatherSprites[5];
                break;

            case "thunderstorm":
                Description.text = "ÃµµÕ¹ø°³";
                WeatherImg.texture = WeatherSprites[6];
                break;

            case "snow":
                Description.text = "´«";
                WeatherImg.texture = WeatherSprites[7];
                break;

            case "mist":
                Description.text = "¾È°³";
                WeatherImg.texture = WeatherSprites[8];
                break;
        }
    }

    private string GetWindDeg()
    {
        int Dir = WeatherData.WeatherInfo.wind.dir;

        if (Dir >= 340 || Dir <= 20)
        {
            return "ºÏ";
        }
        else if (Dir <= 70)
        {
            if (Dir <= 40)
                return "ºÏ/ºÏµ¿";
            else if (Dir <= 60)
                return "ºÏµ¿";
            else
                return "µ¿/ºÏµ¿";
        }
        else if (Dir <= 160)
        {
            if (Dir <= 110)
                return "µ¿";
            else if (Dir <= 130)
                return "µ¿/³²µ¿";
            else if (Dir <= 150)
                return "³²µ¿";
            else
                return "³²/³²µ¿";
        }
        else if (Dir <= 250)
        {
            if (Dir <= 200)
                return "³²";
            else if (Dir <= 220)
                return "³²/³²¼­";
            else if (Dir <= 240)
                return "³²¼­";
            else
                return "¼­/³²¼­";
        }
        else
        {
            if (Dir <= 290)
                return "¼­";
            else if (Dir <= 310)
                return "¼­/ºÏ¼­";
            else if (Dir <= 330)
                return "ºÏ¼­";
            else
                return "ºÏ/ºÏ¼­";
        }

    }
}
