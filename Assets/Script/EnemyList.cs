using Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : Singleton<EnemyList>
{
    [SerializeField]
    private UnitProgressBar unitProgressBar;

    private List<Enemy> enemyList;

    public List<Enemy> EEnemyList => enemyList;

    private void Start()
    {
        enemyList = new List<Enemy>();
    }

    private void Update()
    {
        float farthestUnitPositionX = -100f;

        foreach (var unit in enemyList)
        {
            if (unit.transform.position.x > farthestUnitPositionX)
            {
                farthestUnitPositionX = unit.transform.position.x;
            }
        }

        unitProgressBar.PlayerUnitPositionX = farthestUnitPositionX;
    }
}
