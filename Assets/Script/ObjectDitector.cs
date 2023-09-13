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
                    if (clone.inOperation == true) //�̹� ������ ������ barrack�̶�� Ŭ���ص� �ƹ��ϵ� �Ͼ�� ���� (���� ������ �ߵ� ���� ������ ���ϰ� ����)
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