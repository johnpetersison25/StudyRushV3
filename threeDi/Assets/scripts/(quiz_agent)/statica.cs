using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class statica : MonoBehaviour
{
    public static int collectedNotes;
    public static int coin;

    public GameObject[] teacherObjects; // Assign in Inspector ✅
    public static GameObject[] teachers; // Access from anywhere ✅

    public TMP_Text collectedNotesTXT;
    public TMP_Text coinTXT;

    public static int difficultySpeed;
    public static int numberOfQuestions;

    // Track previous scores to avoid repeat coin adding
    private int lastFilipinoScore = -1;
    private int lastEnglishScore = -1;
    private int lastScienceScore = -1;
    private int lastHistoryScore = -1;
    private int lastMathScore = -1;

    void Awake()
    {
        teachers = teacherObjects; // Transfer non-static to static at runtime
    }

    void Start()
    {
        collectedNotesTXT.text = collectedNotes.ToString();
        coinTXT.text = coin.ToString();
    }

    void Update()
    {
        collectedNotesTXT.text = collectedNotes + " / 5";

        // Filipino
        if (FilipinoQ.FilipinoScore == 3 && FilipinoQ.filipinoQuizIsDone && lastFilipinoScore != 3)
        {
            coin += 1;
            lastFilipinoScore = 3;
        }

        // English
        if (EnglishQ.EnglishScore == 3 && EnglishQ.EnglishQuizIsDone && lastEnglishScore != 3)
        {
            coin += 1;
            lastEnglishScore = 3;
        }

        // Science
        if (scienceQ.scienceScore == 3 && scienceQ.scienceQuizIsDone && lastScienceScore != 3)
        {
            coin += 1;
            lastScienceScore = 3;
        }

        // History
        if (historyQ.historyScore == 3 && historyQ.historyQuizIsDone && lastHistoryScore != 3)
        {
            coin += 1;
            lastHistoryScore = 3;
        }

        // Math
        if (mathQ.mathScore == 3 && mathQ.mathQuizIsDone && lastMathScore != 3)
        {
            coin += 1;
            lastMathScore = 3;
        }

        coinTXT.text = "" + coin;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(8);
            collectedNotes = 0;
            coin = 0;
            difficultySpeed = 0;

            FilipinoQ.FilipinoScore = 0;
            FilipinoQ.filipinoQuizIsDone = false;
            EnglishQ.EnglishScore = 0;
            EnglishQ.EnglishQuizIsDone = false;
            scienceQ.scienceScore = 0;
            scienceQ.scienceQuizIsDone = false;
            historyQ.historyScore = 0;
            historyQ.historyQuizIsDone = false;
            mathQ.mathScore = 0;
            mathQ.mathQuizIsDone = false;
            BGM.Instance.StopMusic();
        }
    }

    public void easy()
    {
        difficultySpeed = 8;
        SceneManager.LoadSceneAsync(2);
    }

    public void medium()
    {
        difficultySpeed = 15;
        SceneManager.LoadSceneAsync(2);
    }

    public void hard()
    {
        difficultySpeed = 20;
        SceneManager.LoadSceneAsync(2);
    }
}
