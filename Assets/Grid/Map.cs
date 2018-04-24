using System.Collections;
using System.Collections.Generic;
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

                newCell.XPos = i;
                newCell.YPos = j;

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

    public List<Cell> GetCellsInInvertedRow(Cell startCell, int length, int orientation)
    {
        List<Cell> result = new List<Cell>();
        
        result.Add(startCell);

        for (int i = 1; i < length; i++)
        {
            switch (orientation)
            {
                case 0:
                {
                    Cell cell = GetCell(startCell.XPos, startCell.YPos + i);

                    if (cell != null)
                    {
                        result.Add(cell);                                    
                    }

                    break;
                }
                case 1:
                {
                    Cell cell = GetCell(startCell.XPos - i, startCell.YPos);    
                
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
                    Cell cell = GetCell(startCell.XPos, startCell.YPos - i);

                    if (cell != null)
                    {
                        result.Add(cell);                                    
                    }

                    break;
                }
                case 1:
                {
                    Cell cell = GetCell(startCell.XPos + i, startCell.YPos);    
                
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

    public void HighlightRotationCells(List<Cell> cells)
    {
        foreach (var cell in cells)
        {
            cell.Highlight(Color.yellow);
        }
    }

    public void ResetHighlight()
    {
        foreach (var cell in highlightedCells)
        {
               cell.RemoveHighligh();
        }
    }

    public void ResetMRotationHighlight()
    {
        foreach(var cell in transform.GetComponentsInChildren<Cell>())
        {
            cell.RemoveHighligh();
            cell.IsRotateTarget = false;
        }
    }

    public void ShowRotationOptions(List<Cell> cells, Unit unit)
    {    
        Color color = Color.green;

        foreach (var cell in cells)
        {
            if (cell.Unit != null)
            {
                if (cell.Unit != unit)
                {
                    color = Color.red;                    
                }
            }
        }

        foreach (var cell in cells)
        {
            cell.Highlight(color);
        }
    }

    public void HighLightCells(Cell startCell, Unit unit)
    {
        ResetHighlight();
        
        var cells = GetCellsInRow(startCell, unit.Length, unit.Orientation);

        highlightedCells = cells;
        
        Color color;

        if (cells.Count < unit.Length)
        {
            color = Color.red;
        }
        else
        {
            color = Color.green;
        }

        foreach (var cell in cells)
        {
            if (cell.Unit != null)
            {
                color = Color.red;
            }
        }

        foreach (var cell in cells)
        {
            cell.Highlight(color);
        }
    }
}
