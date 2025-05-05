using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform camTransform;
    public float rotationSpeed = 40f;
    public float moveSpeed = 1f;
    bool turnCameraLeft;
    bool turnCameraRight;
    bool moveFoward;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        UnityEngine.XR.InputTracking.Recenter();
    }

    // Update is called once per frame
    void Update()
    {
        if (turnCameraLeft)
        {
            controller.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }else if (turnCameraRight)
        {
            controller.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (moveFoward)
        {
            //Vector3 moveDirection = camTransform.forward * moveSpeed * Time.deltaTime;
            Vector3 moveDirection = new Vector3(camTransform.forward.x * moveSpeed * Time.deltaTime, -1f, camTransform.forward.z * moveSpeed * Time.deltaTime);
            controller.Move(moveDirection);
        }

    }

    public void StartTurnCameraLeft()
    {
        turnCameraLeft = true;
    }

    public void StopTurnCameraLeft()
    {
        turnCameraLeft = false;
    }

    public void StartTurnCameraRight()
    {
        turnCameraRight = true;
    }

    public void StopTurnCameraRight()
    {
        turnCameraRight = false;
    }

    public void StartMoveFoward()
    {
        moveFoward = true;
    }

    public void StopMoveFoward()
    {
        moveFoward = false;
    }
}
