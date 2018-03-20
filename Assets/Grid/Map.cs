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
            Debug.LogWarning(e.Message);
            return null;
        }
    }

    public List<Cell> GetCellsInRow(Cell startCell, int length, int orientation)
    {
        List<Cell> result = new List<Cell>();

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
}
