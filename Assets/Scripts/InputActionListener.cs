using System;
using UnityEngine;

public class InputActionListener : MonoBehaviour
{
    public event Action OnActionKeyPressed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnActionKeyPressed?.Invoke();
        }
    }
}
