using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public enum OnOffSet
    {
        OnLine, OffLine
    }
    public OnOffSet onOffSet = OnOffSet.OnLine;

    [SerializeField]
    public List<GameObject> Characters = new List<GameObject>();

    private const int Young_Boy = 1, Boy = 2, Girl = 3;

    private int Character_Code = Young_Boy;

    private static CharacterManager instance = null;

    [Space(10f)]
    public string roomNum = "";
    public string playerName = "";

    [Space(10f)]
    public int lastPotalNum = 0;
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
       
    }

    public void On_Character_Code(int code)
    {
        print(code); 
        switch (code)
        {
            case Young_Boy:
                Character_Code = Young_Boy;
                off_Chracters();
                Characters[code-1].SetActive(true);
                break;
            case Boy:
                Character_Code = Boy;
                off_Chracters();
                Characters[code - 1].SetActive(true);
                break;
            case Girl:
                Character_Code = Girl;
                off_Chracters();
                Characters[code - 1].SetActive(true);
                break;
        }
    }

    public int get_Character_Code() { return Character_Code; }

    private void off_Chracters()
    {
        foreach(var ch in Characters) 
        {
            ch.SetActive(false);
        }
    }

    public void Set_OnOff(int InCode)
    {
        onOffSet = InCode == 1 ? OnOffSet.OnLine : OnOffSet.OffLine;
    }
}
