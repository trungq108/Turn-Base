using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float rotateSpeed = 10f;

    GridPosition currentGridPosition;
    float stopDistance = .1f;
    Vector3 targetPosition;

    private void Awake()
    {
        targetPosition = transform.position;
    }

    private void Start()
    {
        currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(currentGridPosition, this);
    }

    private void Update()
    {
        if (Vector3.Distance(targetPosition, transform.position) > stopDistance)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * Time.deltaTime * moveSpeed;
            transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime * rotateSpeed);

            animator.SetBool("IsWalking", true);
        }
        else { animator.SetBool("IsWalking", false); }

        GridPosition nextGridPos = LevelGrid.Instance.GetGridPosition(transform.position);
        if(nextGridPos != currentGridPosition)
        {
            LevelGrid.Instance.ChangeUnitToGrid(currentGridPosition, nextGridPos, this);
            currentGridPosition = nextGridPos;
        }


    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

}
