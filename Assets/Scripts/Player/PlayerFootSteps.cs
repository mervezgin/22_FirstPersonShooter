using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{
    [SerializeField] private AudioClip[] footStepClip;
    private CharacterController characterController;
    private AudioSource footStepSound;
    [HideInInspector] public float volumeMin, volumeMax;
    [HideInInspector] public float stepDistance;
    private float accumulatedDistance; //how far can we go before play sound 
    private void Awake()
    {
        footStepSound = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }
    private void Update()
    {
        CheckToPlayFootstepSound();
    }
    private void CheckToPlayFootstepSound()
    {
        if (!characterController.isGrounded) return;
        if (characterController.velocity.sqrMagnitude > 0)
        {
            //accumulatedDistance is the value how far can we go 
            //make a step or sprint or move while crouching
            //until we play the footstepSound
            accumulatedDistance += Time.deltaTime;

            if (accumulatedDistance > stepDistance)
            {
                footStepSound.volume = Random.Range(volumeMin, volumeMax);
                footStepSound.clip = footStepClip[Random.Range(0, footStepClip.Length)];
                footStepSound.Play();

                accumulatedDistance = 0;
            }
        }
        else
        {
            accumulatedDistance = 0;
        }
    }
}
