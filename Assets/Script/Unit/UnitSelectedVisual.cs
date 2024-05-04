using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] Unit unit;
    [SerializeField] MeshRenderer meshRenderer;

    private void Awake()
    {
        ChangeVisual();
    }

    private void Start()
    {
        EventManager.Instance.AddListener(EventType.OnSelecUnit, ChangeVisual);
    }

    void ChangeVisual()
    {
        if (UnitActionManager.Instance.SelectUnit == unit)
        {
            meshRenderer.enabled = true;
        }
        else meshRenderer.enabled = false;
    }
}
