using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;
using UnityEditor;
using UnityEngine.Advertisements;

public class Unit : Ownable
{
    public int MaxHitpoints;
    public int CurrentHitpoints;
    public int Firepower;
    public int RemainingFirepower;
    public int MaxMovementRange;
    public int RemainingMovementRange;
    public int Length;

    public Map Map;

    public List<Cell> Locations = new List<Cell>();

    // 0: vertical
    // 1: horizontal
    public int Orientation = 0;

    public void Turn()
    {
        Orientation = Orientation == 0 ? 1 : 0;
    }

    public void Select()
    {
        foreach (var cell in Locations)
        {
            cell.Highlight(Color.cyan);
        }
    }

    public void Deselect()
    {
        foreach (var cell in Locations)
        {
            cell.RemoveHighligh();
        }
    }

    public List<Cell> GetRotationPoints()
    {
        var rotationsPoints = new List<Cell>();

        if (Orientation == 0)
        {
            var cellTop = Locations.Where(c => c.YPos == Locations.Max(x => x.YPos));
            var cellBot = Locations.Where(c => c.YPos == Locations.Min(x => x.YPos));

            rotationsPoints.AddRange(cellTop);
            rotationsPoints.AddRange(cellBot);
        }
        else
        {
            var right = Locations.Where(c => c.XPos == Locations.Max(x => x.XPos));
            var left = Locations.Where(c => c.XPos == Locations.Min(x => x.XPos));

            rotationsPoints.AddRange(right);
            rotationsPoints.AddRange(left);
        }

        return rotationsPoints;
    }

    public void ShowRotationOptions()
    {

        player.IsRotating = true;
        Map.ResetHighlight();
        
        var cells = GetRotationPoints();

        foreach (var anchorCell in cells)
        {
            List<Cell> turnCells;
            List<Cell> turnCells2;

            if (Orientation == 0)
            {
                // Get options going in both direction from the ships anchor points.
                //    2            1
                // {----}shipPos{----}
                turnCells = Map.GetCellsInRow(anchorCell, Length, 1);
                turnCells2 = Map.GetCellsInInvertedRow(anchorCell, Length, 1);
                
                ShowHighlight(turnCells, 0);
                ShowHighlight(turnCells2, 0);

            }
            else
            {
                turnCells = Map.GetCellsInRow(anchorCell, Length, 0);
                turnCells2 = Map.GetCellsInInvertedRow(anchorCell, Length, 0);
                
                ShowHighlight(turnCells, 1);
                ShowHighlight(turnCells2, 1);

            }

        }
    }

    private void ShowHighlight(List<Cell> cells, int orientation)
    {
        if (cells.Count >= Length)
        {
            bool isObstructed = false;

            foreach (var turnCell in cells)
            {
                if (turnCell.Unit != null)
                {
                    if (turnCell.Unit != this)
                    {
                        isObstructed = true;
                    }
                }
            }

            if (!isObstructed)
            {
                if (orientation == 0)
                {
                    var left = cells.Where(c => c.XPos == cells.Min(x => x.XPos));

                    foreach (var cell in left)
                    {
                        cell.Highlight(Color.green);
                        cell.IsRotateTarget = true;
                    }
                }
                else
                {
                    var top = cells.Where(c => c.YPos == cells.Max(x => x.YPos));

                    foreach (var cell in top)
                    {
                        cell.Highlight(Color.green);
                        cell.IsRotateTarget = true;
                    }
                }
            }
        }
    }
}