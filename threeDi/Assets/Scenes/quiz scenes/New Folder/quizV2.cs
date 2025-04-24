using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class quizV2 : MonoBehaviour
{
    public enum Subject { Math, English, Science, History, Filipino }
    public Subject currentSubject;

    public GameObject[] Quiz_Number;
    public GameObject resultPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultScoreText;

    private int currentLevel;
    private Dictionary<Subject, int> subjectScores = new Dictionary<Subject, int>();
    public int finalScore;
    public bool quizdone = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currentLevel = 0;
        InitializeScores();
        UpdateScoreText();
        resultPanel.SetActive(false);
    }

    void InitializeScores()
    {
        foreach (Subject subj in System.Enum.GetValues(typeof(Subject)))
        {
            subjectScores[subj] = 0;
        }
    }

    public void CorrectAnswer()
    {
        StartCoroutine(HandleAnswer(true));
    }

    public void WrongAnswer()
    {
        StartCoroutine(HandleAnswer(false));
    }

    private IEnumerator HandleAnswer(bool isCorrect)
    {
        yield return new WaitForSeconds(0.25f);

        if (isCorrect)
        {
            subjectScores[currentSubject]++;
            UpdateScoreText();
        }

        Quiz_Number[currentLevel].SetActive(false);
        currentLevel++;

        if (currentLevel < Quiz_Number.Length)
        {
            Quiz_Number[currentLevel].SetActive(true);
        }
        else
        {
            ShowResult();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = $"Score: {subjectScores[currentSubject]}";
    }

    void ShowResult()
    {
        resultPanel.SetActive(true);

        foreach (GameObject question in Quiz_Number)
        {
            Destroy(question);
        }

        // Hide score text
        Color color = scoreText.color;
        color.a = 0;
        scoreText.color = color;

        finalScore = subjectScores[currentSubject];
        resultScoreText.text = $"{finalScore} / {Quiz_Number.Length}";
    }

    public int GetSubjectScore(Subject subject)
    {
        return subjectScores.ContainsKey(subject) ? subjectScores[subject] : 0;
    }

    public void NextScene(string nameScene)
    {
        SceneManager.UnloadSceneAsync(nameScene);
        StartCoroutine(Delete());
        quizdone = true;
        Time.timeScale = 1;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);

        GameObject manager = GameObject.FindGameObjectWithTag("GameManager");

        if (manager != null)
        {
            Destroy(manager);
            Debug.Log("GameManager destroyed");
        }
        else
        {
            Debug.LogWarning("GameManager not found!");
        }
    }
}
