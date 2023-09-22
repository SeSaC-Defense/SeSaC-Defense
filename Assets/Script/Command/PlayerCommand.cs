using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawn : ICommand
{
    int location;
    int unitType;

    public UnitSpawn(int location, int unit)
    {
        this.location = location;
        this.unitType = unit;
    }

    public void Execute()
    {
        ObjectDetector.Instance.Tile[location].GetComponent<UnitSpawner>().UnitChoice(unitType);
    }
}

public class TowerSpawn : ICommand
{
    int location;
    int towerType;

    public TowerSpawn(int location, int towerType)
    {
        this.location = location;
        this.towerType = towerType;
    }
    public void Execute()
    {
        TowerSpawner.Instance.SpawnTower(towerType);
    }
}

public class TearDown : ICommand
{
    int localtion;

    public TearDown(int localtion)
    {
        this.localtion = localtion;
    }

    public void Execute()
    {
        Transform o = ObjectDetector.Instance.HitTransform;
        Object.Destroy(o.gameObject);
    }
}
