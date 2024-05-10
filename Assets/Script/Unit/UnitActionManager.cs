using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitActionManager : MonoBehaviour
{
    [SerializeField] Unit selectUnit; public Unit SelectUnit {  get { return selectUnit; } }

    public static UnitActionManager Instance {  get; private set; }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else { Destroy(this); }
    } // singleton

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (HandleSelecUnit()) return;

            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            if(selectUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
            {
                selectUnit.GetMoveAction().Move(mouseGridPosition);
            }
        }
    }

    private bool HandleSelecUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, 1 << 7))
        {
            hitInfo.transform.TryGetComponent<Unit>(out Unit unit);
            ChangeUnit(unit);
            return true;
        }
        return false;
    }

    public void ChangeUnit(Unit unit)
    {
        selectUnit = unit;
        EventManager.Instance.TriggerEvent(EventType.OnSelecUnit);
    }
}
