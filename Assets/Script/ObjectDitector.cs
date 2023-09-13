using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField] private TowerSpawner towerSpawner;
    [SerializeField] private GameObject unitPanel;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Tile"))
                {
                    towerSpawner.SpawnTower(hit.transform);
                }
                if (hit.transform.CompareTag("Barrack"))
                {
                    BarrackController clone = hit.transform.GetComponent<BarrackController>();
                    if (clone.inOperation == true) //이미 유닛이 생성된 barrack이라면 클릭해도 아무일도 일어나지 않음 (추후 정보는 뜨되 유닛 선택은 못하게 변경)
                    {
                        return;
                    }
                    unitPanel.GetComponent<BarrackDataViewer>().SelectedBuilding(hit.transform);
                    unitPanel.SetActive(true);
                }
            }
        }
    }
}