using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSlider : MonoBehaviour
{
    [SerializeField]
    private Toggle toggle;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject targetObject;
    private bool TargetBool
    {
        get => targetObject.GetComponent<IToggleSliderVO>().GetOptionValueBool();
        set => targetObject.GetComponent<IToggleSliderVO>().SetOptionValue(value);
    }

    private float TargetFloat
    {
        get => targetObject.GetComponent<IToggleSliderVO>().GetOptionValueFloat();
        set => targetObject.GetComponent<IToggleSliderVO>().SetOptionValue(value);
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
        targetObject.GetComponent<IToggleSliderAction>().ToggleSliderAction();
        Debug.Log(slider.value);
    }
    public void OnOptionToggleChange()
    {
        TargetBool = toggle.isOn;
        targetObject.GetComponent<IToggleSliderAction>().ToggleSliderAction();
    }
}
