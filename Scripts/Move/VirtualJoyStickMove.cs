using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class VirtualJoyStickMove : MonoBehaviour
{
    [SerializeField]
    private float player_speed = 5f;

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    Button jumpBtn;
    [SerializeField]
    private float RayRange = 1f;

    [SerializeField]
    private Rigidbody rigid;

    [SerializeField]
    private VariableJoystick joystick;
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;
    private bool isJump = false;

    Animator animator;
    [SerializeField]
    private PhotonView pv;
    [SerializeField]
    private GameObject[] uiObj;

    private void Awake()
    {
        animator = characterBody.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] characterBodys = GameObject.FindGameObjectsWithTag("Player");
        pv = gameObject.GetComponent<PhotonView>();
        if (pv != null)
        {
            foreach (GameObject input in characterBodys)
            {
                if (!input.GetComponent<PhotonView>().IsMine)
                {
                    Destroy(input.GetComponent<VirtualJoyStickMove>());
                    if (!pv.IsMine)
                    {
                        for (int i = 0; i < uiObj.Length; i++)
                        {
                            Destroy(uiObj[i]);
                        }
                    }

                }
            }
        }
       
        isJump = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        // 이동 방향키 입력 값 가져오기
        Vector2 moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        // 이동 방향키 입력 판정 : 이동 방향 벡터가 0보다 크면 입력이 발생하고 있는 중
        bool isMove = moveInput.magnitude != 0;
        // 입력이 발생하는 중이라면 이동 애니메이션 재생
        animator.SetBool("isMove", isMove);
        if (isMove)
        {
            // 카메라가 바라보는 방향
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            // 카메라의 오른쪽 방향
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            // 이동 방향
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            // 이동할 때 이동 방향 바라보기
            characterBody.forward = moveDir;
            // 이동
            transform.position += moveDir * Time.deltaTime * player_speed;
        }
        else if(!isMove && !isJump)
        {
            characterBody.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    //점프 
    public void Jump()
    {
        if (!isJump)
        {
            isJump = true;
            animator.SetBool("isJump", true);
            animator.SetTrigger("Jump");
            rigid.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;
            animator.SetBool("isJump", false);
        }
    }
}
