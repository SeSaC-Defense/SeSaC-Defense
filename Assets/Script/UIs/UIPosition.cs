using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIPosition : MonoBehaviour
{
    public void MoveTo(Transform target)
    {
        Vector3 targetPosition = CameraManager.Instance.CurrentCamera.WorldToScreenPoint(target.position);
        targetPosition.z = this.transform.position.z;

        transform.position = targetPosition;
    }
}
