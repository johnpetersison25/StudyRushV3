using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class statica : MonoBehaviour
{
    public static int collectedNotes;
    public GameObject[] teacherObjects; // Assign in Inspector ✅
    public static GameObject[] teachers; // Access from anywhere ✅
    public TMP_Text collectedNotesTXT;
    

    void Awake()
    {
        teachers = teacherObjects; // Transfer non-static to static at runtime
    }

    void Start()
    {
        collectedNotesTXT.text = collectedNotes.ToString();
    }

    void Update()
    {
        collectedNotesTXT.text = collectedNotes + " / 5";
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(8);
        }
    }
}