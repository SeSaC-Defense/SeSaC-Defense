# SeSaC-Defense
# 🗼SeSaC-Defense

https://github.com/SeSaC-Defense/SeSaC-Defense

https://www.figma.com/file/cb0pSLpKq0j5RgQ1zip6BA/%EB%94%94%ED%8E%9C%EC%8A%A4-%ED%99%94%EB%A9%B4?type=design&node-id=0%3A1&mode=design&t=yaEZ4RXmxYV3Z89y-1

https://drive.google.com/file/d/1lIG2WIIkDOxOuFN-6nlFFIu_a6mT_6oB/view?usp=drive_web

### 박경용과 아이,,(임정원)

---

## 🧑‍💻기획의도

디펜스 게임 + 대전 전략

타워 설치와 유닛 생성을 동시에 할 수 있는 타워 디펜스

네트워크 기반 2인 대전 타워 디펜스 게임의 **기능적** 구현

---

## 🎲게임 설명

### 장르

> 전략 TowerDefense
> 

### 플랫폼

> Android
PC
> 

### 제작 환경

> Unity
GitHub
> 

### 게임 방식

> 마우스 또는 터치를 이용한 제한적인 조작
적이 보내는 유닛에게서 본진을 수비하는 타워를 설치
적진으로 이동하는 유닛을 생성하는 배럭을 설치하여 공격
적의 본진에 유닛을 통과시키면 승리하는 게임
> 

---

## 📊제작 설계

## ○Figma

**시작화면**

설정버튼과 게임시작버튼

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/cd6e6edb-d391-4382-ba40-f6e45922548b/Untitled.png)

**로딩화면**

랜덤매칭 시스템이 들어가면 도입 될 로딩화면
기다리는 시간의 지루함을 덜어내기 위해 이미지나 팁을 적음

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/f366a065-f2fb-4b95-bf19-807aac324089/Untitled.png)

**시작화면 - 옵션**

배경음과 효과음을 조절하는 화면, 게임화면에서도 켤 수 있음

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/bf0dd3c8-352d-48fd-9daf-4a939340ef02/Untitled.png)

**게임 화면**

상단에 적과 아군 유닛의 진행 상황을 파악 할 수 있음
UI를 통한 건설 기능, 건설 가능한 Tile을 한눈에 볼 수 있음

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/43374288-f44c-4b07-bd33-b1923cb9b77d/Untitled.png)

### ○**Figma 전체 설계**

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/0b7a537a-7aaf-4187-a49c-06d7742a5129/Untitled.png)

---

## 🧑‍🤝‍🧑사용자 유스케이스

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/aecfbd8e-8cdd-43ce-b829-74ac6ee77796/Untitled.png)

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/ab173d5c-76fe-44f8-a1b6-67b48dc89162/Untitled.png)

---

## 📓클래스 다이어그램

### ○제작 전 클래스 다이어그램

![디펜스 다이어그램-클래스 다이어그램-Simple.jpg](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/548a6158-64e3-4f26-a5a4-86ef4c48cf87/%EB%94%94%ED%8E%9C%EC%8A%A4_%EB%8B%A4%EC%9D%B4%EC%96%B4%EA%B7%B8%EB%9E%A8-%ED%81%B4%EB%9E%98%EC%8A%A4_%EB%8B%A4%EC%9D%B4%EC%96%B4%EA%B7%B8%EB%9E%A8-Simple.jpg)

---

### ○개발 시작 후 변경된 클래스 다이어그램

![클래스다이어그램.png](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/656cd4b2-6128-4061-be24-6c4b6c2f109b/%ED%81%B4%EB%9E%98%EC%8A%A4%EB%8B%A4%EC%9D%B4%EC%96%B4%EA%B7%B8%EB%9E%A8.png)

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/9bd21329-f04a-47f5-8d6f-c3b93be1d68c/Untitled.png)

---

## 💻기능 구현

### ○네트워크 및 주요 시스템

Unity Game Service를 이용하여 p2p 네트워크 게임 접속 구현

- Unity Game Service의 Authentication을 이용한 로그인
- Unity Game Service의 Relay를 통한 게임 생성 및 접속

Unity Netcode를 이용한 통신

- Relay 접속 정보를 이용해 게임 생성
- NetworkObject와 NetworkBehaviour를 통한 gameObject 생성
- NetworkTransform을 이용한 위치 정보 동기화
- NetworkVariable을 사용하여 게임 정보 동기화
- RPC를 이용한 통신과 소유권에 따른 동작

Netcode의 게임 생성 방식

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/febdf52b-7807-42fc-8edc-8d630c7984f4/Untitled.png)

게임 내 RPC 동작 방식

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/0b2db804-bc17-4b0b-9a3c-6b668cef9a9c/Untitled.png)

Netcode 컴포넌트 및 클래스 적용 범위

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/edb8efa3-01c0-4996-b919-41e26051e879/Untitled.png)

상태와 이벤트를 이용한 인게임 UI 전환

- UI 상태 갱신에 따라 이벤트가 발생하고 각 UI의 활성화/비활성화

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/224e8ba7-8627-4dac-80e8-349b188a8b05/Untitled.png)

모든 UI 비활성화 상태

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/0bc8c9b8-bccf-4e99-b20c-e67d5b10b824/Untitled.png)

건축 재확인 활성화 상태

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/b59c424f-d24e-4c53-bba0-0b4099906e20/Untitled.png)

건축 부지 UI 활성화 상태

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/5f709b95-32b7-4028-bc17-5929d459eb1b/Untitled.png)

유닛 선택 활성화 상태

### ○**RelayManager**

Host는 UnityGameService에 Relay를 통해 서버를 할당 받음

```csharp
public async void OnStartHost()
    {
        uiGroupHost.SetStartButtonInteractable(false);
        panelLoading.SetActive(true);

        try
        {
            await SignInAsync();
            allocation = await RelayService.Instance.CreateAllocationAsync(1);

            var data = new RelayServerData(allocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(data);
            NetworkManager.Singleton.StartHost();

            uiGroupHost.SetStartButtonInteractable(true);
        }
        catch (Exception e)
        {
            ShowErrorMessage(e);
        }

        panelLoading.SetActive(false);
    }
```

Clinet가 JoinCode를 통해 host의 서버에 접속

```csharp
public async void GetJoinCode()
    {
        panelLoading.SetActive(true);

        try
        {
            string joinCode = await GetJoinCodeAsync();
            uiGroupHost.SetJoinCodeText(joinCode);
        }
        catch (Exception e)
        {
            ShowErrorMessage(e);
        }

        panelLoading.SetActive(false);
    }
```

### ○**GameLifecycle**

게임을 시작하면 각 host와 client에게 player오브젝트를 할당하여 그에 맞는 int값을 정해 host와 client의 player의 각기 다른 플레이 화면과 오브젝트를 할당하는 바탕

```csharp
private IEnumerator StartGameCoroutine()
    {
        for (int i = 5; i > 0; i--)
        {
            CountDownClientRpc(i);
            yield return new WaitForSeconds(1f);
        }

        IReadOnlyList<NetworkClient> list = NetworkManager.ConnectedClientsList;

        foreach (var client in list)
        {
            GameObject player = Instantiate(playerPrefab);
            ulong clientId = client.ClientId;

            player.GetComponent<Player>().Setup(GetEnemyId(clientId));

            player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        }

        yield return new WaitForSeconds(1f);

        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            client.PlayerObject.GetComponent<Player>().SetCameraClientRpc();
        }

        ShowGameUIClientRpc();
    }
```

### ○**UIStateEventHandler**

UI들을 이벤트로 저장하여 조종하는 손잡이가 되는 스크립트

```csharp
public UIStateType CurrentState { get; private set; }

    private void Start()
    {
        CurrentState = UIStateType.None;
        OnStateChanged += UIStateEventHandler_OnStateChange;
    }

    private void UIStateEventHandler_OnStateChange(UIStateType state)
    {
        CurrentState = state;
    }

    public void ChangeState(UIStateType state)
    {
        OnStateChanged(state);
    }
```

### ○**ObjectDetector**

마우스 클릭이나 터치에 따라 Tile, Tower, Barrack등 다른 UI 이벤트가 실행되게함

![제목 없는 디자인.gif](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/f6f86cc6-f55e-4eab-be5e-9a839da9c99a/%EC%A0%9C%EB%AA%A9_%EC%97%86%EB%8A%94_%EB%94%94%EC%9E%90%EC%9D%B8.gif)

```csharp
private void Update()
    {
        RaycastHit hit;

        if (CanHit(out hit))
        {
            HitTransform = hit.transform;

            switch ((hit.transform.tag, UIStateEventHandler.Instance.CurrentState))
            {
                case ("Tile", UIStateType.ConstructionChecking):
                    ChangeUIStateForTile(hit);
                    break;
                case ("Tower", UIStateType.None):
                    ChangeUIStateForTower(hit);
                    break;
                case ("Barrack", UIStateType.None):
                    ChangeUIStateForBarrack(hit);
                    break;
                default:
                    UIStateEventHandler.Instance.ChangeState(UIStateType.None);
                    break;
            }
        }
    }
```

### ○Player

Player int값을 받아 플레이하게될 위치와 그게 맞는 카메라를 할당

```csharp
public int PlayerNo => IsOwnedByServer ? 0 : 1;
[ClientRpc]
    public void SetCameraClientRpc()
    {
        if (IsHost)
        {
            cameraPlayerBase = GameObject.Find("CameraPlayer0").GetComponent<Camera>();
            cameraEnemyBase = GameObject.Find("CameraPlayer1").GetComponent<Camera>();
        }
        else
        {
            cameraPlayerBase = GameObject.Find("CameraPlayer1").GetComponent<Camera>();
            cameraEnemyBase = GameObject.Find("CameraPlayer0").GetComponent<Camera>();
        }

        SwitchCameraToPlayerBase();
    }
```

### 건물 생성

UI 클릭 후 Tile오브젝트를 클릭하여 원하는 위치에 건물을 건설

### ○**Tile Script**

건물 중복 설치가 되지 않게 설치될 위치인 Tile 마다
bool값을 가진 스크립트를 부여하여 중복 건설을 방지

```csharp
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool HasBuilding
    {
        set; get;
    }

    private void Awake()
    {
        HasBuilding = false;
    }

    public int PlayerNo => transform.parent.GetComponent<TileMapSite>().PlayerNo;
    
}
```

### ○**TowerSpanwer**

크게 두 종류로 나뉘어진 건물의 종류에 따라
내장된 Spawn함수를 실행시켜 건물을 활성화 해줌

```csharp
public void SpawnTower(Transform tileTransform)
    {
        int ix = (int)TowerChosen;
        Tile tile = tileTransform.GetComponent<Tile>();
        if (playerGold.CurrentGold < towerBuildGold)
        {
            return;
        }
        if (tile.HasBuilding == true) return;

        playerGold.CurrentGold -= towerBuildGold;

        tile.HasBuilding = true;
        if (ix == 0)
        {
            SpawnBarrack(NetworkManager.Singleton.LocalClientId, tileTransform);
            return;
        }
        SpawnTower(NetworkManager.Singleton.LocalClientId, tileTransform);
    }
```

### 건물

실질적인 수비를 담당하는 TowerWeapon과 유닛을 생성해 공격을 담당하는 UnitSpawner으로 나뉘어짐

### ○TowerWeapon

범위내에 있는 적을 찾아 적에게 닿으면 피해를 주는 오브젝트를 생성

```csharp
private Transform FindClosestAttackTarget()
    {
        float ClosestDistSqr = Mathf.Infinity;
        for ( int i = 0; i < EnemyUnitList.Count; ++i)
        {
            float distance = Vector3.Distance(EnemyUnitList[i].transform.position, transform.position);
            if ( distance <= attackRange && distance <= ClosestDistSqr)
            {
                ClosestDistSqr = distance;
                attackTarget = EnemyUnitList[i].transform;
            }
        }
        return attackTarget;
    }
```

### ○UnitSpawner

배럭을 다시 한번 더 클릭해 생성할 유닛을 선택하여 IEumerator를 통해 무한히 생성

```csharp
private void SpawnUnitServerRpc(ulong clientId, int unitTypeIx)
    {
        GameObject unitPrefab = unitPrefabs[unitTypeIx];
        GameObject clone = Instantiate(unitPrefab);

        Unit unit = clone.GetComponent<Unit>();

        clone.GetComponent<NetworkObject>().SpawnWithOwnership(clientId);

        unit.Setup(playerNo, gameObject.transform);
    }
```

### 유닛과 공격

적진을 향해 이동하는 유닛과 유닛을 향해 쏘아지는 공격오브젝트

### ○Movement2D

이동할 위치를 받아 좌표로 이동하며 위치 방향으로 방향 전환

```csharp
public void Move()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 directtion) 
    {
        //변수값을 이동할 위치에 넣어줌
        moveDirection = directtion;
        if (moveDirection.x < 0) //이동할 방향이 좌측이라면 좌측을 바라보고
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (moveDirection.x > 0) //이동할 방향이 우측이라면 우측을 바라보게
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
```

### ○Unit

이동할 waypoint를 list로 받아와 Index값을 늘리거나 줄여 다음으로 이동할 위치를 업데이트해줌

```csharp
private int FindNearestWaypointIndex()
    {
        int nearestWaypointIndex = -1;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < Waypoints.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, Waypoints[i].position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestWaypointIndex = i;
            }
        }

        return nearestWaypointIndex;
    }
```

---

## Github을 통한 이슈 관리, 코드 리뷰

![이슈](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/28eb04a4-9b01-4a69-8fe6-7d4f778f8bb8/Untitled.png)

이슈

![PR 목록](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/b2e84066-e982-42ff-81b1-fc36ea0d5461/Untitled.png)

PR 목록

![PR 요청 코드 리뷰](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/4f447cba-6d95-4835-82c8-f3415a549c73/Untitled.png)

PR 요청 코드 리뷰

## Github 브랜치 관리

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/34eb494b-7799-48af-af2d-1b8710ffd58d/84c8f1d3-41d4-4bab-a1f1-f517e175220e/Untitled.png)

## 문제점 및 해결방안

UI 클릭 시 게임 씬의 오브젝트가 같이 선택되는 문제

- UI 상태에 따른 이벤트로 오브젝트 디텍션 및 UI 활성화 제어

hierachy 최상단에 있지 않은 singleton의 제어 오류

- 인게임 내에서만 필요한 singleton의 DontDestroyOnLoad 제거
- 기타 singleton을 hierachy 최상단으로 이동

네트워크 얹기

- 구조적 변경
- 토이 프로젝트를 통한 패키지 API 기능 테스트
- 야근

## 📅일정

[제목 없는 데이터베이스](https://www.notion.so/42bd85d31a614b32a38bff98d7a3b4e6?pvs=21)

## 📔보완할 점

- 게임 전역 기능
    - 게임 씬 전환
    - 오디오 및 이미지 등 에셋 적용
    - Lobby 서비스를 이용한 자동 입장 및 private 룸 생성 구현
    
- 인게임 기능
    - 게임 유닛 및 타워 세부 구현 및 밸런싱
    - 여러 종류의 재화를 통한 타워 구매
    - 게임 종료, 상대방 퇴장 시 동작

- 리팩터링
    - 인터페이스 구현 및 적용
    - 책임에 따른 클래스 분리
    

# 유니티에서 Netcode 사용하시려면 처음부터 네트워크 연동하세요!
