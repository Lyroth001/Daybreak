using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class playerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody rb;
    public float moveSpeed = 5f;
    public spacerMovement playerControls;
    public int adjust;
    public float breakForce;
    private Vector3 moveDirection;
    private float verticalDirection;
    private float verticalRotation;
    private float horizontalRotation;
    private InputAction move;
    private InputAction ascend;
    private InputAction descend;
    private InputAction rotate;
    private InputAction halt;
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
        rotate = playerControls.Player.Look;
        halt = playerControls.Player.Halt;
        move.Enable();
        ascend.Enable();
        descend.Enable();
        rotate.Enable();
        halt.Enable();
        
    }

    
    private void OnDisable()
    {
        move.Disable();
        ascend.Disable();
        descend.Disable();
        rotate.Disable();
        halt.Disable();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xyForce = move.ReadValue<Vector2>();
        moveDirection.x = xyForce.x;
        moveDirection.y = xyForce.y;
        verticalDirection = ascend.ReadValue<float>() - descend.ReadValue<float>();
        horizontalRotation = rotate.ReadValue<Vector2>().x;
        verticalRotation = rotate.ReadValue<Vector2>().y;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity += new Vector3(moveDirection.x/adjust, verticalDirection/adjust, moveDirection.y/adjust);
        rb.angularVelocity += new Vector3(verticalRotation/adjust, horizontalRotation/adjust, 0);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, adjust);
        rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, adjust);
        if (halt.ReadValue<float>() != 0)
        {
            //invert all directions
            rb.linearVelocity += rb.linearVelocity*breakForce;
            rb.angularVelocity += rb.angularVelocity*breakForce;
        }
    }
}
