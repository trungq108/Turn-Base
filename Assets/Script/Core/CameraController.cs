using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float cameraSpeed = 10f;
    float cameraRotateSpeed = 90f;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    CinemachineTransposer cinemachineTransposer;
    Vector3 targetFollowOffset;

    private void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }



    void Update()
    {
        Vector3 inputMoveDirection = new Vector3(0, 0, 0);
        if(Input.GetKey(KeyCode.W))
        {
            inputMoveDirection.z = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDirection.z = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDirection.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDirection.x = +1;
        }
        Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
        transform.position += moveVector * Time.deltaTime * cameraSpeed;


        Vector3 rotateVector = new Vector3 (0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotateVector.y = -1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotateVector.y = +1;
        }
        transform.eulerAngles += rotateVector * Time.deltaTime * cameraRotateSpeed;


        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= 1;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += 1;
        }

        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, 2, 12);

        float zoomSpeed = 5f;
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);


    }

}
