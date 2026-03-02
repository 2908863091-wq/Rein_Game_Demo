using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CuttingObjects
{
    public KitchenObject input;
    public KitchenObject output;
    public int cuttingNumber_max;

}
[CreateAssetMenu()]
public class CuttingObjectsList : ScriptableObject
{
    public List<CuttingObjects> list;

    public KitchenObject GetOutput(KitchenObject input)
    {
        foreach (CuttingObjects obj in list)
        {
            if (obj.input == input)
            {
                return obj.output;
            }
        }
        return null;
    }
    public bool TryGetCuttingObjects(KitchenObject input,out CuttingObjects cuttingObjects )
    {
        foreach (CuttingObjects obj in list)
        {
            if (obj.input == input)
            {
                cuttingObjects = obj;
                return true;
            }
        }
        cuttingObjects = null;
        return false;
    }
}
