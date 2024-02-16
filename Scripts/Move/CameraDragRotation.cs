using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class CameraDragRotation : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField]
    private PhotonView PV;
    [SerializeField]
    public Transform cameraArm;

    Vector3 FirstPoint;
    Vector3 SecondPoint;
    public float xAngle;
    public float yAngle;
    float xAngleTemp;
    float yAngleTemp;

    [SerializeField]
    private float limit_yAngle_lest = -30f; //ī�޶��� ���� ���̸� ���� �� �� �� �ְ� �� 
    [SerializeField]
    private float limit_yAngle_MAX = 70;

    private void Start()
    {
        xAngle = cameraArm.position.x;
        yAngle = cameraArm.position.y;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!PV.IsMine) return;
        BeginDrag(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!PV.IsMine) return;
        OnDrag(eventData.position);       
    }

    public void BeginDrag(Vector2 a_FirstPoint)
    {
        FirstPoint = a_FirstPoint;
        xAngleTemp = xAngle;
        yAngleTemp = yAngle;
    }

    public void OnDrag(Vector2 a_SecondPoint)
    {
        SecondPoint = a_SecondPoint;
        xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
        yAngle = yAngleTemp - (SecondPoint.y - FirstPoint.y) * 90 * 3f / Screen.height; // Y�� ��ȭ�� �� ������ 3�� ������.

        // ȸ������ 40~85�� ����
        if (yAngle < limit_yAngle_lest)
            yAngle = limit_yAngle_lest;
        if (yAngle > limit_yAngle_MAX)
            yAngle = limit_yAngle_MAX;

        cameraArm.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
    }
}
