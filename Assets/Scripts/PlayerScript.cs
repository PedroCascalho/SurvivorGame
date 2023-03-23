using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private PlayerInput playerInput;
    private CharacterController characterController;
    private Animator animator;

    private Vector2 playerMovementInput;
    private Vector3 playerMovement;
    private bool isMoving;
    private float rotationVelocity = 5.0f;

    [SerializeField] private float velocity;


    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        playerInput.Movement.Walk.started += OnMovementInput;
        playerInput.Movement.Walk.canceled += OnMovementInput;
        playerInput.Movement.Walk.performed += OnMovementInput;

    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        playerMovementInput = context.ReadValue<Vector2>();
        playerMovement.x = playerMovementInput.x;
        playerMovement.y = 0.0f;
        playerMovement.z = playerMovementInput.y;
        isMoving = playerMovementInput.y != 0 || playerMovementInput.x != 0;
        //isMoving = playerMovementInput.y != 0 || playerMovementInput.x != 0 ? true : false;
        //if(playerMovementInput.y != 0 || playerMovementInput.x != 0)
        //{
        //    isMoving = true;
        //}
        //else
        //{
        //    isMoving = false;
        //}
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

        if (isMoving)
        {
            Quaternion lookAtRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = (Quaternion.Slerp(currentRotation, lookAtRotation, rotationVelocity * Time.deltaTime));
        }
    }

    private void AnimationHandler()
    {
        if (animator.GetBool("isWalking") == false && isMoving == true)
        {
            animator.SetBool("isWalking", true);
        }

        if (animator.GetBool("isWalking") == true && isMoving == false)
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void MovePlayer()
    {
        characterController.Move(playerMovement * Time.deltaTime * velocity);
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
