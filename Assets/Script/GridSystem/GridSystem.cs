using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class GridSystem
{   
    int width;
    int height;
    int cellSize;
    Grid[,] gridArray;

    public GridSystem(int width, int height, int cellSize)
    {        
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        gridArray = new Grid[this.width, this.height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Grid grid = new Grid(this, new GridPosition(x, z));
                gridArray[x,z] = grid;
            }
        }
    }

    public Vector3 GridToWorldPostion(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    public GridPosition WorldToGridPostion(Vector3 worldPosition)
    {
        return new GridPosition(Mathf.RoundToInt(worldPosition.x / cellSize),
                                Mathf.RoundToInt(worldPosition.z / cellSize));
    }

    public void CreateDebugObjects(Transform gridDebugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridDebugObject = GameObject.Instantiate(gridDebugPrefab, GridToWorldPostion(gridPosition), Quaternion.identity);
                gridDebugObject.GetComponent<GridDebugObject>().SetGridObject(GetGrid(gridPosition));
            }
        }
    }

    public Grid GetGrid(GridPosition gridPosition)
    {
        return gridArray[gridPosition.x, gridPosition.z];
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridPosition.x >= 0 &&
               gridPosition.z >= 0 &&
               gridPosition.x < width &&
               gridPosition.z < height;

    }
}
