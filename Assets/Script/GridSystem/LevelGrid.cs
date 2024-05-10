using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] Transform debugPrefab;
    GridSystem gridSystem;
    public static LevelGrid Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else { Destroy(this); }

        gridSystem = new GridSystem(20, 10, 2);
        gridSystem.CreateDebugObjects(debugPrefab);
    }

    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        Grid grid = gridSystem.GetGrid(gridPosition);
        grid.AddUnit(unit);
    }

    public List<Unit> GetUnitAtGridPosition(GridPosition gridPosition)
    {
        Grid grid = gridSystem.GetGrid(gridPosition);
        return grid.GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        Grid grid = gridSystem.GetGrid(gridPosition);
        grid.RemoveUnit(unit);
    }

    public void ChangeUnitToGrid(GridPosition fromGrid, GridPosition toGrid ,Unit unit)
    {
        RemoveUnitAtGridPosition(fromGrid, unit);
        AddUnitAtGridPosition(toGrid, unit);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.WorldToGridPostion(worldPosition);
    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GridToWorldPostion(gridPosition);

    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        Grid grid = gridSystem.GetGrid(gridPosition);
        return grid.HasAnyUnit();
    }

}
