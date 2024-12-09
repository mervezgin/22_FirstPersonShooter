using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Transform lookRoot;
    private float sprintSpeed = 8.0f;
    private float moveSpeed = 4.0f;
    private float crouchSpeed = 1.5f;
    private float standHeight = 1.6f;
    private float crouchHeight = 1.0f;
    private bool isCrouching;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        lookRoot = transform.GetChild(0);
    }
    private void Update()
    {
        Sprint();
        Crouch();
    }
    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.playerSpeed = sprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.playerSpeed = moveSpeed;
        }
    }
    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouching)
            {
                lookRoot.localPosition = new Vector3(0f, standHeight, 0f);
                playerMovement.playerSpeed = moveSpeed;
                isCrouching = false;
            }
            else
            {
                lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                playerMovement.playerSpeed = crouchSpeed;
                isCrouching = true;
            }
        }
    }
}
