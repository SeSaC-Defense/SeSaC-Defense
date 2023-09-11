using UnityEngine;

public class ObjectDirector : MonoBehaviour
{
    [SerializeField] private TowerSpawner towerSpawner;

    Camera maincamara;
    Ray ray;
    RaycastHit hit;

    private void Awake()
    {
        //"maincamera" 태그를 가지고 있는 오브젝트 탐색 후  camera 컴포넌트 정보 전달
        maincamara = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = maincamara.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Tile"))
                {
                    print("tile");
                    towerSpawner.SpawnTower(hit.transform);
                }
                else if (hit.transform.CompareTag("Tower"))
                {
                    print("Tower");
                    //타워 클릭시 행동 이곳에
                }
            }
        }
    }
}
