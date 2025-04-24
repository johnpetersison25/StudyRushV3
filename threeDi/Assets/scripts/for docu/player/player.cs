using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float dashSpeed = 10f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 100f;

    private CharacterController controller;
    private Transform playerCamera;
    private float xRotation = 0f;
    private bool isLooking = false;

    InteractBook _interactBook;
    InteractDoor _interactDoor;

    [Header("Stamina Settings")]
    public RawImage staminaBar;
    public float barMaxWidth = 100f;
    public float stamina = 100f;
    public float maxStamina = 100f;
    public float dashCostPerSecond = 20f;
    public float staminaRegenPerSecond = 10f;
    private bool isDashing = false;

    void Start()
    {
        _interactBook = FindAnyObjectByType<InteractBook>();
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main.transform;
    }

    void Update()
    {
        HandleMouseInput();
        HandleDashInput();
        PlayerMovement();
        UpdateStamina();
        HandleInteraction();
    }

    void PlayerMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        float currentSpeed = isDashing && stamina > 0 ? dashSpeed : walkSpeed;
        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    void HandleDashInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            isDashing = true;
            stamina -= dashCostPerSecond * Time.deltaTime;
            if (stamina < 0) stamina = 0;
        }
        else
        {
            isDashing = false;
        }
    }

    void UpdateStamina()
    {
        if (!isDashing && stamina < maxStamina)
        {
            stamina += staminaRegenPerSecond * Time.deltaTime;
            if (stamina > maxStamina) stamina = maxStamina;
        }

        if (staminaBar != null)
        {
            float width = (stamina / maxStamina) * barMaxWidth;
            staminaBar.rectTransform.sizeDelta = new Vector2(width, staminaBar.rectTransform.sizeDelta.y);
        }
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

    private InteractBook currentBook;

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("book"))
    {
        InteractBook book = other.GetComponent<InteractBook>();
        if (book != null)
        {
            currentBook = book;

            Color color = book.interactText.color;
            color.a = 1;
            book.interactText.color = color;
            book.canInteract = true;
        }
    }
}

private void OnTriggerExit(Collider other)
{
    if (other.CompareTag("book"))
    {
        InteractBook book = other.GetComponent<InteractBook>();
        if (book != null)
        {
            Color color = book.interactText.color;
            color.a = 0;
            book.interactText.color = color;
            book.canInteract = false;

            if (currentBook == book)
            {
                currentBook = null;
            }
        }
    }
}

void HandleInteraction()
{
    if (Input.GetKeyDown(KeyCode.E) && currentBook != null && currentBook.canInteract)
    {
        currentBook.ExamineBook();
        currentBook = null;
        Time.timeScale = 0;
    }
}
}
