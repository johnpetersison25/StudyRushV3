using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InteractBook : MonoBehaviour
{
    public TMP_Text interactText;
    [SerializeField] GameObject toDestroy;
    [SerializeField] string nextSceneName;
    public bool canInteract = false;

    void Start()
    {
        Color color = interactText.color;
        color.a = 0;
        interactText.color = color;
    }

    public void ExamineBook()
    {
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
        Debug.Log("scene loaded");

        Color color = interactText.color;
        color.a = 0;
        interactText.color = color;

        Destroy(toDestroy);
        Manager.collectedNotes++;
    }
}
