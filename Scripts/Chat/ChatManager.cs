using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public GameObject MineArea, OtherArea;
    public RectTransform ContentRect;
    public Scrollbar scrollbar;
    AreaScript Last_Area;

    [SerializeField]
    private InputField messagesInput;
    [SerializeField]
    PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    //�����ڷ� https://www.youtube.com/watch?v=iARzkDbhA8k
    public void chat(bool isSend, string text, string user, Texture picture)
    {
        //Tring() �����̽��� ���๮�� ������ => �����̸� ����
        if (text.Trim() == "") return;

        //ä���� �������
        bool isBottom = scrollbar.value <= 0.00001f;

        //ä�û���
        AreaScript Area = Instantiate(isSend ? MineArea : OtherArea).GetComponent<AreaScript>();
        Area.transform.SetParent(ContentRect.transform, false);
        //ä�� ù������ 600���� ����
        Area.Box_Rect.sizeDelta= new Vector2(600, Area.Box_Rect.sizeDelta.y);
        Area.Text_Rect.GetComponent<TMP_Text>().text = text;
        Fit(Area.Box_Rect);

        //ũ�⿡ �°� ä�� ������ ����
        float x = Area.Text_Rect.sizeDelta.x + 42;
        float y = Area.Text_Rect.sizeDelta .y;

        if (y > 49)
        {
            for (int i = 0; i < 200; i++)
            {
                Area.Box_Rect.sizeDelta = new Vector2(x - i * 2, Area.Box_Rect.sizeDelta.y);
                Fit(Area.Box_Rect);

                if (y != Area.Text_Rect.sizeDelta.y) { Area.Box_Rect.sizeDelta = new Vector2(x - (i * 2) + 2, y); break; }
            }
        }
        else Area.Box_Rect.sizeDelta = new Vector2(x, y);

        //�ð�����
        DateTime time = DateTime.Now;
        Area.Time = time.ToString("yyyy-MM-dd-HH-mm");
        Area.User = user;

        int hour = time.Hour;
        if(time.Hour== 0) { hour = 12; }
        else if (time.Hour > 12) { hour -= 12; }
        Area.Time_Text.text = (time.Hour > 12 ? "����" : "����") + hour + ":" + time.Minute.ToString("D2");

        //���� ä���̶� ������ ������ ���� ����
        bool isSame = Last_Area != null && Last_Area.Time == Area.Time && Last_Area.User == Area.User;
        if (isSame) Last_Area.Time_Text.text = "";
        Area.Tail.SetActive(!isSame);

        if (!isSend)
        {
            Area.User_Image.gameObject.SetActive(!isSame);
            Area.User_Text.gameObject.SetActive(!isSame);
            Area.User_Text.text = Area.User;
        }

        Fit(Area.Box_Rect);
        Fit(Area.Area_Rect);
        Fit(ContentRect);
        Last_Area= Area;
    }

    void Fit(RectTransform rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(rect);

    public void sendMessage()
    {
        chat(true, messagesInput.text, PhotonNetwork.LocalPlayer.NickName, null);
        photonView.RPC("send_RPC_Message", RpcTarget.Others, messagesInput.text);
        messagesInput.ActivateInputField();
        messagesInput.text = "";
    }

    [PunRPC]
    public void send_RPC_Message(string message)
    {
        chat(false, message, PhotonNetwork.LocalPlayer.NickName, null);
    }
}
