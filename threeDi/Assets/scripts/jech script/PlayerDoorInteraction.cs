using UnityEngine;

public class PlayerDoorInteraction : MonoBehaviour {

    public float interactionRange = 3f;
    private GameObject currentDoorPivot = null;
    private bool isNearDoor = false;
    public GameObject interactionText;

    private bool doorOpen = false;
    private float rotationSpeed = 200f;
    private float targetAngle = -90f;

    void Update() {
        CheckForDoor();

        if (isNearDoor && Input.GetKeyDown(KeyCode.E) && !doorOpen) {
            StartCoroutine(OpenDoor(currentDoorPivot.transform));
        }
    }

    void CheckForDoor() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange);

        isNearDoor = false;
        currentDoorPivot = null;

        foreach (Collider collider in hitColliders) {
            if (collider.CompareTag("Door")) {
                currentDoorPivot = collider.gameObject;
                isNearDoor = true;
                break;
            }
        }

        if (interactionText != null) {
            interactionText.SetActive(isNearDoor);
        }
    }

    System.Collections.IEnumerator OpenDoor(Transform doorPivot) {
        doorOpen = true;

        // Store the current rotation of the doorPivot
        float currentY = doorPivot.rotation.eulerAngles.y;
        float targetY = currentY + targetAngle;

        // Start rotating around the DoorPivot's Y-axis
        while (Mathf.Abs(Mathf.DeltaAngle(doorPivot.rotation.eulerAngles.y, targetY)) > 0.5f) {
            // Smoothly rotate to the target angle around the DoorPivot
            float newY = Mathf.MoveTowardsAngle(
                doorPivot.rotation.eulerAngles.y, 
                targetY, 
                rotationSpeed * Time.deltaTime
            );

            // Apply the rotation around the Y-axis only
            doorPivot.rotation = Quaternion.Euler(doorPivot.rotation.eulerAngles.x, newY, doorPivot.rotation.eulerAngles.z);
            yield return null;
        }

        // Final adjustment to ensure the door stops at the target angle
        doorPivot.rotation = Quaternion.Euler(doorPivot.rotation.eulerAngles.x, targetY, doorPivot.rotation.eulerAngles.z);
    }
}
