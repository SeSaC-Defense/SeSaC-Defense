using Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : Singleton<ObjectDetector>
{
    [SerializeField]
    private ButtonGroupToggle buttonGroupToggle;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        RaycastHit hit;

        if (CanHit(out hit))
        {
            buttonGroupToggle.HitTransform = hit.transform;

            switch ((hit.transform.tag, UIStateEventHandler.Instance.CurrentState))
            {
                case ("Tile", UIStateType.ConstructionChecking):
                    UIStateEventHandler.Instance.ChangeState(UIStateType.ConstructionConfirming);
                    break;
                case ("Tower", UIStateType.None):
                    UIStateEventHandler.Instance.ChangeState(UIStateType.TowerPressed);
                    break;
                case ("Barrack", UIStateType.None):
                    UIStateEventHandler.Instance.ChangeState(UIStateType.BarrackPressedOnWaiting);
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

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, Mathf.Infinity);
    }
}