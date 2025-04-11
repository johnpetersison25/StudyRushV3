using UnityEngine;

public class play : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 100f;

    private CharacterController controller;
    private Transform playerCamera;
    private float xRotation = 0f;
    private bool isLooking = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main.transform;
    }

    void Update()
    {
        HandleMouseInput();
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * walkSpeed * Time.deltaTime);
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isLooking = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isLooking = false;
        }

        if (isLooking)
        {
            RotateCamera();
        }
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
