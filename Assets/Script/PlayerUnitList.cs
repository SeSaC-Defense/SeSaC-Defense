using Pattern;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerUnitList : Singleton<PlayerUnitList>
{
    //[SerializeField]
    //private UnitProgressBar unitProgressBar;

    private List<Unit>[] unitLists;
    public List<Unit>[] UnitLists
    {
        get
        {
            return unitLists;
        }
    }

    private void Start()
    {
        unitLists = new List<Unit>[2];
        unitLists[0] = new List<Unit>();
        unitLists[1] = new List<Unit>();
    }

    public IReadOnlyList<Unit> GetEnemyUnitList(int playerNo)
    {
        return unitLists[playerNo == 0 ? 1 : 0];
    }

    public void AddUnit(Unit unit)
    {
        NetworkObject networkObject = unit.GetComponent<NetworkObject>();

        if (networkObject.IsOwnedByServer)
        {
            unitLists[0].Add(unit);
        }
        else
        {
            unitLists[1].Add(unit);
        }
    }

    public void RemoveUnit(Unit unit)
    {
        NetworkObject networkObject = unit.GetComponent<NetworkObject>();

        if (networkObject.IsOwnedByServer)
        {
            unitLists[0].Remove(unit);
        }
        else
        {
            unitLists[1].Remove(unit);
        }
    }

}
