using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarViewController : MonoBehaviour
{
    private Transform _mainCameraTransform;

    private void Start()
    {
        _mainCameraTransform = Camera.main.transform;
    }
    void Update()
    {
        transform.rotation = Quaternion.Euler(0F, _mainCameraTransform.eulerAngles.y, 0F);
    }
}
