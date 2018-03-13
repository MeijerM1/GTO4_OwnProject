using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    public PlaceManager PlaceManager;
    public GameObject Prototype;

    public void SetSpawnUnit()
    {
        PlaceManager.ObjectToPlace = Prototype;
    }
}
