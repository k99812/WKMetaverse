## WKDCMetaverseS2
| 항목 | 내용 | 비고 |
|------|------|------|
| 개발 기간 | 2022.06 ~ 2023.02	 |
| 엔진 | Unity 2021.3.14f1 |
| 언어 | C# |
| 주요 시스템 | UGUI / Photon / NativeGallery |

<br/>

아래 링크에서 해당프로젝트에 쓰인 플로우차트를 확인할 수 있습니다.

<a href="https://www.figma.com/board/vSDbySXst7xYlMhZew8J1L/WKM-System-flow?node-id=0-1&t=L0rwCpZiX0pDOKq9-1" height="5" width="10" target="_blank" >
<img src="https://img.shields.io/badge/플로우 차트 (Figma)-000000?style=for-the-badge&logo=figma&logoColor=white">
</a>

<br/>

## 담당 개발 기능

* 포톤 엔진을 이용해 멀티플레이 구현
* Photon의 RPC 기능을 활용한 실시간 채팅 구현
* 캐릭터 스폰 및 동기화, 캐릭터 선택 기능 구현
* 기본 캐릭터 이동 및 조작 시스템 개발

<br/>

## 플레이모습
---
![Animation](https://github.com/k99812/WKMetaverse/assets/108670965/e33e461d-b901-4bcd-a601-a2b38b31b9a2)
<br>
![Animation (1)](https://github.com/k99812/WKMetaverse/assets/108670965/66821ab9-f5f7-46d4-9fb1-78575a008c25)  

![Animation (3)](https://github.com/k99812/WKMetaverse/assets/108670965/67655280-2ba0-4a1c-8454-946ee6042e98)  

![Animation (2)](https://github.com/k99812/WKMetaverse/assets/108670965/74ea1206-8cfc-4f3f-b7ff-1461fb4b5c1c)  

<br/>

# 기술 설명서
## 프로젝트 전체 구성
<img width="1434" height="449" alt="image" src="https://github.com/user-attachments/assets/25b6e2c8-de4d-4367-98e6-b3cd4e8cea2e" />

프로젝트 실행 순서    
1. 프로젝트 실행시 MainLoby Scene이 실행됩니다.
2. MainLoby에서 싱글/멀티 중 하나를 선택하면 CharacterSelec Scene으로 이동합니다.
3. 캐릭터 선택후 Go 버튼을 누르면 Main Scene으로 이동합니다
4. 싱글플레이의 경우 바로 캐릭터가 스폰되며 멀티플레이는 NetworManager 스크립트에 의해 UI가 표시됩니다

<br/>

## NetworManager
<img width="1480" height="382" alt="image" src="https://github.com/user-attachments/assets/b90e2039-34a7-4004-b1d2-1f9d161b6602" />
   
NetworManager코드의 경우 캐릭터 스폰 처리만 관여해 스크립트를 저장소에 올리지 않았습니다.
* 멀티플레이로 선택시 포톤 접속 ~ 캐릭터 스폰까지의 과정입니다.
* 네트워크 매니저는 포톤 접속, 캐릭터 스폰, 네트워크UI를 담당

<br/>

> NetworManager
    
    //SpawnCoroutine()
    IEnumerator SpawnCoroutine(float spawnTime)
    {
         ~~~
         
        if (Character_Manager != null)
        {
            player = PhotonNetwork.Instantiate(CharacterManager.Instance.GetSelectedCharacterName(), spawnList[spawnPo].position, spawnList[spawnPo].rotation, 0);
        }

         ~~~
         
    }

* CharacterManager인스턴스를 통해 선택한 캐릭터의 이름을 가져와 PhotonNetwork.Instantiate() 함수를 이용하여 캐릭터 스폰

<br/>

## CharacterManager
<img width="460" height="792" alt="image" src="https://github.com/user-attachments/assets/06f3dc95-6732-41b2-bc2d-96aebe5d3f7a" />

<br/>

* 캐릭터 매니저는 플레이어의 싱글/멀티 정보, 선택된 캐릭터 정보, 캐릭터 선택 씬에서 캐릭터 온오프 기능을 담당

<br/>

> CharacterManager
    
    public static CharacterManager Instance { get; private set;  }

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

* 캐릭터 매니저는 싱글톤 패턴을 활용하여 인스턴스를 생성

<br/>

### 온라인/오프라인 선택
<img width="556" height="135" alt="image" src="https://github.com/user-attachments/assets/5f54ae31-d1b8-4abf-9685-c61dc6a63ebe" />

* 버튼 OnClick 이벤트에 Set_OnOff 함수 바인드

<br/>

> CharacterManager
    
    public enum OnOffSet
    {
        OnLine, OffLine
    }
    public OnOffSet onOffSet = OnOffSet.OnLine;

    public void Set_OnOff(int InCode)
    {
        onOffSet = InCode == 1 ? OnOffSet.OnLine : OnOffSet.OffLine;
    }

* OnClick 이벤트로 플레이어가 선택한 모드 저장

<br/>

### 캐릭터 선택 및 온오프
<img width="261" height="298" alt="image" src="https://github.com/user-attachments/assets/0b8332de-579b-43b0-a87f-9419d176077b" />
<img width="377" height="88" alt="image" src="https://github.com/user-attachments/assets/057ca14a-b08e-4e5f-85d3-91fdfd7b9db2" />

* 해당 토글에 OnValueChanged 이벤트에 On_Character_Code함수 바인드

<br/>

> CharacterManager

    [SerializeField]
    public List<GameObject> Characters = new List<GameObject>();

    private const int Young_Boy = 1, Boy = 2, Girl = 3;
    private int Character_Code = Young_Boy;
    
    //On_Character_Code()
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

* OnValueChanged 이벤트 발생시 Character_Code 갱신
* 캐릭터 선택씬 off_Chracters 함수로 캐릭터 비활성화 및 선택 캐릭터 활성화

<br/>

### 캐릭터 스폰

> CharacterManager
    
    private Dictionary<int, GameObject> CharacterMap = new Dictionary<int, GameObject>();

    [SerializeField]
    private List<string> CharacterNames = new List<string>() { "None", "Player_V9_CamTest", 
        "Player_Man_V9_CamTest", "Player_Girl_V9_CamTest" };
    
    private void Start()
    {
        CharacterMap[Young_Boy] = Resources.Load<GameObject>(CharacterNames[Young_Boy]);
        CharacterMap[Boy] = Resources.Load<GameObject>(CharacterNames[Boy]);
        CharacterMap[Girl] = Resources.Load<GameObject>(CharacterNames[Girl]);
    }

    public string GetSelectedCharacterName() { return CharacterMap[Character_Code].name; }

    public GameObject GetSelectedCharacter() { return CharacterMap[Character_Code]; }

* CharacterMap 캐릭터 애셋을 저장할 자료구조
* CharacterNames 캐릭터의 경로를 저장할 자료구조
* Start() 함수에서 CharacterMap 초기화
* GetSelectedCharacterName() 함수는 멀티플레이에서 Resource 폴더를 이용해 캐릭터 생성시 사용
* GetSelectedCharacter() 함수는 싱글플레이시 캐릭터 생성시 사용

<br/>

## 캐릭터 컨트롤
### 캐릭터 이동
캐릭터 이동에 필요한
<a href="https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631?srsltid=AfmBOoooMs3dACuBTGDoAHcQsbaErge2w09_yHIQnSbk9xnvevOYArFl" >조이스틱</a>은 해당 애셋을 사용했습니다

> VirtualJoyStickMove

    [SerializeField]
    private VariableJoystick joystick;
    
    public void Move()
    {
        if (CharacterManager.Instance != null && CharacterManager.Instance.onOffSet == CharacterManager.OnOffSet.OnLine
            && PV != null && !PV.IsMine) return;

            Vector2 moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
            bool isMove = moveInput.magnitude != 0;
            animator.SetBool("isMove", isMove);
   
        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * player_speed;
        }
        else if(!isMove && !isJump)
        {
            characterBody.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

* CharacterManager.Instance가 null인 경우는 싱글모드 씬으로 직접 들어간 경우   
  onOffSet이 OnLine일 경우만 체크하여 싱글플레이에서도 해당 함수를 사용 가능
* PV != null && !PV.IsMine 포톤뷰를 체크하여 멀티플레이에서 자기 캐릭터가 아닌경우 움직일 수 없도록 제한
* VariableJoystick에 저장되는 joystick.Horizontal, joystick.Vertical로 움직임을 구현

<br/>

### 카메라 로테이션

> CameraDragRotation

      public class CameraDragRotation : MonoBehaviour, IBeginDragHandler, IDragHandler
      {

         [SerializeField]
         private float limit_yAngle_lest = -30f;
         [SerializeField]
         private float limit_yAngle_MAX = 70;

         Vector3 FirstPoint, Vector3 SecondPoint;
         float xAngle, float yAngle;
         float xAngleTemp, float yAngleTemp;
      }

* IBeginDragHandler, IDragHandler 인터페이스를 상속받아
  드레그 이벤트를 통해 카메라 암 회전 구현
* FirstPoint, SecondPoint: 첫 터치 위치와 드래그로 인해 변하는 위치 저장
* xAngle, yAngle: 회전할 각도를 저장
* xAngleTemp, yAngleTemp: 드레그 시작시 기존 앵글을 저장

<br/>

> CameraDragRotation

      public void OnBeginDrag(PointerEventData eventData)
      {
            if (CharacterManager.Instance != null && CharacterManager.Instance.onOffSet == CharacterManager.OnOffSet.OnLine
               && PV != null && !PV.IsMine) return;
            BeginDrag(eventData.position);
      }

      public void BeginDrag(Vector2 a_FirstPoint)
      {
         FirstPoint = a_FirstPoint;
         xAngleTemp = xAngle, yAngleTemp = yAngle;
      }

* 싱글플레이, 씬으로 다이렉트 접근(캐릭터매니저 인스턴스 null), 포톤뷰(캐릭터 소유가 플레이어)
  위의 경우에만 실행하도록 if문으로 체크
* OnBeginDrag 이벤트 발생시 pos을 BeginDrag 함수로 넘김
* 첫번째 터치 위치와 기존 앵글을 저장

<br/>

> CameraDragRotation

      public void OnDrag(PointerEventData eventData)
      {
            if (CharacterManager.Instance != null && CharacterManager.Instance.onOffSet == CharacterManager.OnOffSet.OnLine
               && PV != null && !PV.IsMine) return;
         OnDrag(eventData.position);       
      }

      public void OnDrag(Vector2 a_SecondPoint)
      {
         SecondPoint = a_SecondPoint;
         xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
         yAngle = yAngleTemp - (SecondPoint.y - FirstPoint.y) * 90 * 3f / Screen.height;
         
         yAngle = Mathf.Clamp(yAngle, limit_yAngle_lest, limit_yAngle_MAX);
      
         cameraArm.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
      }

* OnDrag 이벤트 발생시 데이터를 OnDrag(Vector2 a_SecondPoint)에 넘겨줌
* SecondPoint - FirstPoint 로 첫번째 터치에서 움직인 만큼 기존 앵글에 더하여 새로운 앵글을 계산
* Mathf.Clamp로 Y각도를 제한 후 Quaternion.Euler 함수로 카메라암 회전

<br/>

## 채팅 기능
채팅 기능의 주요 로직은 <a href="https://www.youtube.com/watch?v=iARzkDbhA8k">해당영상</a>을 참고하며 만들었습니다.

> ChatManager

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

* 영상을 토대로 chat(bool isSend, string Message, string userName, Texture texture)로 구현했으며
* sendMessage 함수를 send 버튼 및 Enter 키입력과 바인딩 했습니다.
* sendMessage 실행시 자기자신에게는 isSend = true로 설정하여 UI를 스폰합니다
  RPC함수를 RpcTarget을 자기자신을 제외하는 Others로 설정해 호출합니다.

<br/>

## 사진 기능
> Self_Cam

      public class Self_Cam : MonoBehaviour   
      {
         public void startCor()
         {
            StartCoroutine(ShootingScreen());
         }
      
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
      }

* 사진 촬영 버튼에 startCor 함수를 바인드
* ShootingScreen 함수에선 버튼을 비활성화 시킨후
  yield return new WaitForEndOfFrame()로 한프레임 후 로직 실행
* Images는 미리보기 사진의 배경 이미지

<br/>

> Self_Cam

      public class Self_Cam : MonoBehaviour
      {
         private void CaptureScreen()
         {
            string tempDate = System.DateTime.Now.ToString("yyyy-mm-dd-mm-ss");
            string fileName = "WonkwangUs-ScreenShoot-" + tempDate + ".png";
            
            //플랫폼 분기
            if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
               CaptureScreenMobile(fileName);
            }
            else if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            {
               CaptureScreenPC(fileName);
            }
         }
      }

* 스크린샷 이름을 생성하여 플랫폼 별로 함수를 실행

<br/>

> Self_Cam

      public class Self_Cam : MonoBehaviour
      {
         private void CaptureScreenPC(string fileName)
         {
            texture = ScreenCapture.CaptureScreenshotAsTexture();
            string path = Application.persistentDataPath + fileName;
            
            byte[] bytes;
            bytes = texture.EncodeToPNG();
            System.IO.File.WriteAllBytes(path, bytes);
            Debug.Log("여기에 저장되었습니다. " + path);
         }
      }

* PC의 경우 Application.persistentDataPath + fileName으로 경로지정
* System.IO.File.WriteAllBytes 함수로 캡쳐화면 저장

<br/>

> Self_Cam

      public class Self_Cam : MonoBehaviour
      {
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
      }

* 모바일 플랫폼의 경우 유니티 애셋 NativeGallery 플러그인을 통하여 파일 저장

<br/>

> Self_Cam

      public class Self_Cam : MonoBehaviour
      {
         private void showPreview()
         {
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            capture_Image.sprite = sprite;
         }

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

* showPreview 함수를 실행하여 sprite를 생성 하여 미리보기 스프라이트에 지정
* 코루틴을 이용해 스프라이트의 alpha 값을 조절하여 효과 구현

<br/>

# WKDCMetaverseS2

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

WK Metaverse Season 2 - Time Machine

원광대 디지털콘텐츠공학과 메타버스 시즌2 - 타임머신 Version3 by (김준서, 김태욱, 이강인, 박혜진, 심미림, 조혜림, 이희원)


## Requirement

[Unity 2021.3.14f1 (LTS)](https://unity3d.com/unity/qa/lts-releases?version=2021.3)

## WKMetaversity Project Description

Wonkwang university, Dept. of Digital Content Engineering will soon be opening a new campus in the metaverse. WK Metaverse Campus (named WKMetaversity), which is an university immersive environment combined VR and AR technologies, is introducing an immersive 3D, virtual learning experience to completely transform the online and hybrid education landscape in Korea. This is a Digital Twin of a physical campus, and where students and faculty can interact with each other via their digital representation of themselves.

### Metaversity

The higher education Metaverse is about creating immersive learning experiences. Soon, students will be attending the Metaversity. 

Immersive virtual platforms have been around for two decades. Some perhaps remember Second Life, an online multimedia platform that allowed people to create an avatar for themselves and live a virtual life in a virtual world. [Source: www.fierceeducation.com]

## Works

* Contributors 공유 정보
* 코드 작성 시에, Doxygen 주석 방법에 맞춰서 클래스, 변수와 함수에 대해 주석 작성하기

### Unity
  * Unity 버전을 통일 합니다 (2021.3.11f1 LTS)
  * Asset Searilization을 통일 합니다 (Edit - Project Settings - Editor)에서 Asset Searialization mode 를 (Force Text)로 맞춥니다
  * Git 서버는 Gitea (2022.06.) - 백업용으로 미러링
  * Git 서버를 Github 로 변경 (대용량 파일 처리 2022.06.25.)
  * Client tool : SourceTree, Github Desktop

### Platform
  * Android, preferentially
  * Oculus Quest2
 

## License

[MIT License]


