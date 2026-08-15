using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputActions input;
    private Rigidbody rb;
    private Player player;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private float xRotation = 0f;

    private bool isDescending;
    

    public float JumpForce = 2f;
    public float moveSPD = 3f;
    public float swimSpd = 2f;
    public float LookSpeed = 10f;
    private bool isAscending;
    public Transform PlayerCamera;


    private void Awake()
    {
        input = new PlayerInputActions();

        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
    }


    private void OnEnable()
    {
        if (input == null)
        {
            input = new PlayerInputActions();
        }

        input.Enable();

        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMove;

        input.Player.Look.performed += OnLook;
        input.Player.Look.canceled += OnLook;

        input.Player.Jump.performed += OnJump;
        input.Player.Jump.canceled += OnJump;

        input.Player.Descend.performed += OnDescend;
        input.Player.Descend.canceled += OnDescend;
    }


    private void OnDisable()
    {
        if (input == null)
            return;

        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= OnMove;

        input.Player.Look.performed -= OnLook;
        input.Player.Look.canceled -= OnLook;

        input.Player.Jump.performed -= OnJump;
        input.Player.Jump.canceled -= OnJump;

        input.Player.Descend.performed -= OnDescend;
        input.Player.Descend.canceled -= OnDescend;

        input.Disable();
    }


    private void Update()
    {
        Look();
        Swimming();
    }


    private void FixedUpdate()
    {
        Move();
        SwimVertical();
         
    }


    // =========================
    // INPUT
    // =========================

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }


    private void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }


 private void OnJump(InputAction.CallbackContext context)
{
    if (context.performed)
    {
        isAscending = true;
    }

    if (context.canceled)
    {
        isAscending = false;
    }
}

private void OnDescend(InputAction.CallbackContext context)
{   
    if(context.performed)

    {
        isDescending = true;
    }
    if(context.canceled)
    {
        isDescending = false;
    }
}


    // =========================
    // MOVEMENT
    // =========================

private void Move()
{
    Vector3 movement;

    if (player.isSubmerged)
    {
        movement =
            PlayerCamera.right * moveInput.x +
            PlayerCamera.forward * moveInput.y;
    }
    else
    {
        movement =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;
    }

    rb.linearVelocity = new Vector3(
        movement.x * moveSPD,
        rb.linearVelocity.y,
        movement.z * moveSPD
    );
}


    // =========================
    // CAMERA
    // =========================

    private void Look()
    {
        transform.Rotate(
            Vector3.up *
            lookInput.x *
            LookSpeed *
            Time.deltaTime
        );

        xRotation -=
            lookInput.y *
            LookSpeed *
            Time.deltaTime;

        xRotation = Mathf.Clamp(
            xRotation,
            -90f,
            90f
        );

        PlayerCamera.localRotation =
            Quaternion.Euler(
                xRotation,
                0f,
                0f
            );
    }


    // =========================
    // ASCEND
    // =========================

    


    // =========================
    // DESCEND
    // =========================

 private void SwimVertical()
{if (isAscending)
{
    rb.linearVelocity = new Vector3(
        rb.linearVelocity.x,
        swimSpd,
        rb.linearVelocity.z
    );
}
else if (isDescending)
{ //underwater downward movement
    rb.linearVelocity = new Vector3(
        rb.linearVelocity.x,
        -swimSpd,
        rb.linearVelocity.z
    );
}
else
{
    rb.linearVelocity = new Vector3(
        rb.linearVelocity.x,
        0f,
        rb.linearVelocity.z
    );
}
}
//gravity Check

    private void Swimming()
    {
        if (player.isSubmerged)
        {
            rb.useGravity = false;
        }
        else
        {
            rb.useGravity = true;
        }
    }
}

