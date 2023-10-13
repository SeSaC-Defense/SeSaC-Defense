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

    private float player0UnitPositionX;
    private float player1UnitPositionX;
    private float player0BasePositionX;
    private float player1BasePositionX;
    private float barLength;

    public float Player0UnitPositionX { set => player0UnitPositionX = value; }
    public float Player1UnitPositionX { set => player1UnitPositionX = value; }

    private void Awake()
    {
        barLength = RectTransformUtility.PixelAdjustRect(neutralBar, canvas).size.x;
        player0BasePositionX = playerBaseTransform.position.x;
        player1BasePositionX = enemyBaseTransform.position.x;
    }

    private void Update()
    {
        float distanceBetweenBase = player1BasePositionX - player0BasePositionX;

        float unitPositionNearest = player0UnitPositionX > player1UnitPositionX ? player1UnitPositionX : player0UnitPositionX;
        float unitPositionFarthest = player0UnitPositionX > player1UnitPositionX ? player0UnitPositionX : player1UnitPositionX;
        
        float playerBarLength = (unitPositionNearest - player0BasePositionX) / distanceBetweenBase;
        float enemyBarLength = (player1BasePositionX - unitPositionFarthest) / distanceBetweenBase;

        playerBar.sizeDelta = new Vector2(barLength * playerBarLength, playerBar.sizeDelta.y);
        enemyBar.sizeDelta = new Vector2(barLength * enemyBarLength, enemyBar.sizeDelta.y);
    }
}
