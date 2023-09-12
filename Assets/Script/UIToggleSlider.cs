using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggleSlider : MonoBehaviour
{
    [SerializeField]
    private Toggle toggle;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject targetObject;
    private bool TargetBool
    {
        get => targetObject.GetComponent<IUIToggleSliderVO>().GetOptionValueBool();
        set => targetObject.GetComponent<IUIToggleSliderVO>().SetOptionValue(value);
    }

    private float TargetFloat
    {
        get => targetObject.GetComponent<IUIToggleSliderVO>().GetOptionValueFloat();
        set => targetObject.GetComponent<IUIToggleSliderVO>().SetOptionValue(value);
    }

    private void Awake()
    {
        toggle.isOn = TargetBool;
        slider.value = TargetFloat;
    }

    public void OnOptionSliderChange()
    {
        toggle.isOn = slider.value > 0;
        TargetFloat = slider.value;
        targetObject.GetComponent<IUIToggleSliderAction>().ToggleSliderAction();
        Debug.Log(slider.value);
    }
    public void OnOptionToggleChange()
    {
        TargetBool = toggle.isOn;
        targetObject.GetComponent<IUIToggleSliderAction>().ToggleSliderAction();
    }
}
