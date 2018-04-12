using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

	public int xPos;
    public int yPos;

    public Unit unit;

    private Color _startcolor;

    public PlaceManager PlaceManager;
    public Map Map;

    private void Start()
    {
        _startcolor = GetComponent<Renderer>().material.color;
    }

    void OnMouseEnter()
    {
        if (PlaceManager == null) return;
        
        if(ShowPlacementHighlight()) return;        
    }

    public void Highlight(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    public void RemoveHighligh()
    {
        GetComponent<Renderer>().material.color = _startcolor;
    }

    private bool ShowPlacementHighlight()
    {
        if (PlaceManager.ObjectToPlace == null) return false;
        
        Map.HighLightCells(this, PlaceManager.ObjectToPlace);

        return true;
    }
}
