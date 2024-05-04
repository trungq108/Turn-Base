using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    GridSystem gridSystem;
    void Start()
    {
        gridSystem = new GridSystem(10, 20, 2);
    }

    void Update()
    {
        gridSystem.WorldToGridPostion(MouseWorld.GetPosition());
    }
}
