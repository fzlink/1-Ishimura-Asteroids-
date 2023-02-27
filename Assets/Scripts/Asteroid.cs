using System;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : PoolableObject
{
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private CollisionHandler CollisionHandler;
    [SerializeField] private List<Transform> Roots;
    [SerializeField] private List<Vector2> DurabilityScales;
    [SerializeField] private float ShatteredSpeed;
    [SerializeField] private ScreenTraveler ScreenTraveler;

    public event Action OnDestroyCompletely;
    
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CollisionHandler collisionHandler))
        {
            collisionHandler.Collide(typeof(Asteroid), transform.position);
        }
    }

    private void Shatter(Vector2 collisionPoint)
    {
        ScoreManager.Instance.AddScore(_durability);
        _durability--;
        AsteroidPool.Instance.ReturnToPool(this);
        if (_durability > 0)
        {
            Vector2 centerPos = transform.position;
            var direction = Vector2.Perpendicular(centerPos - collisionPoint);
            for (int i = 0; i < 2; i++)
            {
                var asteroid = (Asteroid) AsteroidPool.Instance.GetObject();
                asteroid.gameObject.SetActive(true);
                asteroid.SetAsteroidModel(_durability);
                asteroid.transform.position = collisionPoint;
                asteroid.Release(direction);
                direction = -direction;
            }
        }
        else
        {
            OnDestroyCompletely?.Invoke();
        }
    }

    public void SetAsteroidModel(int durability)
    {
        _durability = durability;
        ResetRoots();
        int index = durability - 1;
        Roots[index].localScale = DurabilityScales[index];
        Roots[index].gameObject.SetActive(true);
        CollisionHandler.OnCollided -= OnCollided;
        CollisionHandler = Roots[index].GetComponentInChildren<CollisionHandler>();
        CollisionHandler.OnCollided += OnCollided;
        ScreenTraveler.SetInvisibilityDetector();
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