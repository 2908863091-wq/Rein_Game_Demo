using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TurretData
{
    public GameObject prefab;
    public int cost;
    public GameObject nextPrefab;
    public int upCost;
    public TurretType type;
}
public enum TurretType
{
    standard,
    missile,
    laser,
}
