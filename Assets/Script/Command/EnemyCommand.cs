using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : ICommand
{
    int location;
    int unitType;

    public EnemySpawn(int location, int unit)
    {
        this.location = location;
        this.unitType = unit;
    }

    public void Execute()
    {
        //ObjectDetector.Instance.tile[location].GetComponent<EnemySpawner>().UnitChoice(unitType);
    }
}

public class EnemyTowerSpawn : ICommand
{
    int location;
    int towerType;

    public EnemyTowerSpawn(int location, int towerType)
    {
        this.location = location;
        this.towerType = towerType;
    }
    public void Execute()
    {
        //TowerSpawner.Instance.SpawnTower(towerType);
    }
}

public class EnemyTearDown : ICommand
{
    int localtion;

    public EnemyTearDown(int localtion)
    {
        this.localtion = localtion;
    }

    public void Execute()
    {
        Transform o = ObjectDetector.Instance.HitTransform;
        Object.Destroy(o.gameObject);
    }
}
