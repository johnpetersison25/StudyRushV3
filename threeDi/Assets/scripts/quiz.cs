using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class quiz : MonoBehaviour
{
    public GameObject[] Quiz_Number;
    int currentLevel;

    public TextMeshProUGUI scoreText;
    int score = 0;

    public GameObject resultPanel;
    public TextMeshProUGUI resultScoreText;

    public float speedChase;
    teacher teacher;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        score = 0;
        UpdateScoreText();

        resultPanel.SetActive(false); // Hide result panel at start

        teacher = FindAnyObjectByType<teacher>();

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
        score += 1;
        UpdateScoreText();
    }

    // Disable the current question
    Quiz_Number[currentLevel].SetActive(false);
    currentLevel++;

    // Check if there are more questions
    if (currentLevel < Quiz_Number.Length)
    {
        // Show the next question
        Quiz_Number[currentLevel].SetActive(true);
    }
    else
    {
        // All questions answered, show result
        ShowResult();
    }
}

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void ShowResult()
    {
        resultPanel.SetActive(true);

        foreach (GameObject questions in Quiz_Number)
        {
            Destroy(questions);
        }

        // Hide score text
        Color color = scoreText.color;
        color.a = 0;
        scoreText.color = color;

        // Display the final score in the result panel
        resultScoreText.text = "Final Score " + score+ " / 3";
    }

    void Update()
    {
        Chase();
        
    }

    public void Chase()
    {
        if(score <= 2)
        {
            speedChase = speedChase = 5;
        }
        else
        {
            speedChase = speedChase = 0;
        }
        
    }

    public void NextScene()
    {
        SceneManager.LoadSceneAsync(1);
        StartCoroutine(Delete());
    }


    IEnumerator Delete()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
