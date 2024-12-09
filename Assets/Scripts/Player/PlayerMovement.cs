using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private CharacterController characterController;
    private Vector3 moveDirection;
    public float playerSpeed = 4.0f;
    private float gravity = 20.0f;
    private float jumpForce = 10.0f;
    private float verticalVelocity;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        moveDirection = new Vector3(Input.GetAxis(HORIZONTAL), 0.0f, Input.GetAxis(VERTICAL));

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= playerSpeed * Time.deltaTime;

        HandleGravity();

        characterController.Move(moveDirection);
    }

    private void HandleGravity()
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
