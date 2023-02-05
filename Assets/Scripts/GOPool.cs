using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GOPool : MonoBehaviour
{
    [SerializeField] private PoolableObject Prefab;
    [SerializeField] private int PoolInitSize;
    [SerializeField] private int ResizeAmount;
    
    private Stack<PoolableObject> Objects;

    protected virtual void Awake()
    {
        InitPool();
    }

    private void InitPool()
    {
        Objects = new Stack<PoolableObject>(PoolInitSize);
        for (int i = 0; i < PoolInitSize; i++)
        {
            AddNewObject();
        }
    }

    private void AddNewObject()
    {
        var obj = Instantiate(Prefab);
        obj.gameObject.SetActive(false);
        Objects.Push(obj);
    }

    public PoolableObject GetObject()
    {
        if (Objects.Count > 0)
        {
            var obj = Objects.Pop();
            return obj;
        }
        else
        {
            ResizePool();
            return GetObject();
        }
    }

    public void ReturnToPool(PoolableObject obj)
    {
        obj.gameObject.SetActive(false);
        Objects.Push(obj);
    }

    private void ResizePool()
    {
        for (int i = 0; i < ResizeAmount; i++)
        {
            AddNewObject();
        }
    }
}
