using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public static WayPoint Instance { get; private set; }

    public List<Transform> wayPointsList;
    private void Awake()
    {
        Instance = this;

        wayPointsList = new List<Transform>();
        foreach (Transform child in transform)
        {
            wayPointsList.Add(child);
        }
    }
    public int GetLength()
    {
        return wayPointsList.Count;
    }
    public Vector3 GetPoint(int index)
    { 
        return wayPointsList[index].position;
    }
}
