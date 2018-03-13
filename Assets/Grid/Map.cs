using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Spawn()
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
        return transform.GetChild((x * SizeY) + y).GetComponent<Cell>();
    }
}
