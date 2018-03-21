using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class PlaceManager : MonoBehaviour
{

    public Unit ObjectToPlace;
    public Player player;
    public Map map;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ObjectToPlace != null)
            {
                PlaceObject();
            }
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            if (ObjectToPlace != null)
            {
                ObjectToPlace.Turn();
            }
        }
    }

    private void PlaceObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Cell cell = hit.transform.GetComponent<Cell>();
            
            if(cell != null && cell.unit == null)
            {

                if (!CheckSpace(cell))
                {
                    return;
                }

                Unit newUnit = Instantiate(ObjectToPlace);
                                   
                newUnit.transform.SetParent(hit.transform.gameObject.transform, false);

                // FIXME better rotation solution.
                if (newUnit.orientation == 1)
                {
                    hit.transform.Rotate(0, -90, 0);
                }                                
                
                OccupyCells(cell , newUnit);
                
                player.units.Add(newUnit);

                ObjectToPlace = null;                
            }
            else
            {
                Debug.LogWarning("Cell already occupied");
                
            }
        }
    }

    private bool CheckSpace(Cell cell)
    {
        var cells = map.GetCellsInRow(cell, ObjectToPlace.length, ObjectToPlace.orientation);
        
        if (cells.Count < ObjectToPlace.length)
        {
            return false;
        }
        
        foreach (var c in cells)
        {
            if (c.unit != null)
            {
                Debug.LogWarning("Cell already occupied");
                return false;
            }
        }

        return true;
    }

    private void OccupyCells(Cell cell, Unit unit)
    {        
        var cells = map.GetCellsInRow(cell, ObjectToPlace.length, ObjectToPlace.orientation);
        
        foreach (var c in cells)
        {
            c.unit = unit;
        }

    }
}
