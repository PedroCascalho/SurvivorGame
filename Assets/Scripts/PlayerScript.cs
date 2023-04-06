using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private PlayerInput playerInput;
    private CharacterController characterController;
    private Animator animator;

    private int a_isWalking;
    private int a_isRunning;

    private Vector2 playerMovementInput;
    private Vector3 playerMovement;
    private Vector3 playerRunMovement;
    private bool isMovimentPressed;
    private float rotationVelocity = 5.0f;

    private bool isRunningPressed;

    [SerializeField] private float velocity;
    [SerializeField] private float runMultipleVelocity = 3;


    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        GetAnimatorParameters();

        playerInput.Movement.Walk.started += OnMovementInput;
        playerInput.Movement.Walk.canceled += OnMovementInput;
        playerInput.Movement.Walk.performed += OnMovementInput;

        playerInput.Movement.Run.started += OnRunningInput;
        playerInput.Movement.Run.canceled += OnRunningInput;

    }

    void GetAnimatorParameters()
    {
        a_isWalking = Animator.StringToHash("isWalking");
        a_isRunning = Animator.StringToHash("isRunning");
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        playerMovementInput = context.ReadValue<Vector2>();
        playerMovement.x = playerMovementInput.x;
        playerMovement.y = 0.0f;
        playerMovement.z = playerMovementInput.y;
        playerRunMovement.x = playerMovementInput.x * runMultipleVelocity;
        playerRunMovement.z = playerMovementInput.y * runMultipleVelocity;
        isMovimentPressed = playerMovementInput.y != 0 || playerMovementInput.x != 0;
    }

    void OnRunningInput(InputAction.CallbackContext context)
    {
        isRunningPressed = context.ReadValueAsButton();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        AnimationHandler();
        PlayerRotationHandler();
    }

    private void PlayerRotationHandler()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = playerMovement.x;
        positionToLookAt.y = playerMovement.y;
        positionToLookAt.z = playerMovement.z;
        Quaternion currentRotation = transform.rotation;

        if (isMovimentPressed)
        {
            Quaternion lookAtRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = (Quaternion.Slerp(currentRotation, lookAtRotation, rotationVelocity * Time.deltaTime));
        }
    }

    private void AnimationHandler()
    {
        bool isWalkingAnimation = animator.GetBool(a_isWalking);
        bool isRunningAnimation = animator.GetBool(a_isRunning);

        if (isMovimentPressed && !isWalkingAnimation)
        {
            animator.SetBool(a_isWalking, true);
        }

        else if (!isMovimentPressed && isWalkingAnimation)
        {
            animator.SetBool(a_isWalking, false);
        }

        if (isMovimentPressed && isRunningPressed && !isRunningAnimation)
        {
            animator.SetBool(a_isRunning, true);
        }

        else if (!isMovimentPressed || !isRunningPressed && isRunningAnimation)
        {
            animator.SetBool(a_isRunning, false);
        }
    }

    private void MovePlayer()
    {
        if (isRunningPressed)
        {
            characterController.Move(playerRunMovement * Time.deltaTime * velocity);
        }
        else
        {
            characterController.Move(playerMovement * Time.deltaTime * velocity);
        }
    }

    private void OnEnable()
    {
        playerInput.Movement.Enable();
    }

    private void OnDisable()
    {
        playerInput.Movement.Disable();
    }
}
