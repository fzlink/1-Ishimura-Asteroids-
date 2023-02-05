using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTraveler : MonoBehaviour
{
    [SerializeField] private InvisibilityDetector InvisibilityDetector;
    private Camera _mainCamera;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        InvisibilityDetector.Detector_OnInvisible += Travel;
    }

    private void Travel()
    {
        var disappearPos = transform.position;
        var min = _mainCamera.ScreenToWorldPoint(Vector2.zero);
        var max = _mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        if (min.x > disappearPos.x)
        {
            disappearPos.x = max.x;
        }
        else if (max.x < disappearPos.x)
        {
            disappearPos.x = min.x;
        }
        else if (min.y > disappearPos.y)
        {
            disappearPos.y = max.y;
        }
        else if (max.y < disappearPos.y)
        {
            disappearPos.y = min.y;
        }

        transform.position = disappearPos;
    }
}
