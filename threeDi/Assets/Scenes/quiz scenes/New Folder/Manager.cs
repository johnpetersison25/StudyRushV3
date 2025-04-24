using UnityEngine;

public class Manager : MonoBehaviour
{
    public static int collectedNotes;
    public static quizV2 quiz;

    public float MathAgentSpeed;
    public float EnglishAgentSpeed;
    public float ScienceAgentSpeed;
    public float HistoryAgentSpeed;
    public float FilipinoAgentSpeed;

    private bool updated = false;

    void Start()
    {
        quiz = FindObjectOfType<quizV2>();
    }

    void Update()
    {
        if (quiz == null)
        {
            quiz = FindObjectOfType<quizV2>();
            if (quiz == null) return;
        }

        if (quiz.quizdone && !updated)
        {
            int myScore = quiz.finalScore;

            if (quiz.currentSubject == quizV2.Subject.Math && myScore < 3)
                MathAgentSpeed = 6f;

            if (quiz.currentSubject == quizV2.Subject.English && myScore < 3)
                EnglishAgentSpeed = 5f;

            if (quiz.currentSubject == quizV2.Subject.Science && myScore < 3)
                ScienceAgentSpeed = 6f;

            if (quiz.currentSubject == quizV2.Subject.History && myScore < 3)
                HistoryAgentSpeed = 6f;

            if (quiz.currentSubject == quizV2.Subject.Filipino && myScore < 3)
                FilipinoAgentSpeed = 6f;

            updated = true;
        }
    }
}
