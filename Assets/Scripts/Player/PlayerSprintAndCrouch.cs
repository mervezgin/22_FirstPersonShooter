using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerFootSteps playerFootSteps;
    private PlayerStats playerStats;
    private Transform lookRoot;
    private float sprintSpeed = 8.0f;
    private float moveSpeed = 4.0f;
    private float crouchSpeed = 1.5f;
    private float standHeight = 1.6f;
    private float crouchHeight = 1.0f;
    private float sprintVolume = 1f;
    private float crouchVolume = 0.1f;
    private float walkVolumeMin = 0.2f, walkVolumeMax = 0.6f;
    private float walkStepDistance = 0.4f;
    private float sprintStepDistance = 0.25f;
    private float crouchStepDistance = 0.5f;
    private float sprintValue = 100.0f;
    [SerializeField] private float sprintThreshold = 10.0f;
    private bool isCrouching;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerFootSteps = GetComponentInChildren<PlayerFootSteps>();
        playerStats = GetComponent<PlayerStats>();
        lookRoot = transform.GetChild(0);
    }
    private void Start()
    {
        playerFootSteps.volumeMin = walkVolumeMin;
        playerFootSteps.volumeMax = walkVolumeMax;
        playerFootSteps.stepDistance = walkStepDistance;
    }
    private void Update()
    {
        Sprint();
        Crouch();
    }
    private void Sprint()
    {
        if (sprintValue > 0.0f)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
            {
                playerMovement.playerSpeed = sprintSpeed;
                playerFootSteps.stepDistance = sprintStepDistance;
                playerFootSteps.volumeMin = sprintVolume;
                playerFootSteps.volumeMax = sprintVolume;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.playerSpeed = moveSpeed;

            playerFootSteps.stepDistance = walkStepDistance;
            playerFootSteps.volumeMin = walkVolumeMin;
            playerFootSteps.volumeMax = walkVolumeMax;
        }
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            sprintValue -= sprintThreshold * Time.deltaTime;
            if (sprintValue <= 0f)
            {
                sprintValue = 0f;
                playerMovement.playerSpeed = moveSpeed;

                playerFootSteps.stepDistance = walkStepDistance;
                playerFootSteps.volumeMin = walkVolumeMin;
                playerFootSteps.volumeMax = walkVolumeMax;
            }
            playerStats.DisplayStaminaStats(sprintValue);
        }
        else
        {
            if (sprintValue != 100f)
            {
                sprintValue += (sprintThreshold / 2f) * Time.deltaTime;
                playerStats.DisplayStaminaStats(sprintValue);
                if (sprintValue > 100f)
                {
                    sprintValue = 100f;
                }
            }
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

                playerFootSteps.stepDistance = walkStepDistance;
                playerFootSteps.volumeMin = walkVolumeMin;
                playerFootSteps.volumeMax = walkVolumeMax;

                isCrouching = false;
            }
            else
            {
                lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);

                playerMovement.playerSpeed = crouchSpeed;
                playerFootSteps.stepDistance = crouchStepDistance;
                playerFootSteps.volumeMin = crouchVolume;
                playerFootSteps.volumeMax = crouchVolume;

                isCrouching = true;
            }
        }
    }
}
