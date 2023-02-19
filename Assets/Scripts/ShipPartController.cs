using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipPartController : MonoBehaviour
{
    private List<Rigidbody2D> parts;
    private List<Vector2> startPositions;
    private List<float> startRotations;

    private void Awake()
    {
        parts = GetComponentsInChildren<Rigidbody2D>().ToList();
    }

    private void Start()
    {
        startPositions = new List<Vector2>(parts.Count);
        startRotations = new List<float>(parts.Count);
        for (int i = 0; i < parts.Count; i++)
        {
            startPositions.Add(parts[i].transform.localPosition);
            startRotations.Add(parts[i].transform.localEulerAngles.z);
        }
    }

    public void Distribute()
    {
        foreach (var part in parts)
        {
            var x = UnityEngine.Random.Range(-1f, 1f);
            var y = UnityEngine.Random.Range(-1f, 1f);
            part.AddForce(new Vector2(x,y)*20);
        }
    }

    public void Respawn()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            parts[i].transform.localPosition = startPositions[i];
            parts[i].transform.localEulerAngles = new Vector3(0f, 0f, startRotations[i]);
        }
        gameObject.SetActive(false);
    }
    
}
