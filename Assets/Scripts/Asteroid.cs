﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : PoolableObject
{
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private CollisionHandler CollisionHandler;
    [SerializeField] private List<Transform> Roots;
    [SerializeField] private List<Vector2> DurabilityScales;
    [SerializeField] private float ShatteredSpeed;

    private int _durability;
    
    private void Awake()
    {
        CollisionHandler.OnCollided += OnCollided;
    }

    private void OnCollided(object sender, CollisionEventArgs eventArgs)
    {
        if(eventArgs.type == typeof(Bullet) || eventArgs.type == typeof(PlayerController))
            Shatter(eventArgs.collisionPoint);
    }

    private void Shatter(Vector2 collisionPoint)
    {
        _durability--;
        AsteroidPool.Instance.ReturnToPool(this);
        Vector2 centerPos = transform.position;
        var direction = Vector2.Perpendicular(centerPos - collisionPoint);
        for (int i = 0; i < 2; i++)
        {
            var asteroid = (Asteroid) AsteroidPool.Instance.GetObject();
            asteroid.SetAsteroidModel(_durability);
            transform.position = collisionPoint;
            asteroid.Release(direction);
            direction = -direction;
        }
    }

    public void SetAsteroidModel(int durability)
    {
        _durability = durability;
        ResetRoots();
        int index = durability - 1;
        Roots[index].localScale = DurabilityScales[index];
        Roots[index].gameObject.SetActive(true);
    }

    private void ResetRoots()
    {
        for (int i = 0; i < Roots.Count; i++)
        {
            Roots[i].localScale = Vector3.one;
            Roots[i].gameObject.SetActive(false);
        }
    }

    public void Release(Vector2 direction)
    {
        RB.AddForce(direction * ShatteredSpeed, ForceMode2D.Impulse);
    }
}