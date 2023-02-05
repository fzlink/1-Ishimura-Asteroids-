using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event EventHandler<CollisionEventArgs> OnCollided;

    public void Collide(Type type, Vector2 collisionPoint)
    {
        OnCollided?.Invoke(this, new CollisionEventArgs(type, collisionPoint));
    }
}

public struct CollisionEventArgs
{
    public Type type;
    public Vector2 collisionPoint;

    public CollisionEventArgs(Type type, Vector2 collisionPoint)
    {
        this.type = type;
        this.collisionPoint = collisionPoint;
    }
}
