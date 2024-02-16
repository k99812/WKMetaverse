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
        AverageTemp.text = WeatherData.WeatherInfo.main.temp.ToString() + " ��C";
        FeelsLikeTemp.text = WeatherData.WeatherInfo.main.feels_like.ToString("N1") + " ��C";
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
                Description.text = "����";
                WeatherImg.texture = WeatherSprites[0];
                break;

            case "few clouds":
                Description.text = "���� ����";
                WeatherImg.texture = WeatherSprites[1];
                break;

            case "scattered clouds":
                Description.text = "���� ����";
                WeatherImg.texture = WeatherSprites[2];
                break;

            case "broken clouds":
                Description.text = "�帲";
                WeatherImg.texture = WeatherSprites[3];
                break;

            case "shower rain":
                Description.text = "�ҳ���";
                WeatherImg.texture = WeatherSprites[4];
                break;

            case "rain":
                Description.text = "��";
                WeatherImg.texture = WeatherSprites[5];
                break;

            case "thunderstorm":
                Description.text = "õ�չ���";
                WeatherImg.texture = WeatherSprites[6];
                break;

            case "snow":
                Description.text = "��";
                WeatherImg.texture = WeatherSprites[7];
                break;

            case "mist":
                Description.text = "�Ȱ�";
                WeatherImg.texture = WeatherSprites[8];
                break;
        }
    }

    private string GetWindDeg()
    {
        int Dir = WeatherData.WeatherInfo.wind.dir;

        if (Dir >= 340 || Dir <= 20)
        {
            return "��";
        }
        else if (Dir <= 70)
        {
            if (Dir <= 40)
                return "��/�ϵ�";
            else if (Dir <= 60)
                return "�ϵ�";
            else
                return "��/�ϵ�";
        }
        else if (Dir <= 160)
        {
            if (Dir <= 110)
                return "��";
            else if (Dir <= 130)
                return "��/����";
            else if (Dir <= 150)
                return "����";
            else
                return "��/����";
        }
        else if (Dir <= 250)
        {
            if (Dir <= 200)
                return "��";
            else if (Dir <= 220)
                return "��/����";
            else if (Dir <= 240)
                return "����";
            else
                return "��/����";
        }
        else
        {
            if (Dir <= 290)
                return "��";
            else if (Dir <= 310)
                return "��/�ϼ�";
            else if (Dir <= 330)
                return "�ϼ�";
            else
                return "��/�ϼ�";
        }

    }
}
