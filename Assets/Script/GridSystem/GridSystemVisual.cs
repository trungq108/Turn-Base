using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    [SerializeField] Transform gridVisualPrefab;
    GridSystemVisualSingle[,] gridVisualArray;

    public static GridSystemVisual Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        CreatGridsVisual();
    }

    private void Update()
    {
        UpdateGridsVisual();
    }

    public void CreatGridsVisual()
    {
        gridVisualArray = new GridSystemVisualSingle[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for(int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);
                Transform gridVisualObject = Instantiate(gridVisualPrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);

                gridVisualArray[x,z] = gridVisualObject.GetComponent<GridSystemVisualSingle>(); 
            }
        }
    }

    public void HideAllGridVisual()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                gridVisualArray[x, z].Hide();
            }
        }

    }
    public void ShowGridVisual(List<GridPosition> gridPositions)
    {
        foreach(var gridPosition in gridPositions)
        {
            gridVisualArray[gridPosition.x, gridPosition.z].Show();
        }
    }

    public void UpdateGridsVisual()
    {
        Unit unit = UnitActionManager.Instance.SelectUnit;
        GridSystemVisual.Instance.HideAllGridVisual();
        GridSystemVisual.Instance.ShowGridVisual(unit.GetMoveAction().GetValidActionGridPositionList());
    }

}
