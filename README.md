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
## 프로젝트 전체 구조
<img width="1434" height="449" alt="image" src="https://github.com/user-attachments/assets/25b6e2c8-de4d-4367-98e6-b3cd4e8cea2e" />

1. 프로젝트 실행시 MainLoby Scene이 실행됩니다.
2. MainLoby에서 싱글/멀티 중 하나를 선택하면 CharacterSelec Scene으로 이동합니다.
3. 캐릭터 선택후 Go 버튼을 누르면 Main Scene으로 이동합니다
4. 싱글플레이의 경우 바로 캐릭터가 스폰되며 멀티플레이는 NetworManager 스크립트에 의해 UI가 표시됩니다

<br/>

## NetworManager
<img width="1480" height="382" alt="image" src="https://github.com/user-attachments/assets/b90e2039-34a7-4004-b1d2-1f9d161b6602" />

멀티플레이로 선택시 포톤 접속 ~ 캐릭터 스폰까지의 과정입니다.   

<br/>

> NetworManager

    private CharacterManager Character_Manager;
    
    //Start()
    void Start()
    {
         GameObject gameObject = GameObject.Find("CharacterManager");
         if (gameObject != null) 
         {
             Character_Manager = gameObject.GetComponent<CharacterManager>();
         }

         ~~~
         
    }

Start 함수에서 CharacterManager 초기화

<br/>

> SpawnCoroutine
    
    //Start()
    IEnumerator SpawnCoroutine(float spawnTime)
    {
         ~~~
         
        if (Character_Manager != null)
        {
            player = PhotonNetwork.Instantiate(Character_Manager.GetSelectedCharacterName(), spawnList[spawnPo].position, spawnList[spawnPo].rotation, 0);
        }

         ~~~
         
    }

가져온 CharacterManager를 통해 선택한 캐릭터의 이름을 가져와 PhotonNetwork.Instantiate() 함수를 이용하여 생성

<br/>

## CharacterManager


<br/>

## 캐릭터 컨트롤


<br/>

## 채팅 기능


<br/>

## 사진 기능


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


