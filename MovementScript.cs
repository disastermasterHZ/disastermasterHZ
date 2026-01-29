using Unity.Netcode;
using UnityEngine;
using System.Collections;
using UnityEngine.Animations;
using Unity.Mathematics;

public class PlayerMovement : NetworkBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;


    [Header("Jumping")]
    public float groundDrag = 5f;
    public float jumpStrength = 7f;
    public float jumpCooldown = 0.5f;
    public float airMultiplier = 0.5f;
    private bool canJump = true;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask groundMask;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public bool isGrounded;

    [Header("Stamina System")]
    public float maxStamina = 100f;
    public NetworkVariable<float> currentStamina = new NetworkVariable<float>(100f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public float staminaRegen = 5f;
    public float sprintCost = 10f;
    public float jumpCost = 5f;
    private bool isResting = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("References")]
    public Transform orientation;
    //public Transform visualsRoot; // Your Visuals child object with Animator
    private Rigidbody rb;
    //private Animator animator; // Reference to the Animator component

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        moveSpeed = walkSpeed;

        // animator = visualsRoot.GetComponent<Animator>();
        //if (animator == null)
        //{
        //    Debug.LogError("Animator component is missing on the visualsRoot!");
        //}

        if (IsServer)
        {
            currentStamina.Value = maxStamina;
        }

        if (!IsOwner)
        {
            GetComponentInChildren<AudioListener>().enabled = false;
        }

        Debug.Log($"[{NetworkManager.Singleton.LocalClientId}] PlayerObject: {NetworkObject.IsPlayerObject}, IsOwner: {IsOwner}, IsLocalPlayer: {IsLocalPlayer}, OwnerClientId: {NetworkObject.OwnerClientId}, IsServer: {IsServer}");

    }

    private void Update()
    {
        if (!IsOwner) return;

        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);

        MyInput();
        StateHandler();
        SpeedLimiter();
        //UpdateAnimator();

        rb.linearDamping = isGrounded ? groundDrag : 0;

        if (IsServer)
        {
            HandleStaminaRegeneration();
        }
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!IsOwner) return;

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void MyInput()
    {
        if (!IsOwner) return;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(jumpKey) && canJump)
        {
            TryJumpRpc();
        }
    }

    [Rpc(SendTo.Server)]
    private void TryJumpRpc()
    {
        if (isGrounded && canJump && currentStamina.Value >= jumpCost)
        {
            canJump = false;
            isResting = false;
            currentStamina.Value -= jumpCost;
            JumpRpc();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    [Rpc(SendTo.Owner)]
    private void JumpRpc()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
    }

    private void StateHandler()
    {
        if (!IsOwner) return;

        if (Input.GetKey(sprintKey) && isGrounded && currentStamina.Value > 5)
        {
            moveSpeed = runSpeed;
            isResting = false;
            UpdateStaminaRpc(-sprintCost * Time.deltaTime);
        }
        else if (isGrounded)
        {
            moveSpeed = walkSpeed;
            isResting = true;
        }
    }

    private void HandleStaminaRegeneration()
    {
        if (currentStamina.Value < maxStamina && isResting)
        {
            currentStamina.Value += staminaRegen * Time.deltaTime;
        }
        currentStamina.Value = Mathf.Clamp(currentStamina.Value, 0, maxStamina);
    }

    private void ResetJump()
    {
        canJump = true;
        isResting = true;
    }

    private void SpeedLimiter()
    {
        if (!IsOwner) return;

        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            rb.linearVelocity = new Vector3(flatVel.normalized.x * moveSpeed, rb.linearVelocity.y, flatVel.normalized.z * moveSpeed);
        }
    }

    [Rpc(SendTo.Server)]
    private void UpdateStaminaRpc(float staminaChange)
    {
        currentStamina.Value = Mathf.Clamp(currentStamina.Value + staminaChange, 0, maxStamina);
    }

    /*private void LateUpdate()
    {
        if (!IsOwner) return;

         Smoothly rotate visuals to face movement direction
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            visualsRoot.rotation = Quaternion.Lerp(
                visualsRoot.rotation,
                Quaternion.LookRotation(new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z)),
                Time.deltaTime * 10f
            );
        }
    }

    private void UpdateAnimator()
    {
        if (!IsOwner) return;

        SetFacingDirection();

        if (animator == null) return;

        bool isMoving = horizontalInput != 0 || verticalInput != 0;

        animator.SetBool("isWalking", isMoving && moveSpeed == walkSpeed);
        animator.SetBool("isRunning", isMoving && moveSpeed == runSpeed);
        animator.SetBool("isIdle", !isMoving && isGrounded);
        animator.SetBool("isJumping", !isGrounded);
    }
    
    private void SetFacingDirection()
    {
        if (!IsOwner) return;

        if (!isGrounded || (moveSpeed != runSpeed) || (horizontalInput == 0 && verticalInput == 0))
        {
            animator.SetInteger("FacingDirection", -1);
            return;
        }

        int facingDir = -1;

         Prioritize forward/back if both axes are pressed
        if (Mathf.Abs(verticalInput) >= 0.1f)
        {
            if (verticalInput > 0)
                facingDir = 0; // Forward
            else
                facingDir = 1; // Back
        }
        else if (Mathf.Abs(horizontalInput) >= 0f && Mathf.Abs(verticalInput) == 0)
        {
            if (horizontalInput > 0)
                facingDir = 3; // Right
            else
                facingDir = 2; // Left
        }

        animator.SetInteger("FacingDirection", facingDir);
    }*/
}