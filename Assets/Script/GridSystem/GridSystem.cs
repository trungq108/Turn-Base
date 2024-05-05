using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class Grid
{
    GridSystem gridSystem;
    GridPosition gridPosition;
    List<Unit> unitList;

    public Grid(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        unitList = new List<Unit>();
    }

    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    public override string ToString()
    {
        string unitString = "";
        foreach (Unit unit in unitList)
        {
            unitString += unit + "\n";
        }
        return gridPosition.ToString() + "\n" + unitString;
    }
}

public struct GridPosition: IEquatable<GridPosition>
{
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
               x == position.x &&
               z == position.z;
    }

    public bool Equals(GridPosition other)
    {
        return this == other;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }

    public override string ToString()
    {
        return "x:" + x + "," + "z:" + z;
    }

    public static bool operator == (GridPosition a, GridPosition b)
    {
        return a.x == b.x && a.z == b.z;
    }
    public static bool operator !=(GridPosition a, GridPosition b)
    {
        return !(a == b);
    }

}

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
}
