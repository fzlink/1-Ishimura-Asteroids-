using System;
using UnityEngine;

public class InvisibilityDetector : MonoBehaviour
{

    public Action Detector_OnInvisible;
    private void OnBecameInvisible()
    {
        Detector_OnInvisible?.Invoke();
    }
}
