using Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitList : Singleton<PlayerUnitList>
{
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
}
