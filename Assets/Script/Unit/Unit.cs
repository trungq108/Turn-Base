using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    GridPosition currentGridPosition; public GridPosition GetGridPosition() => currentGridPosition;
    MoveAction moveAction; public MoveAction GetMoveAction() => moveAction;
    SpinAction spinAction; public SpinAction GetSpinAction() => spinAction;


    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
    }

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

}
