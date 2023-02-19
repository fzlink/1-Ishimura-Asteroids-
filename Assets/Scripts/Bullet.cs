using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableObject
{
    [SerializeField] private Rigidbody2D RB;

    private Coroutine _lifeRoutine;
    
    public void Release(Vector2 force)
    {
        RB.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out AsteroidCollisionHandler asteroidCollisionHandler))
        {
            asteroidCollisionHandler.Collide(typeof(Bullet), transform.position);
            BulletPool.Instance.ReturnToPool(this);
        }
    }

    public void SetLifetime(float lifetime)
    {
        _lifeRoutine = StartCoroutine(CountLifetime(lifetime));
    }

    private IEnumerator CountLifetime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        KillThisBullet();
    }

    private void KillThisBullet()
    {
        if(_lifeRoutine != null)
            StopCoroutine(_lifeRoutine);
        
        BulletPool.Instance.ReturnToPool(this);
    }
}