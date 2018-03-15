using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Resource : MonoBehaviour {

    public int Amount;
    public int StartingAmount;

    public UnityEvent OnValueChange;

    public void Awake()
    {
        Amount = StartingAmount;
    }

    public void AddAmount(int amount)
    {
        Amount += amount;
        OnValueChange.Invoke();
    }

    public bool CanAfford(int costs)
    {
        return costs <= Amount;
    }

    public void RemoveAmount(int amount)
    {
        Amount -= amount;
        OnValueChange.Invoke();
    }
}
