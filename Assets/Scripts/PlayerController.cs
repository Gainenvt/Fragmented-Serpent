using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputActions input;
    private Rigidbody rb;
    private Player player;
    private float swimSpd = 2f;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float xRotation = 0f;

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
    Debug.Log("OnEnable started");
    Debug.Log("Input: " + input);

    input.Enable();

    Debug.Log("Input enabled");
    Debug.Log("Player map: " + input.Player);

    input.Player.Move.performed += OnMove;
    input.Player.Move.canceled += OnMove;

    input.Player.Look.performed += OnLook;
    input.Player.Look.canceled += OnLook;
    input.Player.Jump.performed += OnJump;
    input.Player.Descend.performed += OnDescend;
    input.Player.Descend.canceled += OnDescend;
}

    private void OnDisable()
    {
        Debug.Log("OnDisable started");
        if(input == null)
        {
            Debug.Log("Input is null, skipping unsubscription");
            return;
        }
        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= OnMove;

        input.Player.Look.performed -= OnLook;
        input.Player.Look.canceled -= OnLook;

        input.Player.Jump.performed -= OnJump;
        input.Player.Descend.performed -= OnDescend;
        input.Player.Descend.canceled -= OnDescend;

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
    private void OnDescend(InputAction.CallbackContext context)
    {
        Descend();
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
        transform.Rotate(Vector3.up * lookInput.x * LookSpeed * Time.deltaTime);

        xRotation -= lookInput.y * LookSpeed * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        PlayerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void Jump()
    {
        {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, swimSpd, rb.linearVelocity.z);
        }

    }
    private void Descend()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, -swimSpd, rb.linearVelocity.z);
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