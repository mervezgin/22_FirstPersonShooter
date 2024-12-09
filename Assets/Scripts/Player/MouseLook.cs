using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private const string MOUSE_X = "Mouse X";
    private const string MOUSE_Y = "Mouse Y";
    [SerializeField] private Transform playerRoot, lookRoot;
    [SerializeField] private bool invert;
    //[SerializeField] private bool canUnlock = true; //our mouse cursor, 
    [SerializeField] private float sensivity = 5.0f;
    //[SerializeField] private float smoothWeight = 0.4f;
    //[SerializeField] private float rollAngle = 10f;
    //[SerializeField] private float rollSpeed = 3.0f;

    //[SerializeField] private int smoothSteps = 10; 
    [SerializeField] private Vector2 defaultLookLimits = new Vector2(-70f, 80f);
    private Vector2 lookAngles;
    private Vector2 currentMouseLook;
    //private Vector2 smoothMove;
    //private float currentRollAngle;
    //private int lastLookFrame;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //lock cursor to the center of the game window
    }
    private void Update()
    {
        LockAndUnlockCursor();
        if (Cursor.lockState == CursorLockMode.Locked) { LookAround(); }
    }
    private void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
    private void LookAround()
    {
        currentMouseLook = new Vector2(Input.GetAxis(MOUSE_Y), Input.GetAxis(MOUSE_X));

        lookAngles.x += currentMouseLook.x * sensivity * (invert ? 1f : -1f);
        lookAngles.y += currentMouseLook.y * sensivity;

        lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimits.x, defaultLookLimits.y);
        /*
        if (lookAngles.x < -70){ lookAngles.x = -70; }
        if (lookAngles.x > 80 ){ lookAngles.x = 80; } clample bunu yapıyoruz aslında
        */

        //currentRollAngle = Mathf.Lerp(currentRollAngle, Input.GetAxisRaw(MOUSE_X) * rollAngle, Time.deltaTime * rollSpeed); if your character gets drunk you can use this 

        lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0.0f, 0.0f);
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0.0f);
    }
}
