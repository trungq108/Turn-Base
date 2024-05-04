using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;



public class GridSystem
{   
    int width;
    int height;
    int cellSize;

    public GridSystem(int width, int height, int cellSize)
    {        
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Debug.DrawLine(GridToWorldPostion(x,z), GridToWorldPostion(x,z) + Vector3.right * 0.5f, Color.red, 1000);
            }
        }
    }

    public Vector3 GridToWorldPostion(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize;
    }

    public GridPosition WorldToGridPostion(Vector3 worldPosition)
    {
        return new GridPosition(Mathf.RoundToInt(worldPosition.x / cellSize),
                                Mathf.RoundToInt(worldPosition.z / cellSize));
    }


}

public struct GridPosition
{
    int x;
    int z;

    public GridPosition(int x, int z)
    {
        this.x = x; 
        this.z = z;
        Debug.Log("x :" + x + ", " + "z :" + z);
    }

}
