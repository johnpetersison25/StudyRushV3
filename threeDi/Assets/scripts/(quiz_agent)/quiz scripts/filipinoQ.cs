using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FilipinoQ : MonoBehaviour
{
    [SerializeField] GameObject[] quizQuestions;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject resultPanel;
    [SerializeField] TextMeshProUGUI resultScoreText;
    private int currentQuestionIndex = 0;
    public static int FilipinoScore;
    public static bool filipinoQuizIsDone = false;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        FilipinoScore = 0;
        filipinoQuizIsDone = false;
        UpdateScoreText();
        resultPanel.SetActive(false);
    }

    //=========================================================================
    public void CorrectAnswer()
    {
        StartCoroutine(HandleAnswer(true));
    }

    public void WrongAnswer()
    {
        StartCoroutine(HandleAnswer(false));
    }

    //=========================================================================
    private IEnumerator HandleAnswer(bool isCorrect)
    {
        yield return new WaitForSeconds(0.25f);

        if (isCorrect)
        {
            FilipinoScore++;
            UpdateScoreText();
        }

        // Disable the current question and move to the next
        quizQuestions[currentQuestionIndex].SetActive(false);
        currentQuestionIndex++;

        // Show the next question if available
        if (currentQuestionIndex < quizQuestions.Length)
        {
            quizQuestions[currentQuestionIndex].SetActive(true);
        }
        else
        {
            // No more questions, show result
            ShowResult();
        }
    }

  //=========================================================================
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + FilipinoScore.ToString();
    }

    // Method to show the final result
    void ShowResult()
    {
        resultPanel.SetActive(true);

        // Destroy all remaining quiz questions
        foreach (GameObject question in quizQuestions)
        {
            Destroy(question);
        }

        Destroy(scoreText);

        // Display the final score in the result panel
        resultScoreText.text = + FilipinoScore + " / " + quizQuestions.Length;
    }

    //=========================================================================
    public void NextScene(string NameOfThisScene)
    {
        SceneManager.UnloadSceneAsync(NameOfThisScene);
        StartCoroutine(DeleteQuizManagerAfterDelay());
        filipinoQuizIsDone = true;
        statica.collectedNotes++;

        foreach (GameObject teacher in statica.teachers)
        {
            if (teacher != null)
            teacher.SetActive(true);
        }
    }

    private IEnumerator DeleteQuizManagerAfterDelay()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
