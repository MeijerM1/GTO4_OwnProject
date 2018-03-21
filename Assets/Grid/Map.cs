using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Collections;

public class Map : MonoBehaviour {

    public int SizeX;
    public int SizeY;

    public float TileOffsetX;
    public float TileOffsetY;

    public Cell Prototype;

    public PlaceManager PlaceManager;
    
    private List<Cell> highlightedCells = new List<Cell>();

    public void Start()
    {
        TileOffsetX = Prototype.gameObject.GetComponent<Renderer>().bounds.size.x;
        TileOffsetY = Prototype.gameObject.GetComponent<Renderer>().bounds.size.z;
        Spawn();
    }

    private void Spawn()
    {
        float offsetX = 0;
        for (int i = 0; i < SizeX; i++)
        {
            float offsetY = 0;
            for (int j = 0; j < SizeY; j++)
            {
                Cell newCell = Instantiate(Prototype);
                newCell.transform.SetParent(transform);
                newCell.transform.localPosition = new Vector3(offsetX, 0, offsetY);

                newCell.name = "Cell: " + i + "," + j;

                newCell.xPos = i;
                newCell.yPos = j;

                newCell.Map = this;
                newCell.PlaceManager = PlaceManager;

                offsetY += TileOffsetY;
            }

            offsetX += TileOffsetX;
        }
    }

    public Cell GetCell(int x,int y)
    {
        if (x < 0 || y < 0)
        {
            return null;
        }

        try
        {
            return transform.GetChild((x * SizeY) + y).GetComponent<Cell>();
        }
        catch (UnityException e)
        {
            //Debug.LogWarning(e.Message);
            return null;
        }
    }

    public List<Cell> GetCellsInRow(Cell startCell, int length, int orientation)
    {
        List<Cell> result = new List<Cell>();
        
        result.Add(startCell);

        for (int i = 1; i < length; i++)
        {
            switch (orientation)
            {
                case 0:
                {
                    Cell cell = GetCell(startCell.xPos, startCell.yPos - i);

                    if (cell != null)
                    {
                        result.Add(cell);                                    
                    }

                    break;
                }
                case 1:
                {
                    Cell cell = GetCell(startCell.xPos + i, startCell.yPos);    
                
                    if (cell != null)
                    {
                        result.Add(cell);                                    
                    }

                    break;
                }
            }
        }

        return result;
    }

    private void ResetHighlight()
    {
        foreach (var cell in highlightedCells)
        {
               cell.RemoveHighligh();
        }
    }

    public void HighLightCells(Cell startCell, Unit unit)
    {
        ResetHighlight();
        
        var cells = GetCellsInRow(startCell, unit.length, unit.orientation);

        highlightedCells = cells;
        
        Color color;

        if (cells.Count < unit.length)
        {
            color = Color.red;
        }
        else
        {
            color = Color.green;
        }

        foreach (var cell in cells)
        {
            cell.Highlight(color);
        }
    }
}
