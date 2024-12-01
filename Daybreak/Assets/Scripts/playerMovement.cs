using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class playerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float heatDamage;
    public float health = 1;
    public Image heatBar;
    public Rigidbody rb;
    public Transform sunTransform;
    public float moveSpeed = 5f;
    public spacerMovement playerControls;
    public int adjust;
    public float breakForce;
    public AudioClip moveSound;
    public AudioClip hurtSound;
    public AudioSource MoveSource;
    public AudioSource hurtSource;
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
        moveRotation = new Vector3(0, 0, rotate.ReadValue<float>());
        if (moveDirection != Vector3.zero || moveRotation != Vector3.zero)
        {
            if (!MoveSource.isPlaying)
            {
                MoveSource.Play();
            }
        }
        else
        {
            MoveSource.Stop();
        }
        if (health <= 0)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("death");
        }
    }

    private void FixedUpdate()
    {
        sunlight = !Physics.Raycast((rb.position + new Vector3(0.5f,0,0)), (sunTransform.forward * -1), out RaycastHit hit, Mathf.Infinity);
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

        if (sunlight)
        {
            heatBar.rectTransform.localScale = new Vector3(health -= heatDamage * Time.fixedDeltaTime, 1, 1);
            if (health <= 0)
            {
                health = 0;
            }
            hurtSource.PlayOneShot(hurtSound);
        }
        else if (health < 1)
        {
            heatBar.rectTransform.localScale = new Vector3(health += Time.deltaTime * heatDamage,1, 1);
            if (health > 1)
            {
                health = 1;
            }

        }
    }
}
