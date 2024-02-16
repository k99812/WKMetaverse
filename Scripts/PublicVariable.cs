using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicVariable : MonoBehaviour
{
    public bool onEvent = false;
    public WeatherData WeatherInfo;
    public Texture WeatherIconTexture;
    private static System.DateTime dateTime;

    public System.DateTime GetDateTime() { return dateTime; }

    public void SetDateTime(System.DateTime InDateTime) { dateTime = InDateTime; }

    private void Update()
    {
        dateTime = System.DateTime.Now;
    }
}
