using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

	public int xPos;
    public int yPos;

    public Unit unit;

    private Color _startcolor;

    void OnMouseEnter()
    {
        _startcolor = GetComponent<Renderer>().material.color;

        Color newColor;
        
        if (unit != null)
        {
            newColor = Color.blue;
        }
        else
        {
            newColor = Color.yellow;
        }
        
        GetComponent<Renderer>().material.color = newColor;
    }
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = _startcolor;
    }
}
