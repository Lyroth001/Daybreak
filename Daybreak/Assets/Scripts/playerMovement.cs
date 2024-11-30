using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class playerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody rb;
    public Transform sunTransform;
    public float moveSpeed = 5f;
    public spacerMovement playerControls;
    public int adjust;
    public float breakForce;
    private Vector3 moveDirection;
    private Vector3 moveRotation;
    private float verticalDirection;
    private float verticalRotation;
    private float horizontalRotation;
    private InputAction move;
    private InputAction ascend;
    private InputAction descend;
    private InputAction rotate;
    private InputAction halt;
    private Vector2 xyForce;

    public bool sunlight;
    
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
        moveDirection = new Vector3(xyForce.x, (ascend.ReadValue<float>() - descend.ReadValue<float>()), xyForce.y);
        moveRotation = new Vector3(rotate.ReadValue<Vector2>().x, rotate.ReadValue<Vector2>().y,0);
    }

    private void FixedUpdate()
    {
        sunlight = !Physics.Raycast((rb.position + new Vector3(10,0,0)), (sunTransform.forward * -1), out RaycastHit hit, Mathf.Infinity);
        rb.linearVelocity += ((rb.transform.up * moveDirection.y) + (rb.transform.right * moveDirection.x) + (rb.transform.forward * moveDirection.z)) * Time.deltaTime * moveSpeed;
        rb.angularVelocity += ((rb.transform.up * moveRotation.x) + (rb.transform.right * moveRotation.y) + (rb.transform.forward * moveRotation.z)) * Time.deltaTime * moveSpeed;
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, adjust);
        rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, adjust);
        if (halt.ReadValue<float>() != 0)
        {
            //invert all directions
            rb.linearVelocity -= rb.linearVelocity*breakForce*Time.fixedDeltaTime;
            rb.angularVelocity -= rb.angularVelocity*breakForce*Time.fixedDeltaTime;
        }
    }
}
