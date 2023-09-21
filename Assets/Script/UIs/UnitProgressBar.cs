using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProgressBar : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Transform playerBaseTransform;
    [SerializeField]
    private Transform enemyBaseTransform;
    [SerializeField]
    private RectTransform neutralBar;
    [SerializeField]
    private RectTransform playerBar;
    [SerializeField]
    private RectTransform enemyBar;

    private float playerUnitPositionX;
    private float enemyUnitPositionX;
    private float playerBasePositionX;
    private float enemyBasePositionX;
    private float barLength;

    public float PlayerUnitPositionX { set => playerUnitPositionX = value; }
    public float EnemyUnitPositionX { set => enemyUnitPositionX = value; }

    private void Awake()
    {
        barLength = RectTransformUtility.PixelAdjustRect(neutralBar, canvas).size.x;
        playerBasePositionX = playerBaseTransform.position.x;
        enemyBasePositionX = enemyBaseTransform.position.x;
    }

    private void Update()
    {
        float distanceBetweenBase = enemyBasePositionX - playerBasePositionX;

        float unitPositionNearest = playerUnitPositionX > enemyUnitPositionX ? enemyUnitPositionX : playerUnitPositionX;
        float unitPositionFarthest = playerUnitPositionX > enemyUnitPositionX ? playerUnitPositionX : enemyUnitPositionX;
        
        float playerBarLength = (unitPositionNearest - playerBasePositionX) / distanceBetweenBase;
        float enemyBarLength = (enemyBasePositionX - unitPositionFarthest) / distanceBetweenBase;

        playerBar.sizeDelta = new Vector2(barLength * playerBarLength, playerBar.sizeDelta.y);
        enemyBar.sizeDelta = new Vector2(barLength * enemyBarLength, enemyBar.sizeDelta.y);
    }
}
