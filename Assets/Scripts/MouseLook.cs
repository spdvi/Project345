using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 2f;
    private float accumulatedXRotation = 0.0f;

    [SerializeField] private Rigidbody playerRb;

    void Start()
    {
        accumulatedXRotation = 0;
    }

    void Update()
    {
        float mouseYInput = Input.GetAxis("Mouse Y");
        if (Mathf.Abs(mouseYInput) >= 0.05f)
        {          
            float angleRotationAroundXAxis = -mouseYInput * rotateSpeed;
            accumulatedXRotation += angleRotationAroundXAxis;

            if (accumulatedXRotation > 60)
            {
                accumulatedXRotation = 60f;
            }
            if (accumulatedXRotation < -60)
            {
                accumulatedXRotation = -60f;
            }
            transform.localRotation = Quaternion.Euler(accumulatedXRotation, 0, 0);
        }

        float mouseXInput = Input.GetAxis("Mouse X");
        Quaternion angleRotationAroundYAxis = Quaternion.Euler(0, mouseXInput * rotateSpeed, 0);
        playerRb.MoveRotation(playerRb.rotation * angleRotationAroundYAxis);

    }
}
