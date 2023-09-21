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
    private GameObject buttonGroupBuilding;
    [SerializeField]
    private GameObject buttonGroupBarrackOnWaiting;
    [SerializeField]
    private GameObject buttonGroupConstructionConfirming;

    private GameObject currentButtonGroup;

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
            case UIStateType.BuildingPressed:
                currentButtonGroup = buttonGroupBuilding;
                break;
            case UIStateType.BarrackPressedOnWaiting:
                currentButtonGroup = buttonGroupBarrackOnWaiting;
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
        currentButtonGroup.GetComponent<UIPosition>().MoveTo(ObjectDetector.Instance.HitTransform);
    }
}
