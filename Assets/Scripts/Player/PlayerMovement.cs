using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private CharacterController characterController;
    private Vector3 moveDirection;
    private float moveSpeed = 5.0f;
    private float gravity = 20.0f;
    private float jumpForce = 10.0f;
    private float verticalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        moveDirection = new Vector3(Input.GetAxis(HORIZONTAL), 0.0f, Input.GetAxis(VERTICAL));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed * Time.deltaTime;

        ApplyGravity();

        characterController.Move(moveDirection);
    }

    private void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        PlayerJump();

        moveDirection.y = verticalVelocity * Time.deltaTime;
    }
    private void PlayerJump()
    {
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
        }
    }
}
