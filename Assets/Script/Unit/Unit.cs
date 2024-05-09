using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    GridPosition currentGridPosition;
    [SerializeField] MoveAction moveAction;

    private void Start()
    {
        currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(currentGridPosition, this);
    }

    private void Update()
    {
        GridPosition nextGridPos = LevelGrid.Instance.GetGridPosition(transform.position);
        if(nextGridPos != currentGridPosition)
        {
            LevelGrid.Instance.ChangeUnitToGrid(currentGridPosition, nextGridPos, this);
            currentGridPosition = nextGridPos;
        }
    }

    public MoveAction GetMoveAction() => moveAction;

}
