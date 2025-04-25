using UnityEngine;
using TMPro;

public class InteractDoor2 : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2;

    public TMP_Text interactText;

    public float door1OpenAngle = 90f;
    public float door2OpenAngle = -90f;
    public float openSpeed = 2f;

    private bool isPlayerNear = false;
    private bool isOpen = false;

    private Quaternion closedRot1, closedRot2;
    private Quaternion openRot1, openRot2;

    public bool canInteract = false;

    void Start()
    {
        closedRot1 = door1.transform.rotation;
        closedRot2 = door2.transform.rotation;

        // Use fixed open rotation from base rotation
        openRot1 = Quaternion.Euler(closedRot1.eulerAngles.x, closedRot1.eulerAngles.y + door1OpenAngle, closedRot1.eulerAngles.z);
        openRot2 = Quaternion.Euler(closedRot2.eulerAngles.x, closedRot2.eulerAngles.y + door2OpenAngle, closedRot2.eulerAngles.z);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (statica.collectedNotes >= 5)
            {
                isOpen = !isOpen;
            }
            else
            {
                Debug.Log("You need to collect all 5 notes first!");
            }
        }

        // Smoothly rotate to target
        door1.transform.rotation = Quaternion.Lerp(door1.transform.rotation, isOpen ? openRot1 : closedRot1, Time.deltaTime * openSpeed);
        door2.transform.rotation = Quaternion.Lerp(door2.transform.rotation, isOpen ? openRot2 : closedRot2, Time.deltaTime * openSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            ShowInteractText(true);
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            ShowInteractText(false);
            canInteract = false;
        }
    }

    void ShowInteractText(bool show)
    {
        Color color = interactText.color;
        color.a = show ? 1 : 0;
        interactText.color = color;
    }
}
