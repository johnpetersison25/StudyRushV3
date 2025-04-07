using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class quizV2 : MonoBehaviour
{
    public GameObject[] Quiz_Number;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultScoreText;
    int currentLevel;
    int score = 0;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        score = 0;
        UpdateScoreText();
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

        if (currentLevel + 1 < Quiz_Number.Length)
        {
            Quiz_Number[currentLevel].SetActive(false);
            currentLevel++;
            Quiz_Number[currentLevel].SetActive(true);
        }

        if (currentLevel == Quiz_Number.Length - 1)
        {
            ShowResult();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void ShowResult()
    {

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

}
