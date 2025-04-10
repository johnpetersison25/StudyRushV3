using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject door; // Assign in inspector
    public float openAngle = 90f;
    public float openSpeed = 2f;

    private bool isPlayerNear = false;
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = door.transform.rotation;
        openRotation = Quaternion.Euler(door.transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
        }

        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        door.transform.rotation = Quaternion.Lerp(door.transform.rotation, targetRotation, Time.deltaTime * openSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
