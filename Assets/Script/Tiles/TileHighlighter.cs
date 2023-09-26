using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TileHighlighter : MonoBehaviour
{
    void Start()
    {
        UIStateEventHandler.OnStateChanged += OnUIStateChanged;
    }

    private void OnUIStateChanged(UIStateType state)
    {
        GameObject childObject = transform.Find("TileHightlight").gameObject;
        Tile tile = GetComponent<Tile>();

        if (UIStateType.ConstructionChecking != state)
        {
            childObject.SetActive(false);
            return;
        }

        int tileMapPlayerNo = transform.parent.GetComponent<TileMapSite>().PlayerNo;

        if (tileMapPlayerNo == 1 && NetworkManager.Singleton.IsHost)
            return;

        if (tileMapPlayerNo == 0 && !NetworkManager.Singleton.IsHost)
            return;

        if (!tile.HasBuilding)
            childObject.SetActive(true);
    }

}
