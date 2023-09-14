using System.Collections;
using System.Collections.Generic;
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

        if (UIStateType.ConstructionChecking == state
            && !tile.IsBuildTower)
            childObject.SetActive(true);
        else
            childObject.SetActive(false);
    }

}
