using Pattern;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ObjectDetector : Singleton<ObjectDetector>
{    
    private new Camera camera;
    public Transform HitTransform { get; private set; }

    private void Start()
    {
        CameraManager.OnCameraChanged += OnPlayerCameraChanged;
        
        if (NetworkManager.Singleton.IsHost)
            camera = CameraManager.Instance.CamerasPlayerBase[0];
        else
            camera = CameraManager.Instance.CamerasPlayerBase[1];
    }

    private void OnPlayerCameraChanged(Camera camera)
    {
        this.camera = camera;
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

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, Mathf.Infinity);
    }

    private void ChangeUIStateForBarrack(RaycastHit hit)
    {
        if (!IsBuildingMine(hit))
            return;

        if (hit.transform.GetComponent<UnitSpawner>().IsOperating)
            UIStateEventHandler.Instance.ChangeState(UIStateType.BuildingPressed);
        else
            UIStateEventHandler.Instance.ChangeState(UIStateType.BarrackPressedOnWaiting);
    }

    private void ChangeUIStateForTile(RaycastHit hit)
    {
        if (!IsTileMine(hit))
            return;

        if (hit.transform.GetComponent<Tile>().HasBuilding)
            return;
        
        UIStateEventHandler.Instance.ChangeState(UIStateType.ConstructionConfirming);
    }

    private void ChangeUIStateForTower(RaycastHit hit)
    {
        if (!IsBuildingMine(hit))
            return;

        UIStateEventHandler.Instance.ChangeState(UIStateType.BuildingPressed);
    }

    private bool IsBuildingMine(RaycastHit hit)
    {
        ulong ownerId = hit.transform.GetComponent<NetworkObject>().OwnerClientId;
        ulong myId = NetworkManager.Singleton.LocalClientId;

        return ownerId == myId;
    }

    private bool IsTileMine(RaycastHit hit)
    {
        int tileMapPlayerNo = hit.transform.parent.GetComponent<TileMapSite>().PlayerNo;

        if (tileMapPlayerNo == 1 && NetworkManager.Singleton.IsHost)
            return false;

        if (tileMapPlayerNo == 0 && !NetworkManager.Singleton.IsHost)
            return false;

        return true;
    }
}