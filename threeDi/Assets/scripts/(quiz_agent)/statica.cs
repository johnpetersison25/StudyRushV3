using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class statica : MonoBehaviour
{
    public static int collectedNotes;
    public GameObject[] teacherObjects; // Assign in Inspector ✅
    public static GameObject[] teachers; // Access from anywhere ✅
    public TMP_Text collectedNotesTXT;
    public static int difficultySpeed;
    

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
            collectedNotes = 0;
        }
    }



    public void easy()
    {
        difficultySpeed = 7;
    }

    public void medium()
    {
        difficultySpeed = 10;
    }

    public void hard()
    {
        difficultySpeed = 12;
    }
}