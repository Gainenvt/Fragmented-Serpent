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

    private float swimSpd = 2f;

    public float JumpForce = 2f;
    public float moveSPD = 3f;
    public float LookSpeed = 10f;

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
            input = new PlayerInputActions();

        input.Enable();

        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMove;

        input.Player.Look.performed += OnLook;
        input.Player.Look.canceled += OnLook;

        input.Player.Jump.performed += OnJump;
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

        input.Disable();
    }


    private void FixedUpdate()
    {
        Move();
    }


    private void Update()
    {
        Look();
        Swimming();
    }



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
        Jump();
    }


    private void Move()
    {
        Vector3 movement =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;

        rb.linearVelocity = new Vector3(
            movement.x * moveSPD,
            rb.linearVelocity.y,
            movement.z * moveSPD
        );
    }



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


    private void Jump()
    {
        // Normal jumping while on land
        if (!player.isSubmerged)
        {
            rb.AddForce(
                Vector3.up * JumpForce,
                ForceMode.Impulse
            );
        }
        else
        {
            Ascend();
        }
    }



    private void Ascend()
    {
        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x,
            swimSpd,
            rb.linearVelocity.z
        );
    }


    private void Descend()
    {
        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x,
            -swimSpd,
            rb.linearVelocity.z
        );
    }


 

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