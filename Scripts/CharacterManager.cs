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

    private Dictionary<int, GameObject> CharacterMap = new Dictionary<int, GameObject>();

    private const int Young_Boy = 1, Boy = 2, Girl = 3;

    [SerializeField]
    private List<string> CharacterNames = new List<string>() { "None", "Player_V9_CamTest", 
        "Player_Man_V9_CamTest", "Player_Girl_V9_CamTest" };

    private int Character_Code = Young_Boy;

    //다른 클래스에서 접근은 CharacterManager.Instance.접근함수 or 변수
    public static CharacterManager Instance { get; private set;  }

    [Space(10f)]
    public string roomNum = "";
    public string playerName = "";

    [Space(10f)]
    public int lastPotalNum = 0;
    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
       
    }

    private void Start()
    {
        CharacterMap[Young_Boy] = Resources.Load<GameObject>(CharacterNames[Young_Boy]);
        CharacterMap[Boy] = Resources.Load<GameObject>(CharacterNames[Boy]);
        CharacterMap[Girl] = Resources.Load<GameObject>(CharacterNames[Girl]);
    }

    public void On_Character_Code(int code)
    {
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

    private void off_Chracters()
    {
        foreach (var ch in Characters)
        {
            ch.SetActive(false);
        }
    }

    public string GetSelectedCharacterName() { return CharacterMap[Character_Code].name; }

    public GameObject GetSelectedCharacter() { return CharacterMap[Character_Code]; }

    public void Set_OnOff(int InCode)
    {
        onOffSet = InCode == 1 ? OnOffSet.OnLine : OnOffSet.OffLine;
    }
}
