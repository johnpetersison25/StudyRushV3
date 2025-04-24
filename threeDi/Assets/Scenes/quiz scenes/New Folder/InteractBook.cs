using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.AI;

public class InteractBook : MonoBehaviour
{
    [SerializeField] GameObject toDestroy;
    public TMP_Text interactText;
    public bool canInteract = false;


    private void Start()
    {
        Color color = interactText.color;
        color.a = 0; // Make text invisible
        interactText.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger: " + other.name);

            Color color = interactText.color;
            color.a = 1; // Make text visible
            interactText.color = color;

            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger: " + other.name);

            Color color = interactText.color;
            color.a = 0; // Hide text
            interactText.color = color;

            canInteract = false;
        }
    }



    public void ExamineBook()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Additive);

        Color color = interactText.color;
            color.a = 0; // Hide text
            interactText.color = color;

        Destroy(toDestroy);
        Manager.collectedNotes++;
    }
}



    