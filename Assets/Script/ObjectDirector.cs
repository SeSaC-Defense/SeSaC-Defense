using UnityEngine;

public class ObjectDirector : MonoBehaviour
{
    [SerializeField] private TowerSpawner towerSpawner;

    Camera maincamara;
    Ray ray;
    RaycastHit hit;

    private void Awake()
    {
        //"maincamera" �±׸� ������ �ִ� ������Ʈ Ž�� ��  camera ������Ʈ ���� ����
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
                    //Ÿ�� Ŭ���� �ൿ �̰���
                }
            }
        }
    }
}
