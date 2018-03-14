using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlaceManager : MonoBehaviour
{

    public GameObject ObjectToPlace;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ObjectToPlace != null)
            {
                PlaceObject();
            }
        }
    }

    private void PlaceObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.GetChildCount() < 1)
            {
                GameObject newUnit = Instantiate(ObjectToPlace);

                newUnit.transform.SetParent(hit.transform.gameObject.transform, false);                
            }
            else
            {
                Debug.LogWarning("Cell already occupied");
                
            }
        }
    }
}
