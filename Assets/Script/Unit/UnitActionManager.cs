using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitActionManager : MonoBehaviour
{
    [SerializeField] Unit selectUnit; public Unit SelectUnit {  get { return selectUnit; } }
    bool isBusy;

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
        if (isBusy) return;

        if (Input.GetMouseButtonDown(0))
        {           
            if (HandleSelecUnit()) return;

            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            if(selectUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
            {
                SetBusy();
                selectUnit.GetMoveAction().Move(mouseGridPosition, ClearBusy);
            }
        }

        if(Input.GetMouseButtonUp(1))
        {
            SetBusy();
            selectUnit.GetSpinAction().Spin(ClearBusy);
        }
    }

    private void SetBusy()
    {
        isBusy = true;
    }
    private void ClearBusy()
    {
        isBusy = false;
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
