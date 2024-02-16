using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSkyBox
{
    private PublicVariable publicVariable;
    private List<Material> SkyBoxMaterial = new List<Material>();
    private System.DateTime SunSetTime;
    private System.DateTime SunRiseTime;
    private System.DateTime NowTime;

    System.Func<int, System.DateTime> IntToDateTime = InTime => new System.DateTime(1970, 1, 1, 9, 0, 0).AddSeconds(InTime);

    public SetSkyBox()
    {
        publicVariable = GameObject.Find("Public Variable").GetComponent<PublicVariable>();
        SkyBoxMaterial.Add(Resources.Load<Material>("Skyboxes/TFF_Skybox_Day_01A"));
        SkyBoxMaterial.Add(Resources.Load<Material>("Skyboxes/TFF_Skybox_Night_01A"));
        SkyBoxMaterial.Add(Resources.Load<Material>("Skyboxes/TFF_Skybox_Sunset_01A"));
    }

    public void Initiallize(int SunriseTime, int SunsetTime)
    {
        publicVariable.SetDateTime(System.DateTime.Now);
        //Debug.Log(SkyBoxMaterial.Count);
        NowTime = publicVariable.GetDateTime();
        SunRiseTime = IntToDateTime(SunriseTime);
        SunSetTime = IntToDateTime(SunsetTime);
        SetSky();
    }

    private void SetSky()
    {
        if (SunRiseTime <= NowTime && NowTime <= SunSetTime)
        {
            RenderSettings.skybox = SkyBoxMaterial[0];
        }
        else if (NowTime >= SunSetTime)
        {
            RenderSettings.skybox = SkyBoxMaterial[1];
        }
        else if ((0 <= NowTime.Hour && NowTime <= SunRiseTime) ||
            SunSetTime.Hour + 1 <= NowTime.Hour)
        {
            RenderSettings.skybox = SkyBoxMaterial[2];
        }
    }
}
