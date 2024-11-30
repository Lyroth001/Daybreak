using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class playerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody rb;
    public float moveSpeed = 5f;
    public spacerMovement playerControls;
    private Vector3 moveDirection;
    private float verticalDirection;
    private InputAction move;
    private InputAction ascend;
    private InputAction descend;
    private Vector2 xyForce;
    
    private void Awake()
    {
        playerControls = new spacerMovement();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        ascend = playerControls.Player.Jump;
        descend = playerControls.Player.Crouch;
        move.Enable();
        ascend.Enable();
        descend.Enable();
    }
    
    private void OnDisable()
    {
        move.Disable();
        ascend.Disable();
        descend.Disable();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xyForce = move.ReadValue<Vector2>();
        moveDirection.x += xyForce.x;
        if (moveDirection.x > 10)
        {
            moveDirection.x = 10;
        }
        if (moveDirection.x < -10)
        {
            moveDirection.x = -10;
        }
        moveDirection.y += xyForce.y;
        if (moveDirection.y > 10)
        {
            moveDirection.y = 10;
        }
        if (moveDirection.y < -10)
        {
            moveDirection.y = -10;
        }
        verticalDirection += ascend.ReadValue<float>() - descend.ReadValue<float>();
        if (verticalDirection > 10)
        {
            verticalDirection = 10;
        }
        if (verticalDirection < -10)
        {
            verticalDirection = -10;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveDirection.x/10, verticalDirection/10, moveDirection.y/10);
    }
}
