using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FireObjects : ScriptableObject
{
    public List<FireFood> list;

    public bool TryGetFireObjects(KitchenObject input, out FireFood fireFood)
    {
        foreach (FireFood obj in list)
        {
            if (obj.input == input)
            {
                fireFood = obj;
                return true;
            }
        }
        fireFood = null;
        return false;
    }
}
[Serializable]
public class FireFood
{
    public KitchenObject input;
    public KitchenObject output;
    public float cooking_time;
}
