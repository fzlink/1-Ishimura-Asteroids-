using System;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float BulletLife = 1.5f;
    [SerializeField] private Transform BulletPoint;
    [SerializeField] private float BulletCooldown = 0.25f;
    [SerializeField] private float ShootSpeed = 3f;

    private float _bulletTimer = 0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _bulletTimer < 0f)
        {
            Shoot();
            _bulletTimer = BulletCooldown;
        }

        _bulletTimer -= Time.deltaTime;
    }

    private void Shoot()
    {
        var bullet = (Bullet) BulletPool.Instance.GetObject();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = BulletPoint.position;
        Vector2 center = transform.position;
        bullet.Release(((Vector2)BulletPoint.position - center) * ShootSpeed);
        bullet.SetLifetime(BulletLife);
    }
}
