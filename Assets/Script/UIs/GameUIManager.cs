using Pattern;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    UnitProgressBar unitProgressBar;
    [SerializeField]
    private TextMeshProUGUI textGold;
    [SerializeField]
    private TextMeshProUGUI[] textPlayerHealths = new TextMeshProUGUI[2];

    public Player Player0 { get; set; }
    public Player Player1 { get; set; }

    private void Start()
    {
        UpdateGoldText();
        UpdatePlayersHealthText();
        UpdateUnitProgressBar();
    }

    private void Update()
    {
        UpdateGoldText();
        UpdatePlayersHealthText();
        UpdateUnitProgressBar();
    }

    public void UpdateGoldText()
    {
        if (NetworkManager.Singleton.LocalClient.PlayerObject?.GetComponent<PlayerGold>() == null)
            return;

        int gold = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerGold>().CurrentGold;
        textGold.text = gold.ToString();
    }

    public void UpdatePlayersHealthText()
    {
        if (Player0 == null || Player1 == null)
            return;

        textPlayerHealths[0].text = Player0.CurrentHP.ToString();
        textPlayerHealths[1].text = Player1.CurrentHP.ToString();
    }

    public void UpdateUnitProgressBar()
    {
        List<Transform> player0UnitList = PlayerUnitList.Instance.UnitTransformLists[0];
        List<Transform> player1UnitList = PlayerUnitList.Instance.UnitTransformLists[1];

        float player0UnitPositionX = Mathf.NegativeInfinity;
        float player1UnitPositionX = Mathf.Infinity;

        foreach (var Unit in player0UnitList)
        {
            player0UnitPositionX = Mathf.Max(player0UnitPositionX, Unit.transform.position.x);
        }

        foreach (var Unit in player1UnitList)
        {
            player1UnitPositionX = Mathf.Min(player1UnitPositionX, Unit.transform.position.x);
        }

        unitProgressBar.Player0UnitPositionX = player0UnitPositionX;
        unitProgressBar.Player1UnitPositionX = player1UnitPositionX;
    }
}
