using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitFactory : MonoBehaviour
{
    public PlaceManager PlaceManager;
    public Unit Prototype;

    public List<ResourceCosts> costs = new List<ResourceCosts>();
    
    public void SetSpawnUnit()
    {
        foreach (var cost in costs)
        {
            if (!cost.CanAfford())
            {
                Debug.Log("You be broke boy");
                return;
            }
        }
        
        PlaceManager.ObjectToPlace = Prototype;
        
        foreach (var cost in costs)
        {
            cost.Pay();
        }
    }
        
}

[System.Serializable]
public class ResourceCosts
{
    public Resource Resource;
    public int Costs;

    public bool CanAfford()
    {
        return Resource.CanAfford(Costs);
    }

    public void Pay()
    {
        Resource.RemoveAmount(Costs);
    }
}
