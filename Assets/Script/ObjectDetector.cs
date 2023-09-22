using Pattern;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.TerrainUtils;

public class ObjectDetector : Singleton<ObjectDetector>
{
    [SerializeField]
    private Transform[] ttile;

    private Camera mainCamera;
    public Transform HitTransform { get; private set; }
    public Transform[] Tile {  get; private set; }
    public int HitIndex { get; private set; }
    
    private void Start()
    {
        Tile = ttile;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (CanHit(out RaycastHit hit))
        {
            FindIndex(hit.transform);
            Debug.Log(HitIndex);
            HitTransform = Tile[HitIndex];
            Debug.Log(HitTransform);
            switch ((hit.transform.tag, UIStateEventHandler.Instance.CurrentState))
            {
                case ("Tile", UIStateType.ConstructionChecking):
                    if (hit.transform.GetComponent<Tile>().IsBuildTower)
                        return;
                    UIStateEventHandler.Instance.ChangeState(UIStateType.ConstructionConfirming);
                    break;
                case ("Tower", UIStateType.None):
                    UIStateEventHandler.Instance.ChangeState(UIStateType.BuildingPressed);
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

    private bool CanHit(out RaycastHit hit)
    {
        hit = new RaycastHit();

        if (!Input.GetMouseButtonDown(0))
            return false;

        switch (UIStateEventHandler.Instance.CurrentState)
        {
            case UIStateType.ConstructionConfirming:
            case UIStateType.DestructionConfirming:
            case UIStateType.BarrackPressedOnWaiting:
            case UIStateType.BuildingPressed:
            case UIStateType.Config:
                return false;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, Mathf.Infinity);
    }

    private void ChangeUIStateForBarrack(RaycastHit hit)
    {
        if (hit.transform.GetComponent<UnitSpawner>().InOperation)
            UIStateEventHandler.Instance.ChangeState(UIStateType.BuildingPressed);
        else
            UIStateEventHandler.Instance.ChangeState(UIStateType.BarrackPressedOnWaiting);
    }

    public void FindIndex(Transform t)
    {
        for (int i = 0; i < Tile.Length; i++)
        {
            if (Tile[i] == t)
            {
                HitIndex = i;
                break;
            }
        }
    }
}