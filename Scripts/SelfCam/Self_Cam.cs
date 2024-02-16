using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;


public class Self_Cam : MonoBehaviour
{
    [SerializeField]
    private AudioSource shootingSound;
    [SerializeField]
    private CinemachineVirtualCamera selfCam;
    [SerializeField]
    private List<GameObject> otherCanvas;
    [SerializeField]
    private List<GameObject> Buttons;
    [SerializeField]
    private Canvas selfCam_Canvas;
    [SerializeField]
    private GameObject Images;
    [SerializeField]
    private Image capture_Image;
    [SerializeField, Tooltip("���̵� �ƿ� ȿ�� �ð�����")]
    private float time = 1.5f;

    private Texture2D texture;

    private void Start()
    {
        //ù����� ����ī�޶� ��Ȱ��ȭ
        selfCam.enabled = false;
        Images.SetActive(false);
        selfCam_Canvas.enabled = false;
    }

    private void Update()
    {
        if (Images.activeSelf)
        {
            if(Input.GetMouseButtonDown(0) || Input.touchCount > 0)
            {
                Images.SetActive(false );
                selfCam_Canvas.enabled = true;

                Color color = capture_Image.color;
                color.a = 0f;
                capture_Image.color = color;
                texture = null;

                foreach (var button in Buttons)
                {
                    button.SetActive(true);
                }
            }
        }
    }

    //����ķ�� 3��Īī�޶� ü����
    public void ChangeCam() 
    {
        if (selfCam.enabled)
        {
            selfCam.enabled = false;
        }
        else
        {
            selfCam.enabled = true;
        }
    }

    //��ư �̺�Ʈ�� startcoroutine�� ���� �Լ��� ����
    public void startCor()
    {
        StartCoroutine(ShootingScreen());
    }

    //ĵ���� ��Ȱ��ȭ Ȱ��ȭ ����
    public void enter_camCanvas()
    {
        selfCam_Canvas.enabled = true;
        foreach (var canvas in otherCanvas)
        {
            canvas.SetActive(false);
        }
    }

    public void exit_camCanvas()
    {
        selfCam_Canvas.enabled = false;
        foreach (var canvas in otherCanvas)
        {
            canvas.SetActive(true);
        }
    }

    //�ڷ�ƾ�� �̿��Ͽ� ȭ�� ĸ��
    private void CaptureScreen()
    {
        string tempDate = System.DateTime.Now.ToString("yyyy-mm-dd-mm-ss");
        string fileName = "WonkwangUs-ScreenShoot-" + tempDate + ".png";

        //�÷��� �б�
        if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            CaptureScreenMobile(fileName);
        }
        else if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            CaptureScreenPC(fileName);
        }

    }

    //�÷��� �б�
    //������� ��� �⺻ ���� ������ ���� ���ֱ� ����
    //NativeGallery �ּ��� �̿��Ͽ� ����
    //���� https://eunjin3786.tistory.com/521
    private void CaptureScreenMobile(string fileName)
    {
        texture = ScreenCapture.CaptureScreenshotAsTexture();

        NativeGallery.Permission permission = NativeGallery.CheckPermission(NativeGallery.PermissionType.Write);
        if(permission == NativeGallery.Permission.Denied)
        {
            if (NativeGallery.CanOpenSettings())
            {
                NativeGallery.OpenSettings();
            }
        }

        string albumName = "WonkwangUS";
        NativeGallery.SaveImageToGallery(texture, albumName, fileName, (success, path) => { Debug.Log(success); Debug.Log(path); });
    }

    //��θ� + �����̸����� ��ũ�� ĸ��
    //���� https://daily50.tistory.com/m/461
    private void CaptureScreenPC(string fileName)
    {
        texture = ScreenCapture.CaptureScreenshotAsTexture();
        string path = Application.persistentDataPath + fileName;

        byte[] bytes;
        bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(path, bytes);
        Debug.Log("���⿡ ����Ǿ����ϴ�. " + path);
    }

    private void showPreview()
    {
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        capture_Image.sprite = sprite;
    }

    //�ڷ�ƾ�� �̿��Ͽ� ȿ�� �� ȭ��ĸ�� ����
    private IEnumerator ShootingScreen()
    {
        shootingSound.Play();
        foreach (var button in Buttons)
        {
            button.SetActive(false);
        }

        yield return new WaitForEndOfFrame();
        CaptureScreen();
        Images.SetActive(true);
        showPreview();
        StartCoroutine(FadeIn(time));
    }

    //Fade in
    private IEnumerator FadeIn(float time)
    {
        Color color = capture_Image.color;

        while(color.a < 1f)
        {
            color.a += Time.deltaTime/time;
            capture_Image.color = color;

            if(color.a >= 1f)
            {
                color.a = 1f;
                capture_Image.color = color;
            }
            yield return null;
        }
    }
}
