using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    Grid grid;
    [SerializeField] TextMeshPro textMeshPro;

    public void SetGridObject(Grid grid)
    {
        this.grid = grid;
    }

    private void Update()
    {
        textMeshPro.text = this.grid.ToString();
    }
}
