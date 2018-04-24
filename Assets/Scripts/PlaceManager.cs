using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class PlaceManager : MonoBehaviour
{

    public Unit ObjectToPlace;
    public Player Player;
    public Map Map;

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
            
            if(cell != null && cell.Unit == null)
            {                
                if (cell.Map != Map) return;

                if (!CheckSpace(cell))
                {
                    return;
                }

                Unit newUnit = Instantiate(ObjectToPlace);
                
                Player.AudioManager.PlaySound("IntoPosition");
                                   
                newUnit.transform.SetParent(hit.transform.gameObject.transform, false);

                if (newUnit.Orientation == 0)
                {
                    hit.transform.localEulerAngles = new Vector3(0,0,0);
                }
                else
                {
                    hit.transform.localEulerAngles = new Vector3(0,-90,0);
                }

                newUnit.player = Player;
                newUnit.Map = Map;
                
                OccupyCells(cell , newUnit);
                                
                Player.Units.Add(newUnit);

                Map.ResetHighlight();
                
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
        var cells = Map.GetCellsInRow(cell, ObjectToPlace.Length, ObjectToPlace.Orientation);
        
        if (cells.Count < ObjectToPlace.Length)
        {
            return false;
        }
        
        foreach (var c in cells)
        {
            if (c.Unit != null)
            {
                Debug.LogWarning("Cell already occupied");
                return false;
            }
        }

        return true;
    }

    private void OccupyCells(Cell cell, Unit unit)
    {        
        var cells = Map.GetCellsInRow(cell, ObjectToPlace.Length, ObjectToPlace.Orientation);

        unit.Locations = cells;
        
        foreach (var c in cells)
        {
            c.Unit = unit;
        }

    }
}
