using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    private Vector3 targetPosition;


    private void Update()
    {
        float stopDistance = .1f;
        if(Vector3.Distance(targetPosition, transform.position) > stopDistance)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * Time.deltaTime * speed;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Move(new Vector3(2, 0, 10));
        }
    }

    void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
