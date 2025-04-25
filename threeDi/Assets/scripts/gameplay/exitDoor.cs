using UnityEngine;
using TMPro;
using System.Collections;

public class InteractDoor2 : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2;

    public TMP_Text interactText;
    [SerializeField] TMP_Text warning;

    public float door1OpenAngle = 90f;
    public float door2OpenAngle = -90f;
    public float openSpeed = 2f;

    private bool isPlayerNear = false;
    private bool isOpen = false;

    private Quaternion closedRot1, closedRot2;
    private Quaternion openRot1, openRot2;

    public bool canInteract = false;
    public AudioClip soundEffect;  // Assign the audio clip in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Hide warning at start
        SetTextAlpha(warning, 0f);

        closedRot1 = door1.transform.rotation;
        closedRot2 = door2.transform.rotation;

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
                audioSource.PlayOneShot(soundEffect);
            }
            else
            {
                Debug.Log("You need to collect all 5 notes first!");
                StartCoroutine(ShowWarning());
            }
        }

        // Smooth rotation
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
        SetTextAlpha(interactText, show ? 1f : 0f);
    }

    IEnumerator ShowWarning()
    {
        warning.text = "You need to collect all 5 notes first!";
        SetTextAlpha(warning, 1f);
        yield return new WaitForSeconds(3f);
        SetTextAlpha(warning, 0f);
    }

    void SetTextAlpha(TMP_Text text, float alpha)
    {
        Color color = text.color;
        color.a = alpha;
        text.color = color;
    }
}
