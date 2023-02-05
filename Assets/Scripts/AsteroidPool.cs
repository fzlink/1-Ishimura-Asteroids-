using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPool : GOPool
{
    private static AsteroidPool _instance;
    public static AsteroidPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AsteroidPool>();
            }

            return _instance;
        }
    }
    
    protected override void Awake()
    {
        _instance = this;
        base.Awake();
    }
}
