using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class player_movement : MonoBehaviour
{
    // movement speed and jump settings
    [Header("Movement Velocity")]
    public float speed;
    public float groundDrag;

    // navigation systems and inputs
    [Header("Navigation")]
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    public Vector3 moveDirection;
    Rigidbody rb;

    public bool playerControlled = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    } 

    private void MyInput()
    {
        if (playerControlled)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }
        else
        {
            horizontalInput = 0;
            verticalInput = 0;
        }
       
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
}
