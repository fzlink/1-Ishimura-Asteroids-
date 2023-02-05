using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : GOPool
{
    private static BulletPool _instance;
    public static BulletPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BulletPool>();
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
