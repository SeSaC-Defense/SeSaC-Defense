using Pattern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class PlayerUnitList : MonoBehaviour
{
    private int playerNumber = 1;
    public int PlayerNumber => playerNumber;

    [SerializeField]
    private UnitProgressBar unitProgressBar;

    private List<Unit> unitList;

    public List<Unit> UnitList => unitList;

    private void Start()
    {
        unitList = new List<Unit>();
    }

    private void Update()
    {
        float farthestUnitPositionX = -100f;

        foreach (var unit in unitList)
        {
            if (unit.transform.position.x > farthestUnitPositionX)
            {
                farthestUnitPositionX = unit.transform.position.x;
            }
        }

        unitProgressBar.PlayerUnitPositionX = farthestUnitPositionX;
    }

    public void DestroyUnit(Unit unit)
    {
        unitList.Remove(unit);
        Destroy(unit.gameObject);
    }
}
