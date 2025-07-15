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
    private float limit_yAngle_lest = -30f; //카메라의 값을 줄이면 위로 더 볼 수 있게 됨 
    [SerializeField]
    private float limit_yAngle_MAX = 70;

    private void Start()
    {
        xAngle = 0;
        yAngle = 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (PV != null && !PV.IsMine) return;
        BeginDrag(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (PV != null && !PV.IsMine) return;
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
        yAngle = yAngleTemp - (SecondPoint.y - FirstPoint.y) * 90 * 3f / Screen.height; // Y값 변화가 좀 느려서 3배 곱해줌.

        // 회전값을 40~85로 제한
        if (yAngle < limit_yAngle_lest)
            yAngle = limit_yAngle_lest;
        if (yAngle > limit_yAngle_MAX)
            yAngle = limit_yAngle_MAX;

        cameraArm.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
    }
}
