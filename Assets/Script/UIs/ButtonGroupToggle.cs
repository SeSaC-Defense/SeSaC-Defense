using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ButtonGroupType
{
    Construction,
    BarrackOnWaiting,
    BarrackOnProducing,
    Tower
}

public class ButtonGroupToggle : MonoBehaviour
{
    [Header("Button Groups")]
    [SerializeField]
    private GameObject buttonGroupTower;
    [SerializeField]
    private GameObject buttonGroupBarrackOnWaiting;
    [SerializeField]
    private GameObject buttonGroupBarrackOnProducing;
    [SerializeField]
    private GameObject buttonGroupConstructionConfirming;

    private GameObject currentButtonGroup;

    public Transform HitTransform { get; set; }

    private void Start()
    {
        UIStateEventHandler.OnStateChanged += OnUIStateChanged;
    }

    private void OnUIStateChanged(UIStateType state)
    {
        if (currentButtonGroup != null)
            currentButtonGroup.SetActive(false);

        switch (state)
        {
            case UIStateType.TowerPressed:
                currentButtonGroup = buttonGroupTower;
                break;
            case UIStateType.BarrackPressedOnWaiting:
                currentButtonGroup = buttonGroupBarrackOnWaiting;
                break;
            case UIStateType.BarrackPressedOnProducing:
                currentButtonGroup = buttonGroupBarrackOnProducing;
                break;
            case UIStateType.ConstructionConfirming:
                currentButtonGroup = buttonGroupConstructionConfirming;

                Sprite[] towerImages = buttonGroupConstructionConfirming.GetComponent<ButtonGroupConstructionConfirming>().TowerImages;
                Sprite sprite = towerImages[(int)TowerSpawner.Instance.TowerChosen];
                buttonGroupConstructionConfirming.transform.Find("Image").GetComponent<Image>().sprite = sprite;
                break;
            default:
                currentButtonGroup = null;
                return;
        }

        currentButtonGroup.SetActive(true);
        currentButtonGroup.GetComponent<UIPosition>().MoveTo(HitTransform);
    }
}
