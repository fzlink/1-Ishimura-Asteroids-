using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private Transform PlayerForward;
    [SerializeField] private Transform PlayerCenter;
    [SerializeField] private float Speed;
    [SerializeField] private float RotationSpeed;
    
    private bool _boost;
    private Vector2 _directionVector;

    // Update is called once per frame
    void Update()
    {
        _boost = Input.GetKey(KeyCode.Space);
        int direction = 0;
        if (Input.GetKey(KeyCode.RightArrow))
            direction = -1;
        else if (Input.GetKey(KeyCode.LeftArrow))
            direction = 1;
        transform.Rotate(Vector3.forward * RotationSpeed * direction);
    }

    private void FixedUpdate()
    {
        if (_boost)
        {
            Vector2 forwardDir = PlayerForward.position - PlayerCenter.position;
            RB.AddForce(forwardDir * Speed, ForceMode2D.Force);
        }
        
        
    }
}
