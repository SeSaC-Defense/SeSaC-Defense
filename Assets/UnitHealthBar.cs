using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealthBar : MonoBehaviour
{
    private Transform unitTransform;
    private Slider slider;

    public void Setup(ulong clientId, Transform unitTransform)
    {
        this.unitTransform = unitTransform;
        this.slider = GetComponent<Slider>();

        if (clientId == NetworkManager.ServerClientId)
        {
            transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.green;
        }
        else
        {
            transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.red;
        }
        
        transform.SetParent(GameObject.Find("Canvas").transform.Find("CanvasEnemyHealth").transform);
    }

    private void Update()
    {
        if (unitTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 unitPosition = unitTransform.position;
        unitPosition.y -= 0.8f;

        transform.position = CameraManager.Instance.CurrentCamera.WorldToScreenPoint(unitPosition);

        Unit unit = unitTransform.GetComponent<Unit>();
        slider.value = unit.UnitCurrentHealth / unit.UnitMaxHealth;
    }
}
