using Pattern;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ObjectDetector : Singleton<ObjectDetector>
{    
    private Camera mainCamera;
    public Transform HitTransform { get; private set; }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        RaycastHit hit;

        if (CanHit(out hit))
        {
            HitTransform = hit.transform;

            switch ((hit.transform.tag, UIStateEventHandler.Instance.CurrentState))
            {
                case ("Tile", UIStateType.ConstructionChecking):
                    if (hit.transform.GetComponent<Tile>().IsBuildTower)
                        return;
                    UIStateEventHandler.Instance.ChangeState(UIStateType.ConstructionConfirming);
                    break;
                case ("Tower", UIStateType.None):
                    UIStateEventHandler.Instance.ChangeState(UIStateType.TowerPressed);
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
            case UIStateType.BarrackPressedOnProducing:
            case UIStateType.TowerPressed:
            case UIStateType.Config:
                return false;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, Mathf.Infinity);
    }

    private void ChangeUIStateForBarrack(RaycastHit hit)
    {
        if (hit.transform.GetComponent<UnitSpawner>().InOperation)
            UIStateEventHandler.Instance.ChangeState(UIStateType.BarrackPressedOnProducing);
        else
            UIStateEventHandler.Instance.ChangeState(UIStateType.BarrackPressedOnWaiting);
    }
}