using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Unit : Ownable
{

    public int maxHitpoints;
    public int currentHitpoints;
    public int firepower;
    public int remainingFirepower;
    public int maxMovementRange;
    public int remainingMovementRange;
    public int length;

    public List<Cell> locations = new List<Cell>();
    
    // 0: vertical
    // 1: horizontal
    public int orientation = 0;


    public void Turn ()
    {
        if (orientation == 0)
        {
            orientation = 1;
        }
        else
        {
            orientation = 0;
        }
    }

    public void Select()
    {
        foreach (var cell in locations)
        {
            cell.Highlight(Color.cyan);
        }
    }

    public void Deselect()
    {
        foreach (var cell in locations)
        {
            cell.RemoveHighligh();
        }
    }
}
